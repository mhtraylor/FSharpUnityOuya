module FSharp.Ouya.Input

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

//    Input.fs : Wraps up the OuyaInputFramework methods in functions for easy composition/pipelining, etc.

open UnityEngine

let getControllerName p = OuyaInput.GetControllerName(player=p)
let getControllerType p = OuyaInput.GetControllerType(player=p)

let getAxis p a = OuyaInput.GetAxis(axis=a,player=p)

let getButton p b = OuyaInput.GetButton(button=b,player=p)
let getButtonDown p b = OuyaInput.GetButtonDown(button=b,player=p)
let getButtonUp p b = OuyaInput.GetButtonUp(button=b,player=p)

let getJoystick p j = OuyaInput.GetJoystick(joystick=j,player=p)
let getJoystickAngle p j = OuyaInput.GetJoystickAngle(joystick=j,player=p)

let getTrigger p t = OuyaInput.GetTrigger(trigger=t,player=p)

let checkDeadzoneAxial x y d = OuyaInput.CheckDeadzoneAxial(xAxis=x,yAxis=y,deadzone=d)
let checkDeadzoneCircular x y r = OuyaInput.CheckDeadzoneCircular(xAxis=x,yAxis=y,deadRadius=r)
let checkDeadzoneRescaled x y r = OuyaInput.CheckDeadzoneRescaled(xAxis=x,yAxis=y,deadRadius=r)
    
let calculateJoystickAngle v = OuyaInput.CalculateJoystickAngle(joystickInput=v)

