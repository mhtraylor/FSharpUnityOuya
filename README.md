# FSharp.Ouya

This project is now moving on to a full plugin based somewhat on [OuyaInputFramework](https://github.com/rendermat/OuyaInputFramework) and [Ouya Unity Plugin](https://github.com/ouya/ouya-unity-plugin). The goal is to have more of a "turn-key" solution to setting up Unity for Ouya deployment along with more idiomatic ways to access Ouya controller input and menu/IAP functionality from F#. 

Currently the code is something of an incomplete F# port of the C# plugin, and relies on the Java code from the official plugin. The Java side will most likely be replaced with a custom implementation soon. The code also still requires a *OuyaGameObject* for the Java-to-Unity callbacks.

Please consider this merely **experimental** at this time and not for general use.

## Setup/Installation

Expect real documentation soon. Normally, to include an F# dll in your Unity project you reference at least UnityEngine.dll and/or UnityEditor.dll. Under the current iteration, you will no longer have to reference any of the official Ouya plugin assemblies compiled by Unity, but you will have to install the Android (Java) side of the plugin. This will most likely be fixed within the next few updates.
