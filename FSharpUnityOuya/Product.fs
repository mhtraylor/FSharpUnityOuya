module FSharp.Ouya.Product

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

//    Product.fs:  Module for Ouya IAP types.

open System.Runtime.Serialization

type Purchasable = Purchasable of string

[<DataContract>]
type Product =
    {
        [<field: DataMember(Name = "currencyCode")>]
        currencyCode : string
        [<field: DataMember(Name = "identifier")>]
        identifier : string
        [<field: DataMember(Name = "localPrice")>]
        localPrice : single
        [<field: DataMember(Name = "name")>]
        name : string
        [<field: DataMember(Name = "priceInCents")>]
        priceInCents : int
        [<field: DataMember(Name = "productVersionToBundle")>]
        productVersionToBundle : int
    }

[<DataContract>]
type Receipt =
    {
        [<field: DataMember(Name = "currency")>]
        currency : string
        [<field: DataMember(Name = "gamer")>]
        gamer : string
        [<field: DataMember(Name = "generatedDate")>]
        generatedDate : System.DateTime
        [<field: DataMember(Name = "identifier")>]
        identifier : string
        [<field: DataMember(Name = "localPrice")>]
        localPrice : single
        [<field: DataMember(Name = "priceInCents")>]
        priceInCents : int
        [<field: DataMember(Name = "purchaseDate")>]
        purchaseDate : System.DateTime
        uuid : string
    }

type t = 
    | GamerUUID of string 
    | SingleProduct of Product 
    | SingleReceipt of Receipt 
    | ProductList of Product list 
    | ReceiptList of Receipt list


