# 05.01-windows-app-project-conversion: Convert UWP project to Windows App SDK .NET 10

## Objective
Replace the legacy UWP project format with an SDK-style Windows App SDK/WinUI project targeting net10.0-windows, preserving app activation/packaging and the netstandard2.0 Core bridge as feasible.

## Scope and research
- Project: `FileSorter9000/FileSorter9000.csproj` and its app manifest/package assets/XAML/code-behind as required.
- Current project is non-SDK UWP (AppContainerExe, UAP 19041 min 18362) with 124 files, explicit Compile/Page/PRI items, Microsoft.NETCore.UniversalWindowsPlatform 6.2.12, Microsoft.UI.Xaml 2.5.0, Microsoft.Toolkit UWP dependencies, Win2D.uwp and generated UWP XAML import.
- Assessment proposes `net10.0-windows10.0.26100.0`, Windows App SDK 2.5.1, Microsoft.Graphics.Win2D 1.1.0, and Microsoft.Windows.Compatibility 10.0.12; four UWP packages have no supported replacement identified.
- Current environment has VS 2026 Insiders MSBuild and the UWP XAML targets. The SDK CLI build uses a different MSBuild extensions path and cannot build the legacy project directly.

## Research confirmed
- The app has 124 tracked project items: 49 C# source files, 13 XAML pages/resources, app manifest, localized `.resw`, image/media assets, plus legacy ruleset/key. Legacy project uses explicit Compile/Page/Content entries, target UAP 10.0.19041 / min 10.0.18362, AppContainerExe output, x86/x64/ARM/ARM64 configurations, MSIX-style `Package.appxmanifest`, and app entry point `FileSorter9000.App`.
- App launch is implemented in `App.xaml.cs` using `Windows.UI.Xaml.Application`, `OnLaunched`/`OnActivated`/`OnBackgroundActivated`, an `ActivationService`, and a ShellPage. Retain the manifest identity/capabilities/assets and packaged activation expectations; WinUI app window bootstrap needs migration in this phase or immediately in the following API task.
- Explicit direct app packages include AppCenter Analytics/Crashes 4.3.0, Toolkit.Mvvm 7.1.2, several Microsoft.Toolkit.Uwp 7.0.2 packages, Microsoft.UI.Xaml 2.5.0, and Microsoft.Xaml.Behaviors.Uwp.Managed 2.0.1. Assessment flags Microsoft.NETCore.UniversalWindowsPlatform, Toolkit UWP controls/animations, and UWP XAML Behaviors as incompatible; it proposes Microsoft.WindowsAppSDK 2.5.1, Microsoft.Graphics.Win2D 1.1.0, and Microsoft.Windows.Compatibility 10.0.12. Package lookup confirms Windows App SDK 2.5.1; current stable Win2D lookup returns 1.4.0, differing from stale assessment data, so verify package compatibility before choosing. CommunityToolkit.WinUI is not resolved by current supported-package lookup.
- Source has widespread UWP `Windows.UI.Xaml`/`Windows.ApplicationModel`/`Windows.Storage` usage, plus background tasks, toast, media, pickers, behaviors, and toolkit controls. Assessment lists 4 source-incompatible and 20 behavioral API incidents; this is not a mechanical TFM conversion and requires staged API/XAML migration.
- The automation test project is a separate net481 SDK-conversion/test migration subtask. No stub markers were found in app or UI automation test C# sources.
- The prior MSB4019 was produced by `dotnet build` resolving MSBuild extensions under the .NET SDK path. VS MSBuild has the forwarding target at `C:\Program Files\Microsoft Visual Studio\18\Insiders\MSBuild\Microsoft\WindowsXaml\v18.0\Microsoft.Windows.UI.Xaml.CSharp.Targets`. VS MSBuild confirmed C# compilation, but package validation failed with APPX3207 (oversized manifest-referenced tile images); it also warned the configured temporary signing certificate is expired (APPX0108). No app launch/package validation was completed.
- Windows App SDK templates are not installed (`dotnet new list winui` reports none). Modernization therefore needs careful project recreation/SDK-style conversion; do not assume conversion is fully supported by the generic converter.
- The SDK conversion tool has now converted the project file to SDK style while retaining `net6.0-windows10.0.19041.0` and the existing UWP package list. It generated default UWP preprocessor constants and `UseWinUI`; this intermediate file still requires explicit retargeting to the assessed .NET 10 Windows TFM and replacement of the UWP platform framework/package references. The converted file was normalized to CRLF immediately.
- Package lookup confirms `Microsoft.WindowsAppSDK` 2.5.1 and `Microsoft.Graphics.Win2D` 1.4.0 for modern Windows targets. The assessment's Win2D 1.1.0 is stale; use the current compatible version when needed. UWP Toolkit controls/animations/behaviors remain a separate migration concern; no replacement package is assumed without validating corresponding WinUI APIs.

## Current execution findings
- Confirmed `FileSorter9000.csproj` is SDK-style with `net10.0-windows10.0.26100.0`, `UseWinUI`, MSIX tooling, `Microsoft.WindowsAppSDK` 2.5.1, Win2D 1.4.0, and a project reference to `FileSorter9000.Core`.
- Dependency discovery confirms package references are directly in this project (no CPM): AppCenter Analytics/Crashes 4.3.0, Microsoft.Toolkit.Mvvm 7.1.2, Toolkit.Uwp packages 7.0.2, Windows App SDK 2.5.1, Win2D 1.4.0.
- Restore was re-run against nuget.org. It reports NU1202 specifically for Microsoft.Toolkit.Uwp 7.0.2 and Microsoft.Toolkit.Uwp.UI.Animations 7.0.2; the failure confirms the remaining work is API/package migration, assigned to 05.02. It also reports NU1903/NU1904 for transitive SQLitePCLRaw.lib.e_sqlite3 2.0.2 and System.Drawing.Common 4.7.0, recorded for later handling without suppressing.
- The converted project preserves package-manifest configuration (`Package.appxmanifest`), assets, target minimum, and Core reference. The app does not yet build/restore; documented blockers include the Toolkit incompatibility plus pre-existing APPX3207 oversized logos and expired temporary signing certificate (APPX0108). App launch/package runtime validation remains pending.

## Steps
1. Inspect app XAML, manifest, packages, project item declarations, and activation/packaging assumptions.
2. Convert/recreate the app as SDK-style Windows App SDK with net10.0-windows, preserving assets and entry-point behavior.
3. Replace/remove framework/package references and adapt app project content items as required by WinUI.
4. Build with Visual Studio MSBuild and validate launch/package behavior if available; record hardware/environment blockers.

**Done when**: The migrated Windows App SDK app project builds and its package/activation model is preserved or explicitly documented where blocked.
