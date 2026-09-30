# Progress Details — 05.02.02 MVVM and Toolkit services

## Changes
- Replaced `Microsoft.Toolkit.Mvvm` 7.1.2 with supported `CommunityToolkit.Mvvm` 8.4.2 in `FileSorter9000/FileSorter9000.csproj`.
- Updated all MVVM namespace imports in app view models and `Behaviors/TreeViewCollapseBehavior.cs` from `Microsoft.Toolkit.Mvvm.*` to `CommunityToolkit.Mvvm.*`.
- Confirmed by search that no `Microsoft.Toolkit.Mvvm` imports/package references remain.
- Deferred `FirstRunDisplayService` and toast conversion to the lifecycle migration because both depend on WinUI window/dialog initialization or packaged notification activation. The rationale and exact follow-up are in task.md.
- All edited source/project/workflow files are CRLF; `.gitattributes` now declares repository-wide `text=auto eol=crlf`.

## Validation
- `dotnet restore FileSorter9000/FileSorter9000.csproj` reached package validation but failed with NU1202 for `Microsoft.Toolkit.Uwp` 7.0.2 and `Microsoft.Toolkit.Uwp.UI.Animations` 7.0.2. These are separate planned 05.02.03 blockers.
- Restore emitted NU1903 for `SQLitePCLRaw.lib.e_sqlite3` 2.0.2 and NU1904 for `System.Drawing.Common` 4.7.0. Warnings are recorded and not suppressed.
- The UI project cannot compile/verify MVVM against its new package until incompatible package restore blockers are removed. No tests were run because the app's assets are not restorable yet.
