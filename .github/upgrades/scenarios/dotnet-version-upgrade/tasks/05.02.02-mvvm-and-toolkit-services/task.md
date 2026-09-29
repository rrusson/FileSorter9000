# 05.02.02-mvvm-and-toolkit-services: Migrate MVVM and Toolkit helper/toast dependencies

## Objective
Replace legacy Toolkit MVVM and selected Toolkit UWP helper/toast APIs using decisions recorded by 05.02.01.

## Scope
- MVVM consumers under `FileSorter9000/ViewModels/*.cs` and `Behaviors/TreeViewCollapseBehavior.cs`.
- Toolkit helper `Services/FirstRunDisplayService.cs`; toast APIs `Services/ToastNotificationsService.cs` and `Services/ToastNotificationsService.Samples.cs`.
- Adjust direct package references in `FileSorter9000/FileSorter9000.csproj` only as supported by the research task.

## Steps
1. Apply approved package updates/removals and namespace/API changes.
2. Preserve observable properties, commands, first-run preference behavior, and toast activation/content behavior where supported.
3. Build/restore the app after this focused change and record any remaining platform blockers.

**Done when**: These files no longer depend on incompatible/obsolete Toolkit APIs and the project advances through package restore/build as far as remaining work permits.
