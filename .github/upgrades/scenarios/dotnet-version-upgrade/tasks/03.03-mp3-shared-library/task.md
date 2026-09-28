# 03.03-mp3-shared-library: Extract reusable MP3 code into compatibility library

## Objective
Create a shared MP3 class library from the reusable code currently in `Mp3Mangler`, without leaving Core or its test consumer broken. The library must target a compatibility TFM suitable for the still-unmigrated Core project (currently `netstandard2.0`).

## Research confirmed
- `Mp3Mangler` currently targets `net48` as an SDK-style executable. It contains `Mp3Processor`, `Mp3InfoExtractor`, `FileReader`, `FileWriter`, `Mp3Dto`, and `Mp3TagAndFileInfo`, plus `Program.cs` which calls `Mp3InfoExtractor`.
- `FileSorter9000.Core.csproj` currently references the executable project and source uses `Mp3Mangler` types. Core itself is outside this task and remains at its current TFM.
- `Mp3ManglerTest` references the executable and must switch to the library as part of the migration.

## Steps
1. Create a new SDK-style class library project for reusable MP3 implementation and its package dependencies.
2. Move/reassign reusable source files while preserving namespaces and keeping the executable entry point separate.
3. Update Core and test project references to the new compatibility library; do not change Core's framework yet.
4. Build the new library and its affected consumers on their existing compatible TFMs.

**Done when**: Shared library builds on the compatibility TFM and Core/test references target it without breaking their existing target frameworks.
