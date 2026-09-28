# Progress: 04-core-library

## Changes
- Updated `FileSorter9000.Core.csproj` to target `net10.0` plus temporary `netstandard2.0`, preserving compatibility for the current UWP consumer until its planned Windows App SDK replatform.
- Updated `Microsoft.Identity.Client` to 4.90.1, `Newtonsoft.Json` to 13.0.4, and `System.Configuration.ConfigurationManager` to 10.0.12.
- Retained the `Mp3Mangler.Shared` project reference. No source API edits were required for the Core build.

## Validation
- Restored and built Core with an isolated temporary NuGet source configuration. Both `net10.0` and `netstandard2.0` targets succeeded.
- Rebuilt and ran `Mp3ManglerTest` after Core dependency changes: 1 passed, 0 failed, 0 skipped.
- Full-solution build attempted after restore. It failed at the legacy UWP `FileSorter9000.csproj` import with MSB4019 because `Microsoft.Windows.UI.Xaml.CSharp.targets` is unavailable in the installed .NET SDK/VS 2026 Insiders tool tree. A search of the installed VS tree found no corresponding UWP target. This is the anticipated UWP replatform boundary in task 05, rather than a Core compilation error.

## Warnings / deferred items
- Core reports CS0618 for MSAL `IPublicClientApplication.AcquireTokenByIntegratedWindowsAuth(IEnumerable<string>)`, which the updated MSAL package marks obsolete and recommends replacing with WAM for OS-account SSO.
- Core reports existing CS0067 warnings for the never-used `FakeIdentityService.LoggedIn` and `LoggedOut` events. These warnings were not suppressed or changed.
- Assessment notes `HttpContent` behavior advisories in `MicrosoftGraphService`; no failing behavior test or compile issue surfaced in this task.
- Remove the temporary `netstandard2.0` compatibility target once the UWP application is replatformed and no longer consumes it.
