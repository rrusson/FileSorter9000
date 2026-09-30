# 06-consolidation-validation: Remove migration bridges and validate the solution

After all consumers have migrated, remove temporary legacy TFMs from Core and the shared MP3 library so every intended project targets .NET 10 (using the Windows-qualified TFM for the Windows app). Finish package replacement/resolution work, review and remove legacy binding redirects according to the confirmed selection, restore and build the full solution, and run all available tests. Record central package management as a post-migration recommendation rather than introducing it during the active migration.

**Done when**: All solution projects target .NET 10, no temporary compatibility TFMs or unresolved package/API stubs remain, the solution builds without warnings or errors, all available tests pass, and deferred package/CPM recommendations are documented.

## Research Findings
- Active solution contains eight projects (excluding `MigrationBackup` copies). Seven already target .NET 10 variants; `FileSorter9000.Core` alone multi-targets `r`net10.0;netstandard2.0`, while `Mp3Mangler.Shared` is `r`netstandard2.0`.
- User's workspace context identifies .NET Standard 2.0 as an intentional active target. However the approved upgrade plan explicitly marks the Core/shared library compatibility TFMs as temporary migration bridges to remove during this consolidation task. Validate dependency ownership before dropping them.
- `FileSorter9000.Core` references `Mp3Mangler.Shared`; checked topological order confirms the shared library precedes Core and the Windows app. No other direct project reference to the shared project was found in dependency metadata.
- Core direct packages: Microsoft.Identity.Client 4.90.1, Newtonsoft.Json 13.0.4, System.Configuration.ConfigurationManager 10.0.12. Shared direct packages: CsvHelper 27.2.1, taglib-sharp-netstandard2.0 2.1.0. Both use per-project package versions (no CPM).
- Project files are SDK-style; the WinUI app is `r`net10.0-windows10.0.26100.0` and must be validated with VS MSBuild x64. The WinAppDriver test project now targets `r`net10.0`, uses MSTest 4.4.1, Microsoft.NET.Test.Sdk 18.10.1, and Appium 9.0.0.
- Prior Windows app build succeeds but reports 358 warnings, principally CA1416 plus NU1903/NU1904/NETSDK1206. No warning suppression is authorized. UI test code builds and discovery works; runtime requires WinAppDriver at 127.0.0.1:4723 and an installed packaged app.

## Consolidation approach
- Drop `r`netstandard2.0` from `FileSorter9000.Core` and retarget `Mp3Mangler.Shared` to `r`net10.0` only after validating their packages/API build.
- Audit project TFMs, project references, binding redirect artifacts, package issues, and source stubs; then build solution with VS MSBuild and run available tests.
- Record warnings and environment-dependent UI test/package install limitations honestly. Central package management remains a recommendation, not an active change.
