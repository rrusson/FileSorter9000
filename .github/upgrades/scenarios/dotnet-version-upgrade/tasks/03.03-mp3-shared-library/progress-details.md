# Progress: 03.03-mp3-shared-library

## Changes
- Added `Mp3Mangler.Shared/Mp3Mangler.Shared.csproj` targeting `netstandard2.0`, with linked shared implementation files from `Mp3Mangler` and direct CsvHelper 27.2.1 plus `taglib-sharp-netstandard2.0` 2.1.0 dependencies.
- The package choice preserves the existing `TagLib` API while supporting Core's `netstandard2.0` target; the original `taglib` 2.1.0 package produced NU1701 and was unsuitable for the shared TFM.
- Added the shared project to the solution. Updated Core and Mp3ManglerTest project references to the new library. Updated the Mp3Mangler executable to reference the library, exclude the linked implementation files from its own compile glob, and remove CsvHelper/TagLib direct package references.
- Added `using System;` to the test source so the AppContext-based local fixture path compiles on the current `net48` target.

## Validation
- Shared library build succeeded on `netstandard2.0`.
- `FileSorter9000.Core` build succeeded on `netstandard2.0` with two existing CS0067 unused-event warnings in `FakeIdentityService`.
- `Mp3Mangler` executable build succeeded on `net48`.
- `Mp3ManglerTest` passed: 1 total, 1 succeeded, 0 failed. Test restore required an isolated temporary NuGet config due to user-level PackageSourceMapping; no global NuGet config was changed.
- The test build reports two MSB3277 assembly-version conflict warnings involving CsvHelper's transitive reference dependencies. No warnings were suppressed; user preference is to keep moving on non-blocking warnings, which are tracked for follow-up.

## Deviations / follow-up
- The original `taglib` package did not support the required `netstandard2.0` library. NuGet search found a compatible TagLib# fork, `taglib-sharp-netstandard2.0` 2.1.0; this keeps the existing API without changing implementation consumers.
- The earlier fixture adjustment is now validated successfully as part of this task.
