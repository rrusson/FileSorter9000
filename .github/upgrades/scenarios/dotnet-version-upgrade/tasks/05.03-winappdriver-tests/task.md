# 05.03-winappdriver-tests: Migrate UI automation test project and validate Windows app

## Objective
Move the WinAppDriver test project to an appropriate supported test target, update dependencies, and validate automation against the migrated app when the environment supports it.

## Confirmed research before implementation
- Project is SDK-style but targets `r`net48`, with three PackageReferences directly in the project and no CPM. There is no project reference to the Windows app.
- Assessment confirms net10.0 target, MSTest deprecation, API behavioral advisories, and a GenerateBindingRedirectsOutputType suggestion. Supported version lookup returned MSTest.TestAdapter 4.4.1, MSTest.TestFramework 4.4.1, and Appium.WebDriver 9.0.0 for the Windows-qualified .NET 10 target.
- Source currently uses `WindowsDriver<WindowsElement>`, `AppiumOptions.AddAdditionalCapability`, `WindowsApplicationDriverUrl`, and the legacy package family ID. Appium 9 is a major-version change; build is required to verify these APIs remain available. Do not silently alter the app identity without the WinUI package manifest being finalized.
- `Properties/AssemblyInfo.cs` contains assembly metadata and the project sets `GenerateAssemblyInfo=false`, so this setting must remain unless legacy metadata is otherwise removed.
- UI automation requires WinAppDriver at `127.0.0.1:4723` and an installed/launchable packaged app. Those runtime prerequisites may prevent execution even if the test project builds.

## Scope and research
- `FileSorter9000.Tests.WinAppDriver` is SDK-style but still targets `r`net48`; source is `BasicTests.cs` and legacy assembly metadata at `Properties/AssemblyInfo.cs`.
- Direct dependencies are defined in the project (no CPM): Appium.WebDriver 4.3.1; MSTest.TestAdapter and MSTest.TestFramework 2.2.4.
- Assessment recommends net10.0, MSTest adapter/framework 4.4.1, and flags the MSTest packages as deprecated. Current supported-package lookup for the proposed Windows-qualified target returned Appium.WebDriver 9.0.0; it must be checked for API compatibility because it is a major update from 4.3.1.
- `BasicTests.cs` creates a WindowsDriver<WindowsElement>, sets `AppiumOptions` capabilities, connects to `http://127.0.0.1:4723`, and launches a packaged app by package family name. Real test execution requires Windows App Driver plus the installed/launchable app.
- The code writes a screenshot under `%SystemDrive%\Temp\Screenshots\...`; test-runner compatibility and directory permissions should be checked. Assessment flags TimeSpan/Uri behavioral usages and binding redirect generation for the library test host.
- Tests are UI automation and do not reference the app project directly; the app migration is separately represented in the Windows app phase.

## Resolved test infrastructure and current runtime status
- Isolated package restore confirmed `Appium.WebDriver` 9.0.0 is available from nuget.org; the previous mapping restriction was a user-level NuGet source allowlist issue, not a package incompatibility. Existing user NuGet configuration was not changed.
- Upgraded Appium to 9.0.0 and adapted `BasicTests.cs` to the non-generic `WindowsDriver`, typed `App`/`DeviceName` options, and current screenshot API. Added Microsoft.NET.Test.Sdk 18.10.1 and enabled local package copies; this supplies `Appium.Net.dll` and the testhost infrastructure.
- Replaced incorrect earlier discovery/test notes: `dotnet test --list-tests` now discovers `TakeScreenshotOfLaunchPage`, and the testhost starts successfully. Test execution reaches the session creation step but fails because no service accepts connections at `127.0.0.1:4723`; launch WinAppDriver and install the packaged app to finish runtime validation.

## Steps
1. Read and assess the existing test code and runtime prerequisites.
2. Retarget the already SDK-style test project to .NET 10 and update MSTest packages. Appium remains 4.3.1 until source mapping permits a validated Appium 9 upgrade.
3. Build, discover/run tests; if external driver/app are unavailable, report that limitation precisely.

**Done when**: Test project builds with updated test dependencies; automation test discovery is confirmed and the testhost/runtime blocker is documented accurately.
