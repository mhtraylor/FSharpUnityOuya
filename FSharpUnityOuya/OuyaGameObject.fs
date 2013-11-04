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
open FSharp.Ouya.Product
open FSharp.Ouya.Util
open UnityEngine

type OuyaGameObject () =
    inherit MonoBehaviour ()

    // Public fields. These are exposed to the Unity Editor Inspector.
    [<DefaultValue>]
    val mutable public developerID : string

    // Private backing stores. Mutable!
    let mutable products = List.empty<Product>
    let mutable receipts = List.empty<Receipt>

    // Menu event triggers.
    member x.onMenuButtonUp () =
        Menu.trigger (Menu.Button)

    member x.onMenuAppearing () =
        Menu.trigger (Menu.Appearing)

    member x.onPause () =
        Menu.trigger (Menu.Pause)

    member x.onResume () =
        Menu.trigger (Menu.Resume)

    // IAP event triggers.
    member x.FetchGamerUUIDSuccessListener json =
        json |> GamerUUID |> IAP.Success |> IAP.trigger

    member x.FetchGamerUUIDFailureListener json =
        json |> Json.fromJson<Json.Error> |> IAP.Failure |> IAP.trigger

    member x.FetchGamerUUIDCancelListener json =
        IAP.Cancel |> IAP.trigger

    member x.ProductListClearListener json =
        [] |> ProductList |> IAP.Success |> IAP.trigger

    member x.ProductListListener = function
        | NullOrEmpty json ->
            products <- Json.fromJson<Product> json :: products 
            products 
            |> ProductList 
            |> IAP.Success
            |> IAP.trigger
        | _ -> Debug.LogError("OuyaGameObject: received empty json data.")

    member x.ProductFailureListener json =
        json |> Json.fromJson<Json.Error> |> IAP.Failure |> IAP.trigger

    member x.ProductCompleteListener json =
        products |> ProductList |> IAP.Success |> IAP.trigger

    member x.PurchaseSuccessListener json =
        json |> Json.fromJson<Product> |> SingleProduct |> IAP.Success |> IAP.trigger

    member x.PurchaseFailureListener json =
        json |> Json.fromJson<Json.Error> |> IAP.Failure |> IAP.trigger

    member x.PurchaseCancelListener json =
        IAP.Cancel |> IAP.trigger

    member x.ReceiptListClearListener json =
        [] |> ReceiptList |> IAP.Success |> IAP.trigger

    member x.ReceiptListListener = function
        | NullOrEmpty json ->
            receipts <- Json.fromJson<Receipt> json :: receipts
            receipts
            |> ReceiptList
            |> IAP.Success
            |> IAP.trigger
        | _ -> Debug.LogError("OuyaGameObject: received empty json data")

    member x.ReceiptListFailureListener json =
        json |> Json.fromJson<Json.Error> |> IAP.Failure |> IAP.trigger

    member x.ReceiptListCompleteListener json =
        receipts |> ReceiptList |> IAP.Success |> IAP.trigger

    member x.ReceiptListCancelListener json =
        IAP.Cancel |> IAP.trigger
        
    // Initialization.
    member x.RequestUnityAwake json =
        Java.init x.developerID        

    // Unity callbacks.
    member x.Start () =
        GameObject.DontDestroyOnLoad(x.transform.gameObject)
        Java.init x.developerID



