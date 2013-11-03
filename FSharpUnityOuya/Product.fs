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

type Purchasable = Purchasable of string

type Product =
    {
        currencyCode : string
        identifier : string
        localPrice : single
        name : string
        priceInCents : int
        productVersionToBundle : int
    }

type Receipt =
    {
        currency : string
        gamer : string
        generatedDate : System.DateTime
        identifier : string
        localPrice : single
        priceInCents : int
        purchaseDate : System.DateTime
        uuid : string
    }



