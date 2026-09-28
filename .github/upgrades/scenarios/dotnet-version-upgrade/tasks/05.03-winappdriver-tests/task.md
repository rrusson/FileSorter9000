# 05.03-winappdriver-tests: Migrate UI automation test project and validate Windows app

## Objective
Move the WinAppDriver test project to an appropriate supported test target, update dependencies, and validate automation against the migrated app when the environment supports it.

## Scope and research
- `FileSorter9000.Tests.WinAppDriver` is legacy net481 class library, references Appium.WebDriver 4.3.1 and MSTest adapter/framework 2.2.4; only two project files are present (BasicTests.cs and AssemblyInfo.cs).
- Assessment recommends MSTest adapter/framework 4.4.1; Appium 4.3.1 and Selenium transitive dependencies are currently marked compatible, but runtime interoperability requires validation.
- Assessment API findings in BasicTests.cs concern `TimeSpan.FromSeconds` and `Uri` behavior; no compile-specific code issue yet confirmed.
- App automation requires a running Windows app, Windows App Driver service, and supported Windows desktop environment; availability must be checked before claiming runtime test completion.

## Steps
1. Read and assess the existing test code and runtime prerequisites.
2. Convert the project to SDK style and upgrade test dependencies/TFM where compatible with Appium/WinAppDriver.
3. Build and discover/run tests; if external driver/app are unavailable, report that limitation precisely.

**Done when**: Test project builds with updated dependencies; applicable automation tests pass or their environment-specific blocker is documented.
