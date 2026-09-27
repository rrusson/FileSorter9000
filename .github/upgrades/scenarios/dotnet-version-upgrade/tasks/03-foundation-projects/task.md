# 03-foundation-projects: Upgrade independent projects and MP3 components

Upgrade the leaf projects `AiSorter` and `MLModelMusicFiling_ConsoleApp1` to .NET 10 and update their dependencies, including replacements for deprecated packages where needed. In the MP3 component group, extract reusable processing functionality from the .NET Framework `Mp3Mangler` executable into a new shared library, preserve the executable as a .NET 10 console app, and migrate `Mp3ManglerTest` with its tested component. Use a temporary compatibility target for the shared library if required so the still-unmigrated Core/UWP consumers can continue using it; remove transitional TFMs after those consumers move.

**Done when**: The independent projects and MP3 executable/tests build on .NET 10, the shared library builds for its required temporary and final consumers, and MP3 behavior is covered by passing tests.
