# 05.02-windows-app-api-migration: Migrate UWP APIs, packages, and XAML app code

## Objective
Replace UWP-only APIs and packages with supported Windows App SDK/WinUI alternatives, preserving app behavior.

## Scope and research
- Source includes extensive `Windows.UI.Xaml`, `Windows.ApplicationModel`, `Windows.Storage`, `Windows.System.Threading`, and notification usage; at least 50 search results in source.
- Assessment reports 4 source-incompatible API occurrences and 20 behavioral advisories; notable APIs include `WindowsRuntimeSystemExtensions`, `WindowsRuntimeBufferExtensions.AsBuffer`, timer APIs, `Uri` with ms-appx paths, and storage/runtime changes.
- Assessment flags Microsoft.Toolkit.Uwp, UI.Animations, UI.Controls and Microsoft.Xaml.Behaviors.Uwp.Managed as incompatible with no package replacement specified; use current Windows Community Toolkit/WinUI-compatible packages or native controls after package/API research.
- No `// STUB:` markers found in app or automation test source.

## Steps
1. Inventory all application source/XAML references to UWP namespaces and incompatible package surfaces.
2. Research compatible WinUI/Windows App SDK APIs and package alternatives for each needed feature, document decisions before implementation.
3. Migrate application code and XAML incrementally, preserving navigation, storage, media, notifications, background task and image behaviors where supported.
4. Build and add focused verification for migrated API behavior.

**Done when**: App code compiles against supported Windows App SDK APIs, no unresolved UWP-only dependency remains untracked, and behavior-sensitive differences are tested or documented.
