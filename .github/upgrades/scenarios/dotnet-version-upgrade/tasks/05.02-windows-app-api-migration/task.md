# 05.02-windows-app-api-migration: Migrate UWP APIs, packages, and XAML app code

## Objective
Replace UWP-only APIs and packages with supported Windows App SDK/WinUI alternatives, preserving app behavior.

## Scope and research
- Source includes extensive `Windows.UI.Xaml`, `Windows.ApplicationModel`, `Windows.Storage`, `Windows.System.Threading`, and notification usage; at least 50 search results in source.
- Assessment reports 4 source-incompatible API occurrences and 20 behavioral advisories; notable APIs include `WindowsRuntimeSystemExtensions`, `WindowsRuntimeBufferExtensions.AsBuffer`, timer APIs, `Uri` with ms-appx paths, and storage/runtime changes.
- Assessment flags Microsoft.Toolkit.Uwp, UI.Animations, UI.Controls and Microsoft.Xaml.Behaviors.Uwp.Managed as incompatible with no package replacement specified; use current Windows Community Toolkit/WinUI-compatible packages or native controls after package/API research.
- No `// STUB:` markers found in app or automation test source.

## Confirmed research and scope inventory
- **Project affected**: `FileSorter9000/FileSorter9000.csproj` plus its app code/XAML; it references `FileSorter9000.Core` (already targets net10.0 and netstandard2.0), so project dependency order is satisfied. UI automation tests remain a separate 05.03 task.
- **Assessment**: 33 total issues (7 mandatory, 26 potential): 4 incompatible package references, 1 framework package to replace, 1 package whose functionality is included with Windows App SDK, 4 source-incompatible API occurrences, and 20 behavioral advisories. Package incompatibilities are Toolkit UWP, UI.Animations, UI.Controls, and Microsoft.Xaml.Behaviors.Uwp.Managed. `Microsoft.UI.Xaml` is superseded by Windows App SDK. Assessment identifies removed WinRT interop (`WindowsRuntimeSystemExtensions`, `AsBuffer`), timer and `ms-appx` URI behavioral items.
- **Current restore/package evidence**: `Microsoft.Toolkit.Uwp` and `Microsoft.Toolkit.Uwp.UI.Animations` 7.0.2 cause NU1202 under net10.0-windows10.0.26100.0. `Microsoft.Toolkit.Mvvm` 7.1.2 and `Microsoft.Toolkit.Uwp.Notifications` 7.0.2 restore but are legacy-named dependencies that require support/API validation. Win2D is 1.4.0 and Windows App SDK is 2.5.1. Packages are directly referenced in the project (no CPM).
- **Source feature groups confirmed**:
  - MVVM package namespaces occur across `ViewModels/*.cs` and `Behaviors/TreeViewCollapseBehavior.cs`.
  - UWP-only animation usage is in `Views/ImageGalleryPage.xaml`, `Views/ImageGalleryDetailPage.xaml`, `Views/ImageGalleryDetailPage.xaml.cs`, and `ViewModels/ImageGalleryViewModel.cs`; connected-animation continuation is used in navigation.
  - XAML behaviors occur in `Views/ImageGalleryPage.xaml` and `Views/ShellPage.xaml`; custom header behavior is defined in `Behaviors/NavigationViewHeaderBehavior.cs` and used by `Views/MediaPlayerPage.xaml`.
  - Toolkit UWP controls expose `TreeView`/`TreeViewItem` in `Views/TreeViewPage.xaml`.
  - Toolkit helpers are used in `Services/FirstRunDisplayService.cs`; toast builders/activation are in `Services/ToastNotificationsService.cs` and `.Samples.cs`.
  - App startup and activation use UWP types in `App.xaml.cs`, `Services/ActivationService.cs`, `Services/BackgroundTaskService.cs`, and `Activation/*.cs`; UWP APIs also appear in `BackgroundTasks/*.cs`, `Helpers/*.cs`, `Services/*.cs`, and many Page/ViewModel code-behind files.
- **Decomposition decision**: Scope spans package/API decisions and distinct MVVM, XAML behavior/control, and app/platform lifecycle/service concerns. Split into research, focused implementation groups, and a final integration validation gate; child tasks will be sequenced so package/API choices are recorded before edits.
- **Stubs**: `// STUB:` grep found no markers in the app C# sources.

## Steps
1. Inventory all application source/XAML references to UWP namespaces and incompatible package surfaces.
2. Research compatible WinUI/Windows App SDK APIs and package alternatives for each needed feature, document decisions before implementation.
3. Migrate application code and XAML incrementally, preserving navigation, storage, media, notifications, background task and image behaviors where supported.
4. Build and add focused verification for migrated API behavior.

**Done when**: App code compiles against supported Windows App SDK APIs, no unresolved UWP-only dependency remains untracked, and behavior-sensitive differences are tested or documented.
