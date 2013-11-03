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
open FSharp.Ouya.Product
open UnityEngine

// Initialization.
do
    if 
        Application.platform <> RuntimePlatform.Android || Application.isEditor
    then
        ()
    else 
        AndroidJNI.AttachCurrentThread() |> ignore

        let cls = AndroidJNI.FindClass(name="com/unity3d/player/UnityPlayer")
        let fid = AndroidJNI.GetStaticFieldID(cls,"currentActivity","Landroid/app/Activity;")
        let oba = AndroidJNI.GetStaticObjectField(cls,fid)

        let cls = AndroidJNI.FindClass("tv/ouya/sdk/OuyaUnityPlugin")
        let mid = AndroidJNI.GetMethodID(cls,"<init>","(Landroid/app/Activity;)")
        let obc = AndroidJNI.NewObject(cls,mid,[| new jvalue(l = oba) |])

        Debug.Log("JavaClass: " + obc.ToString())

[<Literal>] 
let JAVA_CLASS = "tv.ouya.sdk.OuyaUnityPlugin"

// AndroidJNI wrapper.
let tryCall jc f =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    try
        try
            use jc = new AndroidJavaClass(jc)
            f jc
        with
            | _ as e -> Debug.LogError(sprintf "FSharp.Ouya.Java error: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore

let tryCallReturn<'a> jc f =
    AndroidJNI.AttachCurrentThread() |> ignore
    AndroidJNI.PushLocalFrame(0) |> ignore
    let mutable rtn = Unchecked.defaultof<'a>
    try
        try
            use jc = new AndroidJavaClass(jc)
            rtn <- f jc
        with
            | _ as e -> Debug.LogError(sprintf "FSharp.Ouya.Java error: %A" e)
    finally
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero) |> ignore
    rtn

let init id = 
    let f id (jc:AndroidJavaClass) =
        jc.CallStatic<string>("setDeveloperID",[| id + "\0" |]) |> ignore
        jc.CallStatic("unityInitialized") |> ignore
    tryCall JAVA_CLASS (f id)

let setResolution id = 
    let f id (jc:AndroidJavaClass) =
        jc.CallStatic("setResolution",[| id + "\0" |])
    tryCall JAVA_CLASS (f id)

let showCursor (b:bool) = 
    let f (b:bool) (jc:AndroidJavaClass) =
        jc.CallStatic("showCursor",[| b.ToString() |])
    tryCall JAVA_CLASS (f b)

let putGameData (k,v) = 
    let f (k,v) (jc:AndroidJavaClass) =
        jc.CallStatic("putGameData",[| k + "\0"; v + "\0" |])
    tryCall JAVA_CLASS (f (k,v))

let getGameData k = 
    let f k (jc:AndroidJavaClass) =
        jc.CallStatic<string>("getGameData",[| k + "\0" |])
    tryCallReturn<string> JAVA_CLASS (f k)

let fetchGamerUUID () = 
    let f (jc:AndroidJavaClass) = 
        jc.CallStatic("fetchGamerUUID")
    tryCall JAVA_CLASS (f)

let addGetProduct (Purchasable pid) =
    let f (pid:string) (jc:AndroidJavaClass) =
        jc.CallStatic("addGetProduct",pid)
    tryCall JAVA_CLASS (f pid)

let debugGetProductList () =
    let f (jc:AndroidJavaClass) =
        jc.CallStatic("debugGetProductList")
    tryCall JAVA_CLASS (f)

let clearGetProductList () =
    let f (jc:AndroidJavaClass) =
        jc.CallStatic("clearGetProductList")
    tryCall JAVA_CLASS (f)

let getProductsAsync () =
    let f (jc:AndroidJavaClass) =
        jc.CallStatic("getProductsAsync")
    tryCall JAVA_CLASS (f)

let requestPurchaseAsync (Purchasable pid) =
    let f (pid:string) (jc:AndroidJavaClass) =
        jc.CallStatic<string>("requestPurchaseAsync",[| pid + "\0" |]) 
        |> ignore
    tryCall JAVA_CLASS (f pid)
