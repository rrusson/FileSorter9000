# 05.02.04-winui-app-lifecycle-and-platform-apis: Migrate app lifecycle and UWP-specific API usage

## Objective
Adapt app startup, activation, background tasks, WinRT interop, and other UWP API usages to supported packaged WinUI/Windows App SDK behavior.

## Scope
- App entry/activation: `App.xaml.cs`, `Services/ActivationService.cs`, `Services/BackgroundTaskService.cs`, `Activation/*.cs`.
- Platform APIs in `BackgroundTasks/*.cs`, `Helpers/*.cs`, `Services/*.cs`, and remaining `Views/*.cs` including `Windows.UI.Xaml` type namespaces.
- Resolve assessment items including `WindowsRuntimeSystemExtensions`, `AsBuffer`, timer API behavior, and ms-appx URI semantics as applicable.

## Steps
1. Apply researched API/namespace replacements, accounting for WinUI app-window lifecycle and packaged activation.
2. Preserve storage, picker, media, background-task, and resource behavior where supported; document unsupported or environment-dependent paths.
3. Build the app and run focused tests or available smoke checks.

**Done when**: Production app sources compile against supported APIs with lifecycle and behavioral gaps explicitly recorded.

## Research Findings
- Confirmed the app project targets `net10.0-windows10.0.26100.0` and references Windows App SDK 2.5.1; validation must use Visual Studio MSBuild x64 because the project is WinUI/XAML.
- `Window.Current` remained in `Services/ActivationService.cs`, `Services/NavigationService.cs`, `Services/FirstRunDisplayService.cs`, and `Views/FirstRunDialog.xaml.cs`; WinUI 3 requires an owned `Microsoft.UI.Xaml.Window` reference.
- `Services/ThemeSelectorService.cs` still iterated UWP `CoreApplication.Views` using `CoreDispatcher`; `Views/MediaPlayerPage.xaml.cs` still used `Windows.UI.Core.CoreDispatcher`. `Helpers/ImageHelper.cs` used the assessment-flagged `byte[].AsBuffer()` extension.
- `BackgroundTasks/BackgroundTask1.cs` and `BackgroundTaskAnalyzeFiles.cs` use `ThreadPoolTimer`; `Helpers/SettingsStorageExtensions.cs` uses WinRT `.AsTask()`; storage/assets use `ms-appx` URIs. These are retained pending compile/API evidence because they remain supported WinRT surfaces.
- UWP toast notifications remain in `Services/ToastNotificationsService.cs` and its sample; they are package-backed Windows notification APIs, not XAML namespace usages. Background task registration remains Windows-specific and requires packaged-app runtime validation.
- Baseline app build (VS MSBuild, Debug x64) succeeded before this task with 0 errors but 362 warnings, mainly CA1416 plus NU1903/NU1904 and NETSDK1206. No blanket warning suppression will be added.

## Execution Notes
- Use a shared explicit main `Window` owned by `ActivationService` for app activation, navigation, theming and dialogs.
- Replace CoreDispatcher dispatch with WinUI `DispatcherQueue` and remove `AsBuffer` by writing bytes through `DataWriter`.
- Validate with VS MSBuild x64; record that actual packaged launch/background-trigger behavior depends on a Windows runtime and is not exercised by compilation alone.
