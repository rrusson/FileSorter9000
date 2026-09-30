# 02.03-mp3mangler-tests: Convert Mp3ManglerTest to SDK style

## Objective
Convert `Mp3ManglerTest/Mp3ManglerTest.csproj` to SDK style without changing its .NET Framework 4.8 target, MSTest package versions, or reference to Mp3Mangler.

## Scope and research
The test library targets .NET Framework 4.8 and has `Mp3ManglerTest.cs`, `Properties/AssemblyInfo.cs`, and the local fixture `TestItems/NerdRockFromTheSun.mp3`. `get_project_dependencies` confirmed MSTest.TestAdapter and MSTest.TestFramework 1.2.1 and the `Mp3Mangler` project reference. `Mp3Mangler` was already converted to SDK style on `net48` and builds successfully. The SDK conversion preserved `net48`, the project reference, and MSTest versions; it removed `packages.config` and added `Microsoft.NET.Test.Sdk` 16.*. The conversion and initial build succeeded. An initial test run discovered one test but failed because its previous relative fixture path did not resolve in the SDK-style output layout. The test was changed to use `AppContext.BaseDirectory`, and the project was configured to copy the local fixture to output; post-change validation was cancelled at the user's request, so this adjustment remains unverified. Do not report the test as passing.

The conversion tool generated an additional `Microsoft.NET.Test.Sdk` `16.*` PackageReference (the old project did not contain this package). The user's NuGet configuration enables package source mapping but does not map this package to any source, so the first restore failed with NU1100. Avoid modifying the user NuGet configuration; validate using an isolated temporary NuGet configuration mapping packages to nuget.org. The test project's existing assembly version is already fixed at `1.0.0.0`.

## Steps
1. Use the retrieved topological ordering: `Mp3Mangler` is converted before this dependent test project.
2. Call the dedicated SDK-style conversion tool for this project only.
3. Build the converted project on its original `net48` TFM. Initial restore required an isolated temporary NuGet configuration because the user's source mapping did not include the converter-added test SDK package; the global NuGet configuration was not changed.
4. Verify `packages.config` was removed, the `Mp3Mangler` project reference remains, and record test discovery/run results and the unverified fixture adjustment.

**Done when**: The format conversion is complete, the project remains on .NET Framework 4.8, the `Mp3Mangler` project reference and MSTest package versions are retained, and build/test evidence is recorded. The initial build succeeded; the test failure and unverified attempted fix are explicitly documented for follow-up.
