module FSharp.Ouya.Util

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

//    Util.fs:  Utility module for helper functions.

open System
open System.IO
open System.Runtime.Serialization
open System.Runtime.Serialization.Json
open System.Text
open System.Xml

let (|NullOrEmpty|_|) t =
    if String.IsNullOrEmpty(t) then None else Some t

[<RequireQualifiedAccess>]
module Json =

    [<DataContract>]
    type Error = 
        {
            [<field: DataMember(Name = "errorCode")>]
            errorCode : int
            [<field: DataMember(Name = "errorMessage")>]
            errorMessage : string
        }

    let toJson<'a> (ob:'a) =
        use m = new MemoryStream()
        (DataContractJsonSerializer(typeof<'a>)).WriteObject(m,ob) 
        Encoding.Default.GetString(m.ToArray())

    let fromJson<'a> (str:string) =
        use m = new MemoryStream(str |> Encoding.UTF8.GetBytes)
        (DataContractJsonSerializer(typeof<'a>)).ReadObject(m)
        :?> 'a 
