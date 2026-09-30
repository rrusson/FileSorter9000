# 05.02.01-api-replacement-research: Research supported WinUI replacements and migration decisions

## Objective
Research and document compatible Windows App SDK/WinUI alternatives for the app's incompatible Toolkit UWP and UWP-only APIs before implementation.

## Scope
- Project: `FileSorter9000/FileSorter9000.csproj` and app source/XAML only.
- Assessment found incompatible Microsoft.Toolkit.Uwp, UI.Animations, UI.Controls, and Microsoft.Xaml.Behaviors.Uwp.Managed; Microsoft.UI.Xaml is included via Windows App SDK.
- Confirmed surfaces: CommunityToolkit MVVM types, Toolkit TreeView, XAML Interactivity behaviors, connected animations, toolkit helper and toast APIs, UWP app activation/background tasks, Windows.UI.Xaml namespace usage, and removed WinRT interop extensions.

## Steps
1. Check current package compatibility and documented successor/migration guidance for each used package/type group.
2. Map app usages to native WinUI/Windows App SDK equivalents or identify unsupported capabilities.
3. Record package version decisions, code/XAML migration mapping, behavioral risks, and any hard blockers in this task file.

**Done when**: Each incompatible package and major API group has an evidence-based disposition and downstream tasks have clear boundaries. No production source edits in this research task.

## Research findings and migration decisions
- Dependency inspection confirms all package versions are directly defined in `FileSorter9000/FileSorter9000.csproj`; no Central Package Management is active. Windows App SDK 2.5.1 already provides the WinUI XAML runtime, so do not add the old `Microsoft.UI.Xaml` package.
- Current supported-package lookups for `net10.0-windows10.0.26100.0` returned `CommunityToolkit.Mvvm` 8.4.2, `Microsoft.Xaml.Behaviors.WinUI.Managed` 3.0.1, and `Microsoft.WindowsAppSDK` 2.5.1. No supported version was returned for the umbrella `CommunityToolkit.WinUI`, `CommunityToolkit.WinUI.UI.Controls`, `CommunityToolkit.WinUI.UI.Controls.TreeView`, or `Microsoft.Windows.AppNotifications` package names. Use the modular MVVM and Behaviors package IDs; do not invent a control package.
- The app's Toolkit `TreeView`/`TreeViewItem` should be mapped to the Windows App SDK native `Microsoft.UI.Xaml.Controls.TreeView` controls (`muxc` namespace alias can remain mapped to `Microsoft.UI.Xaml.Controls`). Verify XAML compiler support in the integration subtask.
- `Microsoft.Toolkit.Uwp.UI.Animations` connected-animation APIs are not a required app feature: remove `Connected.*` attached properties and `SetListDataItemForNextConnectedAnimation` calls if no API exists in the selected Windows App SDK. Keep regular frame navigation/selection and document the lost visual transition.
- `Microsoft.Xaml.Behaviors.Uwp.Managed` usages in `Views/ImageGalleryPage.xaml` and `Views/ShellPage.xaml` have a WinUI package candidate (`Microsoft.Xaml.Behaviors.WinUI.Managed` 3.0.1). Replace package ID and validate behavior namespaces/targets; custom `NavigationViewHeaderBehavior` stays application code.
- `Microsoft.Toolkit.Uwp` is only used for `FirstRunDisplayService`'s `SystemInformation` helper. Replace its first-run/persistence logic with a small `ApplicationData.Current.LocalSettings` helper, which is already used by app storage code, rather than adding a broad toolkit package.
- `Microsoft.Toolkit.Uwp.Notifications` is used to construct toast XML. The supported-package lookup surfaced `CommunityToolkit.WinUI.Notifications` 7.1.2, but the Windows App SDK also supplies native AppNotification APIs; do not choose solely by package name. In 05.02.02, inspect actual code and select a supported API path that preserves packaged activation if available; otherwise document the toast limitation rather than retaining an incompatible package. Toast activation is a behavior-sensitive gate.
- UWP `Windows.UI.Xaml` types must be migrated to `Microsoft.UI.Xaml` namespaces and types. Other WinRT namespaces (`Windows.Storage`, `Windows.ApplicationModel.Activation/Background`, notifications, pickers, media) are not blanket-replaced; validate each against desktop packaged Windows App SDK, and separately adapt app entry/window initialization to WinUI 3.
- Assessment API checks include `WindowsRuntimeSystemExtensions.AsTask` and `WindowsRuntimeBufferExtensions.AsBuffer`, which are not part of modern .NET. Replace with supported async overloads and `Windows.Storage.Streams.Buffer`/stream APIs as appropriate. Recheck generated `obj` issues only after clean build; generated files are not source edits.
- Four implementation groups are now sequenced: MVVM/helpers/toasts; controls/behaviors/animations; lifecycle/platform APIs; integration validation. No `// STUB:` markers exist in app sources.
