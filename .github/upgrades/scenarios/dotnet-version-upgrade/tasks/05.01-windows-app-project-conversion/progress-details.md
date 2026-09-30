# Progress Details — 05.01 Windows App project conversion

## Changes
- Converted `FileSorter9000/FileSorter9000.csproj` from legacy UWP format to SDK-style Windows App SDK/WinUI targeting `net10.0-windows10.0.26100.0`.
- Preserved packaged-app configuration, manifest reference, minimum Windows platform, assets, and the `FileSorter9000.Core` project reference.
- Added/retained Windows App SDK 2.5.1 and Win2D 1.4.0; package references remain per-project (no CPM).
- Updated this task's research record with the verified project/package state and validation findings.

## Validation
- Project dependency inspection confirmed package references are defined directly in the app project.
- `dotnet restore` against nuget.org still fails with NU1202 for `Microsoft.Toolkit.Uwp` 7.0.2 and `Microsoft.Toolkit.Uwp.UI.Animations` 7.0.2. Their API/XAML migration belongs to 05.02; this is the current build blocker.
- Restore also emitted NU1903 for `SQLitePCLRaw.lib.e_sqlite3` 2.0.2 and NU1904 for `System.Drawing.Common` 4.7.0. These were not suppressed; record for follow-up package remediation.
- Earlier VS MSBuild validation of the legacy UWP project compiled C# but packaging failed at APPX3207 (manifest-referenced oversized tile image assets) and warned APPX0108 (expired temporary signing certificate). No packaged launch/runtime validation has been completed.

## Status and follow-up
- The project-format/targeting conversion and packaged-app project configuration are in place. Full app build and activation validation remain blocked until 05.02 migrates the incompatible Toolkit dependencies and APIs; packaging asset and signing issues also remain documented.
- Continue with the next available task, 05.02, to remove the restore blockers and migrate UWP API/XAML consumers. Do not suppress warnings.
