# 03.04-mp3-executable-and-tests: Upgrade MP3 console executable and tests to .NET 10

## Objective
Preserve `Mp3Mangler` as a console executable targeting `net10.0`, move its entry point to consume the extracted library, and migrate `Mp3ManglerTest` alongside it to a supported .NET 10 test target and framework packages.

## Research confirmed
- Current executable was converted to SDK style but targets `net48`; its `Program.cs` calls `Mp3InfoExtractor`.
- The shared implementation is planned for `03.03-mp3-shared-library`; this task depends on that library.
- Test project targets `net48`, has MSTest 1.2.1 adapter/framework plus Microsoft.NET.Test.Sdk 16.*, and references the executable. Assessment reports MSTest packages as deprecated and recommends current 4.4.1.
- Local MP3 fixture exists at `Mp3ManglerTest/TestItems/NerdRockFromTheSun.mp3`; copy metadata and updated AppContext-based path were added in the preceding conversion task but not verified after change.
- The preceding test task's user-requested caveat must be revisited during this MP3 migration; do not claim behavior tests pass absent a successful run.
- **Current state confirmed**: Both projects reference `Mp3Mangler.Shared`. The shared library now targets `netstandard2.0` and uses `taglib-sharp-netstandard2.0` 2.1.0; shared-library extraction task verified Core/executable builds and the MP3 test passed 1/1. The test source now includes `using System;` for `AppContext`.
- **Package/tool findings**: The test project currently uses `Microsoft.NET.Test.Sdk` 16.* and MSTest adapter/framework 1.2.1. Package-version lookup confirms Microsoft.NET.Test.Sdk 18.10.1 and MSTest 4.4.1 for `net10.0`. The executable retains JetBrains.Annotations and several legacy `System.*` PackageReferences, despite its remaining `Program.cs` only using `System` and `Mp3InfoExtractor`; remove references made unnecessary by the framework/library migration and retain only required project dependencies. Existing `App.config` contains .NET Framework startup and binding redirects; review/remove those runtime-only settings for .NET 10.
- **Warning baseline**: Core/test builds previously surfaced CS0067 and MSB3277 warnings, and the old ML.NET dependency chain surfaced NU1903 for Newtonsoft.Json 10.0.3. User preference says do not stall on non-blocking warnings; capture any remaining warnings and do not suppress them.

## Steps
1. Retarget executable and test to `net10.0`, keeping executable output type and dependency on the shared library.
2. Replace outdated MSTest packages with compatible versions and update test project reference.
3. Resolve MP3 package and binding redirect issues as appropriate for modern .NET; modern .NET does not consume legacy App.config binding redirects in the same manner.
4. Build executable and test; run the MP3 behavior test and verify fixture availability.

**Done when**: Executable and test project target `net10.0`, build, and the MP3 behavior test passes; remaining deferred items are documented.
