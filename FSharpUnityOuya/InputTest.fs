namespace FSharp.Ouya

//    Copyright © 2013 Sarissa Game Studio.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

//    InputTest.fs : A simple but unfinished port of OuyaInputTester.cs. 
//    Shows use of Input module, displays a single string of all input values in a GUI Label overlay.

open FSharp.Ouya.OuyaInput
open System.Text
open UnityEngine

[<AutoOpen>]
module __ =

    let parse<'T> n = System.Enum.Parse (typeof<'T>, n) :?> 'T

    let append (sb:StringBuilder) t s = sb.AppendFormat("{0} : {1}",t,s).AppendLine()
    
    let getAllAxes p a sb = Array.fold (fun sb n -> parse<OuyaAxis> n |> getAxis p |> append sb n) sb a

    let getAllButtons p a sb = Array.fold (fun sb n -> parse<OuyaButton> n |> getButton p |> append sb n) sb a

    let getAllJoysticks p a sb = Array.fold (fun sb n -> parse<OuyaJoystick> n |> getJoystick p |> append sb n) sb a

type InputTest () =
    inherit MonoBehaviour ()

//    Use explicit fields such as below to set values in Unity Inspector (if you really need to).
//    Otherwise let-bindings are preferred. This example doesn't check for uninitialized values.

    [<DefaultValue>] val mutable public player : OuyaPlayer
    [<DefaultValue>] val mutable public deadzoneType : DeadzoneType
    [<DefaultValue>] val mutable public deadzone : single
    [<DefaultValue>] val mutable public triggerThreshold : single

    let axes = System.Enum.GetNames(typeof<OuyaAxis>)
    let buttons = System.Enum.GetNames(typeof<OuyaButton>)
    let joysticks = System.Enum.GetNames(typeof<OuyaJoystick>)

//    Uncomment the following and import a suitable monospaced screen font into Unity resource folder if you wish.
//    Unity's default font (Arial) is not that great for this type of usage.
//
//    let mutable font = Unchecked.defaultof<Font>
//
//    member x.Awake () =
//        font <- Resources.Load("your_screen_font") :?> Font
        
    member x.Start () =
        OuyaInput.SetContinuousScanning(false)
        OuyaInput.SetDeadzone(x.deadzoneType,x.deadzone)
        OuyaInput.SetTriggerThreshold(x.triggerThreshold)
        OuyaInput.UpdateControllers()

    member x.OnGUI () =
//        GUI.skin.font <- font
        new StringBuilder()
        |> getAllAxes x.player axes
        |> getAllButtons x.player buttons
        |> getAllJoysticks x.player joysticks
        |> fun sb -> sb.AppendLine(OuyaInput.GetControllerName(x.player))
        |> fun sb -> sb.AppendLine(OuyaInput.GetControllerType(x.player).ToString())
        |> fun sb -> GUILayout.Label(sb.ToString())

    member x.Update () = OuyaInput.UpdateControllers()
