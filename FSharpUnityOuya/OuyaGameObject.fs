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

//    OuyaGameObject.fs:  MonoBehaviour for interfacing with Android.

open FSharp.Ouya.Control
open UnityEngine

type OuyaGameObject () =
    inherit MonoBehaviour ()

    [<DefaultValue>]
    val mutable public developerID : string

    // Event handling.
    member x.onMenuButtonUp () =
        OuyaEvent.fire (MenuButton)

    member x.onMenuAppearing () =
        OuyaEvent.fire (MenuAppear)

    member x.onPause () =
        OuyaEvent.fire (Pause)

    member x.onResume () =
        OuyaEvent.fire (Resume)

    // Unity callbacks.
    member x.Start () =
        GameObject.DontDestroyOnLoad(x.transform.gameObject)
        Java.init x.developerID



