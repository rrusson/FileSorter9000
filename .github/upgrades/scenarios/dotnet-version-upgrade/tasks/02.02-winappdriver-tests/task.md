# 02.02-winappdriver-tests: Convert WinAppDriver tests to SDK style

## Objective
Convert `FileSorter9000.Tests.WinAppDriver/FileSorter9000.Tests.WinAppDriver.csproj` to SDK style without changing its current TFM or package versions.

## Scope and research
Confirmed the live project is an old-style test library targeting .NET Framework 4.8 (`TargetFrameworkVersion` `v4.8`), with one test source file (`BasicTests.cs`) and `Properties/AssemblyInfo.cs`. It has no `packages.config`; its three inline package references are Appium.WebDriver 4.3.1 and MSTest.TestAdapter/MSTest.TestFramework 2.2.4. Other explicit references are System, System.Core, and System.Drawing. The old file imports the conditional Visual Studio `Microsoft.TestTools.targets` and C# targets. The project dependency query confirmed no project references and the same three package versions. Do not modernize packages, alter APIs, or change the TFM in this structural conversion; those concerns are reserved for later migration work.

The initial SDK-style build exposed CS8357 in `Properties/AssemblyInfo.cs`: the prior wildcard `[assembly: AssemblyVersion("1.0.*")]` is incompatible with deterministic compilation. Per the user's selection, set `AssemblyVersion` to `1.0.0.0`, matching the already fixed `AssemblyFileVersion`. The test method is discovered successfully, but running it requires WinAppDriver to be listening at `127.0.0.1:4723` and an installed/launched app; this prerequisite is documented in `BasicTests.cs`.

## Steps
1. Use the retrieved topological ordering; convert after `Mp3Mangler` and before `Mp3ManglerTest`.
2. Call the dedicated SDK-style conversion tool for this project only.
3. Build the converted `net48` project directly with restore.
4. Confirm the TFM and package versions are unchanged and test discovery remains available; attempt the test and record its external driver prerequisite.

**Done when**: The project is SDK-style, still targets .NET Framework 4.8, restores/builds without warnings, and preserves its test references and package versions. Test discovery is available; executing the UI automation may require the external WinAppDriver service and installed app.
