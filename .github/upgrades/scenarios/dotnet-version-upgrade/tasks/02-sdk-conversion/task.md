# 02-sdk-conversion: Convert supported legacy project files

Convert the legacy .NET Framework console and test projects to SDK-style while keeping their current target frameworks. This covers `Mp3Mangler`, `Mp3ManglerTest`, and `FileSorter9000.Tests.WinAppDriver`; the UWP project is excluded because its project-system conversion is part of its later Windows App SDK migration. Preserve project configuration and test discovery, and modernize `packages.config` dependencies to `PackageReference` where conversion requires it.

**Done when**: The three listed projects use supported SDK-style project files on their current TFMs, restore successfully, and their existing tests/builds are validated where the current toolchain permits.
