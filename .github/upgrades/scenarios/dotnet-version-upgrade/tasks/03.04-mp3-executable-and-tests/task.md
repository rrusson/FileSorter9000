# 03.04-mp3-executable-and-tests: Upgrade MP3 console executable and tests to .NET 10

## Objective
Preserve `Mp3Mangler` as a console executable targeting `net10.0`, move its entry point to consume the extracted library, and migrate `Mp3ManglerTest` alongside it to a supported .NET 10 test target and framework packages.

## Research confirmed
- Current executable was converted to SDK style but targets `net48`; its `Program.cs` calls `Mp3InfoExtractor`.
- The shared implementation is planned for `03.03-mp3-shared-library`; this task depends on that library.
- Test project targets `net48`, has MSTest 1.2.1 adapter/framework plus Microsoft.NET.Test.Sdk 16.*, and references the executable. Assessment reports MSTest packages as deprecated and recommends current 4.4.1.
- Local MP3 fixture exists at `Mp3ManglerTest/TestItems/NerdRockFromTheSun.mp3`; copy metadata and updated AppContext-based path were added in the preceding conversion task but not verified after change.
- The preceding test task's user-requested caveat must be revisited during this MP3 migration; do not claim behavior tests pass absent a successful run.

## Steps
1. Retarget executable and test to `net10.0`, keeping executable output type and dependency on the shared library.
2. Replace outdated MSTest packages with compatible versions and update test project reference.
3. Resolve MP3 package and binding redirect issues as appropriate for modern .NET; modern .NET does not consume legacy App.config binding redirects in the same manner.
4. Build executable and test; run the MP3 behavior test and verify fixture availability.

**Done when**: Executable and test project target `net10.0`, build, and the MP3 behavior test passes; remaining deferred items are documented.
