# 05-windows-app: Replatform FileSorter9000 and its UI tests

Migrate the UWP application to a Windows App SDK/WinUI project targeting .NET 10 for Windows. Replace UWP-only packages and APIs identified by the assessment, preserve application behavior and packaging/activation expectations, and update `FileSorter9000.Tests.WinAppDriver` to a supported test target and current test dependencies. This is a platform migration, not only a TFM edit; validate the resulting Windows application and its automation tests on a supported Windows environment.

**Done when**: The app uses the Windows App SDK on .NET 10, incompatible UWP packages/APIs have supported replacements or tracked resolution tasks, the app builds and launches, and applicable WinAppDriver tests pass.
