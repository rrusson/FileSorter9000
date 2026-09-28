# 03.03-mp3-shared-library: Extract reusable MP3 code into compatibility library

## Objective
Create a shared MP3 class library from the reusable code currently in `Mp3Mangler`, without leaving Core or its test consumer broken. The library must target a compatibility TFM suitable for the still-unmigrated Core project (currently `netstandard2.0`).

## Research confirmed
- `Mp3Mangler` currently targets `net48` as an SDK-style executable. It contains `Mp3Processor`, `Mp3InfoExtractor`, `FileReader`, `FileWriter`, `Mp3Dto`, and `Mp3TagAndFileInfo`, plus `Program.cs` which calls `Mp3InfoExtractor`.
- `FileSorter9000.Core.csproj` currently references the executable project and source uses `Mp3Mangler` types. Core itself is outside this task and remains at its current TFM.
- `Mp3ManglerTest` references the executable and must switch to the library as part of the migration.
- **Dependency and source inventory**: `FileSorter9000.Core.csproj` is actually `netstandard2.0` and its only MP3 project edge is `Mp3Mangler`; `Mp3ManglerTest.csproj` targets `net48` and references `Mp3Mangler`; the executable's `Program.cs` uses `Mp3InfoExtractor`. The six reusable implementation files are `FileReader.cs`, `Mp3InfoExtractor.cs`, `Mp3Processor.cs`, `Mp3TagAndFileInfo.cs`, `mp3Dto.cs`, and `FileWriter.cs`; `Program.cs` and `Properties/AssemblyInfo.cs` stay in the executable. Source search confirms no `// STUB:` markers.
- **Library design**: `Mp3Mangler.Shared` is an SDK-style class library targeting `netstandard2.0`, the common compatible target for Core (`netstandard2.0`) and the current executable/test (`net48`). It preserves the `Mp3Mangler` namespace and links the existing implementation source files, excluding those sources from the executable's compile glob. Core, executable, and test now reference the shared project; their TFMs are unchanged.
- **Package scope**: Shared source directly uses CsvHelper 27.2.1 and TagLib. The original `taglib` package produces NU1701 on netstandard; package lookup confirmed `taglib-sharp-netstandard2.0` 2.1.0 as a compatible fork retaining the TagLib namespace/API, now used by the shared project. CsvHelper and TagLib were removed from the executable direct dependencies because its only remaining source is the console entry point. Other legacy package references remain for the executable migration task to reassess.
- **Validation consumers**: The shared library built on `netstandard2.0`; Core (`netstandard2.0`) and the executable (`net48`) built against it. Test restore used a temporary NuGet configuration because the user package-source mapping blocks the test SDK; `Mp3ManglerTest` now passes 1/1 after adding the required `System` import for `AppContext`. Its successful build reports two MSB3277 assembly-version conflict warnings involving CsvHelper's transitive support packages. The user requested non-blocking warnings not to stall progress; they are not suppressed and should be revisited during the executable/test package migration.

## Steps
1. Create a new SDK-style class library project for reusable MP3 implementation and its package dependencies.
2. Move/reassign reusable source files while preserving namespaces and keeping the executable entry point separate.
3. Update Core and test project references to the new compatibility library; do not change Core's framework yet.
4. Build the new library and its affected consumers on their existing compatible TFMs.

**Done when**: Shared library builds on the compatibility TFM and Core/test references target it without breaking their existing target frameworks.
