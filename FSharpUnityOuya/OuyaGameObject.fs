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
    val mutable public id : string

    // Event handling.
    member x.OuyaEvent =
        event.Publish

    member x.OuyaEventFire(e) =
        event.Trigger(e)

    member x.onMenuButtonUp () =
        x.OuyaEventFire(MenuButton)

    member x.onMenuAppearing () =
        x.OuyaEventFire(MenuAppear)

    member x.onPause () =
        x.OuyaEventFire(Pause)

    member x.onResume () =
        x.OuyaEventFire(Resume)

    // Unity callbacks.
    member x.Start () =
        Java.init x.id



