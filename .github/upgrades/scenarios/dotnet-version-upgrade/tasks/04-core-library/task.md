# 04-core-library: Upgrade FileSorter9000.Core

Migrate `FileSorter9000.Core` from .NET Standard 2.1 to .NET 10 after the shared MP3 library is available. Replace its project reference to the .NET Framework executable with a reference to the extracted library, update package versions (including Microsoft.Identity.Client, Newtonsoft.Json, and System.Configuration.ConfigurationManager as appropriate), and resolve the assessed source/API changes. Retain a temporary `netstandard2.1` target only if needed to keep the UWP consumer buildable until its replatforming task completes.

**Done when**: Core builds on .NET 10 against the shared MP3 library, its tests or consuming project checks pass, and any temporary compatibility target is explicitly tracked for removal.
