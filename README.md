# 2D Games with Unity in F\#

My work through the 2D games with unity book by Apress, but using F# instead of C#.

Why? I just prefer the syntax of F#, and its fully compatible with the rest of the .NET runtime. Any functional advantages of F# over C# in the Unity space are probably minimal due to the nature of how Unity code works.

## F# in Unity

It's pretty straightforward.

- The process is to have a netstandard library you build outside of your unity project, which has a nice post build action to copy the dll inside the project. - All your netstandard F# library needs is a reference to whatever Unity dll your using, e.g. UnityEngine to get access to MonoBehaviour (these can be found under the managed folder in your unity install).
- Finally, the FSharp.Core.dll needs to be adjacent to where you copy the dll. An easy way to get this is to publish your project once and grab the Core dll from the output, copying it over manually to unity.

After that, in Unity you will see your dll wherever you copied it, with a little arrow that allows you to access the scripts inside. Works like a charm.

## Book Link

I bought it via Amazon: [Amazon Link](https://www.amazon.com/Developing-Games-Unity-Independent-Programming/dp/1484237714/ref=sr_1_1).

The official page is here: [Apress Link](https://www.apress.com/gp/book/9781484237717)

## Licenses

All assets in this repo are taken from the [original repository](https://github.com/Apress/Devel-2D-Games-Unity) where they are licensed CC.
