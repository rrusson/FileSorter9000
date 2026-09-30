# 05.02.02-mvvm-and-toolkit-services: Migrate MVVM and Toolkit helper/toast dependencies

## Objective
Replace legacy Toolkit MVVM and selected Toolkit UWP helper/toast APIs using decisions recorded by 05.02.01.

## Scope
- MVVM consumers under `FileSorter9000/ViewModels/*.cs` and `Behaviors/TreeViewCollapseBehavior.cs`.
- Toolkit helper `Services/FirstRunDisplayService.cs`; toast APIs `Services/ToastNotificationsService.cs` and `Services/ToastNotificationsService.Samples.cs`.
- Adjust direct package references in `FileSorter9000/FileSorter9000.csproj` only as supported by the research task.

## Confirmed findings before implementation
- NuGet references are project-local. `CommunityToolkit.Mvvm` 8.4.2 is supported for the target TFM; replacing `Microsoft.Toolkit.Mvvm` requires changing the namespace from `Microsoft.Toolkit.Mvvm.*` to `CommunityToolkit.Mvvm.*` in the app's view models and `Behaviors/TreeViewCollapseBehavior.cs`.
- MVVM use consists of `ObservableObject`, `RelayCommand`, and `RelayCommand<T>`; preserve command behavior and observable properties.
- Toast service code uses `Microsoft.Toolkit.Uwp.Notifications` XML builders; lookup found `CommunityToolkit.WinUI.Notifications` 7.1.2, but the existing package currently restores. Activation depends on UWP `ToastNotificationActivatedEventArgs` and is not safe to replace without the app-lifecycle changes. Keep the toast code/package for now and migrate/validate alongside lifecycle work rather than guessing an API path.
- `FirstRunDisplayService` uses Toolkit `SystemInformation.Instance.IsFirstRun`, `CoreApplication.MainView.CoreWindow.Dispatcher`, and UWP `ContentDialog.ShowAsync`. This combines the helper package with UWP dispatch/dialog semantics; migrate it with the lifecycle task where the WinUI Window/XamlRoot exists. Do not make a partial service change that leaves dialog presentation invalid.
- `FileSorter9000.csproj` still has separate restore blockers from UWP controls/animations packages. Those belong to 05.02.03; a build after this narrow MVVM migration may remain restore-blocked until that task.
- Files confirmed to import legacy MVVM namespaces: all source hits in `ViewModels/*.cs` and `Behaviors/TreeViewCollapseBehavior.cs`. App package references include `Microsoft.Toolkit.Mvvm` 7.1.2 and the UWP packages at 7.0.2.
- Applied the MVVM replacement in nine view-model/behavior C# files and the app project file. Confirmed there are no remaining `Microsoft.Toolkit.Mvvm` imports or package references, and all edited source/project files have CRLF line endings.
- Targeted restore reached package validation but remains blocked by NU1202 for `Microsoft.Toolkit.Uwp` 7.0.2 and `Microsoft.Toolkit.Uwp.UI.Animations` 7.0.2. It also reported NU1903 for `SQLitePCLRaw.lib.e_sqlite3` 2.0.2 and NU1904 for `System.Drawing.Common` 4.7.0. No warnings were suppressed. Compilation cannot yet validate the new MVVM package while restore fails on those unrelated incompatible packages.
- First-run dialog and toast conversion are still pending: both depend on WinUI window/XamlRoot initialization and packaged activation behavior and are scoped for 05.02.04. This subtask addresses the MVVM dependency portion only; the parent integration task retains the unresolved service follow-up.

## Steps
1. Apply approved package updates/removals and namespace/API changes.
2. Preserve observable properties, commands, first-run preference behavior, and toast activation/content behavior where supported.
3. Build/restore the app after this focused change and record any remaining platform blockers.

**Done when**: MVVM consumers use the supported CommunityToolkit package and no longer import legacy MVVM namespaces. First-run and toast services remain tracked for WinUI lifecycle follow-up; the project is restored/built as far as unrelated package blockers permit.
