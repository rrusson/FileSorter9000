# 05.03 WinAppDriver Test Project Migration — Progress

## Changes
- Updated `FileSorter9000.Tests.WinAppDriver.csproj` to target `r`net10.0` and set `IsTestProject` explicitly.
- Upgraded MSTest.TestAdapter and MSTest.TestFramework from 2.2.4 to 4.4.1.
- Kept Appium.WebDriver at 4.3.1 because Appium 9.0.0 restore was blocked by the machine-level NuGet PackageSourceMapping allowlist. Appium 4.3.1 still restores under the current configuration.
- Updated the task notes with confirmed assessment, dependencies, and validation evidence.

## Validation
- `dotnet build FileSorter9000.Tests.WinAppDriver/FileSorter9000.Tests.WinAppDriver.csproj`: succeeds, output at `bin/Debug/net10.0/FileSorter9000.Tests.WinAppDriver.dll`.
- Restore reports NU1903 for transitive Newtonsoft.Json 12.0.1 and NU1904 for transitive System.Drawing.Common 4.5.1.
- Test Explorer discovery finds `TakeScreenshotOfLaunchPage`.
- `dotnet test ... --no-build`: testhost aborts before the test runs because the dependency manifest references Appium.WebDriver 4.3.1's `lib/netstandard2.0/Appium.Net.dll`, which is not found in the testhost dependency layout. This is not evidence of a passing UI test. Appium 9 should be reconsidered after NuGet package source mapping is updated; runtime validation will additionally require WinAppDriver and an installed app package.
- `git diff --check` passed. CRLF verification passed for the modified project and task artifact.

## Deviation / open blockers
- Plain `r`net10.0` is used instead of `r`net10.0-windows...` because the test project uses no Windows SDK APIs, and the Windows-qualified TFM caused the testhost to abort on a missing `Microsoft.Windows.SDK.NET.dll` runtime-pack assembly.
- Full solution restore remains blocked by the Windows app's incompatible `Microsoft.Toolkit.Uwp` 7.0.2 and `Microsoft.Toolkit.Uwp.UI.Animations` 7.0.2 packages. Other projects also report existing package vulnerability warnings.
