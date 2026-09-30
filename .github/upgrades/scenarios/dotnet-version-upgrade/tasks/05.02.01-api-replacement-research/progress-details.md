# Progress Details — 05.02.01 API replacement research

## Research completed
- Queried project assessment and package dependencies. The app has 33 assessed issues including four incompatible package entries and source/behavioral API items; all package versions are project-local, not centrally managed.
- Verified compatible package candidates with the supported-version tool: `CommunityToolkit.Mvvm` 8.4.2 and `Microsoft.Xaml.Behaviors.WinUI.Managed` 3.0.1; Windows App SDK remains 2.5.1. No supported version was found for queried `CommunityToolkit.WinUI` umbrella/control package names.
- Documented migration decisions for MVVM, toolkit helpers, toast notifications, native WinUI TreeView, XAML behaviors, connected animations, UWP XAML namespace migration, activation and other WinRT APIs, and removed WinRT interop extensions in `task.md`.
- No production source files changed in this research-only subtask.

## Validation
- Confirmed no `// STUB:` markers in the app C# sources.
- Build/test validation is not applicable to this research-only task; package/API implementation and validation are downstream subtasks.

## Risks
- Windows App SDK desktop packaged activation/background tasks and toast activation require code changes and Windows-specific runtime validation. Connected animation attached properties may have no direct WinUI equivalent; the documented fallback is to remove the transition while preserving ordinary navigation.
