module FSharp.Ouya.Java

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

//    Java.fs:  Module for Ouya-specific Android JNI functions.

open UnityEngine

do 
    AndroidJNI.AttachCurrentThread() |> ignore

    let cls = AndroidJNI.FindClass(name="com/unity3d/player/UnityPlayer")
    let fid = AndroidJNI.GetStaticFieldID(cls,"currentActivity","Landroid/app/Activity;")
    let oba = AndroidJNI.GetStaticObjectField(cls,fid)

    let cls = AndroidJNI.FindClass("tv/ouya/sdk/OuyaUnityPlugin")
    let mid = AndroidJNI.GetMethodID(cls,"<init>","(Landroid/app/Activity;)")
    let obc = AndroidJNI.NewObject(cls,mid,[| new jvalue(l = oba) |])

    Debug.Log("JavaClass: " + obc.ToString())

let init id =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    try
        try
            use jc = new AndroidJavaClass("tv.ouya.sdk.OuyaUnityPlugin")
            jc.CallStatic<string>("setDeveloperID",[| id + "\0" |]) |> ignore
            jc.CallStatic("unityInitialized") |> ignore
        with
            | _ as e -> Debug.LogError(sprintf "FSharp.Ouya.Java initialization error: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore

let setResolution id =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    try
        try
            use jc = new AndroidJavaClass("tv.ouya.sdk.OuyaUnityPlugin")
            jc.CallStatic("setResolution", [| id + "\0" |]) |> ignore
        with
            | _ as e -> Debug.LogError(sprintf "FSharp.Ouya.Java unable to set resolution: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore

let showCursor (b:bool) =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    try
        try
            use jc = new AndroidJavaClass("tv.ouya.sdk.OuyaUnityPlugin")
            jc.CallStatic("showCursor",[| b.ToString() |]) |> ignore
        with
            | _ as e -> Debug.LogError(sprintf "FSharp.Ouya.Java unable to show cursor: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore

let putGameData (k,v) =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    try
        try
            use jc = new AndroidJavaClass("tv.ouya.sdk.OuyaUnityPlugin")
            jc.CallStatic("putGameData",[| k + "\0"; v + "\0" |]) |> ignore
        with
            | _ as e -> Debug.LogError(sprintf "FSharp.Ouya.Java unable to put game data: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore

let getGameData k =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    let mutable r = ""
    try
        try
            use jc = new AndroidJavaClass("tv.ouya.sdk.OuyaUnityPlugin")
            r <- jc.CallStatic<string>("getGameData",[| k + "\0" |])
        with
            | _ as e -> Debug.LogError(sprintf "Unable to set developer id: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore
    r

let fetchGamerUUID () =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    try
        try
            use jc = new AndroidJavaClass("tv.ouya.sdk.OuyaUnityPlugin")
            jc.CallStatic("fetchGamerUUID")
        with
            | _ as e -> Debug.LogError(sprintf "Unable to set developer id: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore

