# 03.02-mlmodel-console: Upgrade MLModelMusicFiling console app to .NET 10

## Objective
Upgrade `MLModelMusicFiling_ConsoleApp1` to `net10.0` and confirm model assets remain available to the console app.

## Research confirmed
- Project file currently says `net5.0`; the assessment metadata reports `net8.0`, so the project file is authoritative for editing and this discrepancy should be recorded.
- SDK-style executable with Microsoft.ML 1.6.0. Model Builder source and model zip are included, and the zip is configured to copy to output.
- Assessment flags only the TFM migration; no source API issues detected.

## Steps
1. Set the project target to `net10.0`.
2. Build and fix migration-related errors; retain asset copying.
3. Record any warnings and confirm output model presence.

**Done when**: The console app targets `net10.0` and builds with the model file copied to output.
