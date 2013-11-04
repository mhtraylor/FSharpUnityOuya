module FSharp.Ouya.Control

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

//    Control.fs:  Module for Ouya system and controller events.
//    Think OUYA button menu, IAP, etc.

open FSharp.Ouya.Product
open FSharp.Ouya.Util

// This will probably change soon...
[<RequireQualifiedAccess>]
module Menu =

    type t = Button | Pause | Resume | Appearing

    let internal evn = new Event<t>()

    let trigger m = evn.Trigger(m)

    let event = evn.Publish

[<RequireQualifiedAccess>]
module IAP =
    
    type t = Cancel | Failure of Json.Error | Success of Product.t

    let internal evn = new Event<t>()

    let trigger m = evn.Trigger(m)

    let event = evn.Publish





