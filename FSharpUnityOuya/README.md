# FSharp.Ouya

Currently this project just has a few simple shim functions for the [OuyaInputFramework](https://github.com/rendermat/OuyaInputFramework) in order to make using Ouya input more idiomatic to F# (see the example in *InputTester.fs*). Eventually, will contain a full port (with discriminated unions and active patterns vs. enums) as well as a wrapper for the [Ouya Unity Plugin](https://github.com/ouya/ouya-unity-plugin). Please consider this merely **experimental** at this time and not for general use.

## Setup/Installation

Expect real documentation soon. Normally, to include an F# dll in your Unity project you reference at least UnityEngine.dll and/or UnityEditor.dll. To access types, etc. from the Ouya plugin and OuyaInputFramework you'll also have to reference (a copy of, preferably) both *Assembly-CSharp-firstpass.dll* and *Assembly-Csharp.dll* of your Ouya project after you have set everything up for Ouya development (i.e. once Unity has compiled these assemblies). Any major changes to the plugin or framework: you'll have to update your copies of these dlls. Or just use Reflection.

