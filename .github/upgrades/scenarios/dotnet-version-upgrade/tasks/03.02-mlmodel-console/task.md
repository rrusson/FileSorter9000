# 03.02-mlmodel-console: Upgrade MLModelMusicFiling console app to .NET 10

## Objective
Upgrade `MLModelMusicFiling_ConsoleApp1` to `net10.0` and confirm model assets remain available to the console app.

## Research confirmed
- Project file currently says `net5.0`; the assessment metadata reports `net8.0`, so the project file is authoritative for editing and this discrepancy should be recorded.
- SDK-style executable with Microsoft.ML 1.6.0. Model Builder source and model zip are included, and the zip is configured to copy to output.
- Assessment flags only the TFM migration; no source API issues detected.
- `get_project_dependencies` confirms Microsoft.ML 1.6.0 is directly declared in this project and no CPM/imported package version source was reported. `Program.cs` invokes the generated model and pauses with `Console.ReadKey()`; the model ZIP is a project `None` item marked `CopyToOutputDirectory=PreserveNewest`. Assessment's reported `net8.0` conflicts with project XML's actual `net5.0`; use the evaluated project file as truth and retarget from `net5.0`.
- No `// STUB:` markers were found in this project source.
- The .NET 10 build completed successfully. It reported two warnings, including NU1903 for transitive Newtonsoft.Json 10.0.3 (GHSA-5crp-9r3c-p9vr). The output model asset was verified at `bin/Debug/net10.0/MLModelMusicFiling.zip` (481,557 bytes). No warning was suppressed.

## Steps
1. Set the project target to `net10.0`.
2. Build and fix migration-related errors; retain asset copying.
3. Record any warnings and confirm output model presence.

**Done when**: The console app targets `net10.0` and builds with the model file copied to output.
