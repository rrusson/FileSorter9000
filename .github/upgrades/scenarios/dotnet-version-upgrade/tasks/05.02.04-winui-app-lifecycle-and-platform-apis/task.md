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
