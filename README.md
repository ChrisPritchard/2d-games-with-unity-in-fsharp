# 2D Games with Unity in F\#

My work through the 2D games with unity book by [Jared Halpern](https://jaredhalpern.com/) and Apress, but using F# instead of C#.

Why? I just prefer the syntax of F#, and its fully compatible with the rest of the .NET runtime. Any functional advantages of F# over C# in the Unity space are a bonus, though most unity code is imperative/mutable/class-based which you just have to accept. F# can do all that pretty easily (public fields on classes are a bit ugly, but I can live with it).

## F# in Unity

It's pretty straightforward. I worked with [this article by Jackson Dunstan](https://jacksondunstan.com/articles/5058) to figure it out, kudos to him.

- The process is to have a netstandard library you build outside of your unity project, which has a nice post build action to copy the dll inside the project.

- All your netstandard F# library needs is a reference to whatever Unity dll you're using, e.g. UnityEngine to get access to MonoBehaviour (these can be found under the managed folder in your unity install).

  - Depending on your tolerance, it might be easier to add every dll under managed/unityengine. Particularly when using packages like cinemachine, in order to avoid the same type and namespace coming from difference assemblies.

- Finally, the FSharp.Core.dll needs to be adjacent to where you copy the dll. An easy way to get this is to publish your project once and grab the Core dll from the output, copying it over manually to unity.

After that, in Unity you will see your dll wherever you copied it, with a little arrow that allows you to access the scripts inside. Works like a charm.

## Further tip with F# in Unity

- If you want the 'I update scripts and they get auto-rebuilt by Unity' experience you could always `dotnet watch build` on the separate project, which would have the same effect. I did this during development and its very seamless.

- VS Code was a good editor to use, or any editor opened at the root of the project, as it allowed me to maintain the git repo with script changes and main unity project changes. I still used full Visual Studio for adding the dll references however.

- C# scripts by default are compiled into a single 'Assembly-CSharp.dll' that is located under the project Library folder. Referencing that dll should allow the F# scripts to call any C# scripts you might have added (though I didn't have any so can't confirm this).

- I need to figure out a nice way to reference the core Unity dlls rather than referencing the install directory (which is version and os and...me...specific). Its only needed for the build, but its still annoying. Maybe a lib folder? Also the unity package manager wouldn't allow me to download cinemachine (some known bug) so I cloned it from github and added from disk, which unfortunately is reflected in the package manifest. Need to fix that too.

This last note means that if you clone this project, you will probably need to update the reference paths before it will compile. But even though they are a lot most are just a different unity path and the path to cinemachine.

## Book Links

I bought it via Amazon: [Amazon Link](https://www.amazon.com/Developing-Games-Unity-Independent-Programming/dp/1484237714/ref=sr_1_1).

The official page is here: [Apress Link](https://www.apress.com/gp/book/9781484237717)

The author's personal page is [here](https://jaredhalpern.com/) and he is on twitter [here](https://twitter.com/JaredEHalpern).

## Licenses

All assets in this repo are taken from the [book GitHub repository](https://github.com/Apress/Devel-2D-Games-Unity), where they are licensed CC.
