# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade all seven solution projects and dependencies to .NET 10 LTS, including replatforming the UWP application to Windows App SDK/WinUI.
**Scope**: Seven assessed projects across .NET Standard 2.1, .NET 5 UWP, .NET 8, and .NET Framework 4.8/4.8.1; one three-level dependency graph; one new shared MP3 library to resolve the existing executable-to-library dependency boundary.

### Selected Strategy
**Bottom-Up (Dependency-First)** — Upgrade from leaf projects through their dependents, validating each tier.
**Rationale**: The solution includes .NET Framework projects and a three-level dependency graph; those migration mechanics require dependency-ordered work.

**Dependency graph from assessment**:
```
Level 2: FileSorter9000 (UWP)
			 ↓
Level 1: FileSorter9000.Core      Mp3ManglerTest
			 ↓                       ↓
Level 0: AiSorter  FileSorter9000.Tests.WinAppDriver  MLModelMusicFiling_ConsoleApp1  Mp3Mangler
```
`Mp3ManglerTest` follows the project it tests; WinAppDriver automation is paired with the UWP app it exercises even though it has no project-reference edge in the assessment graph. The `Mp3Mangler` executable will be preserved while reusable processing code is extracted into a library.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | Bottom-Up | .NET Framework 4.8/4.8.1 projects and a three-level dependency graph benefit from tier-level validation. |
| Project Approach — Class Libraries | In-place | Legacy test libraries can migrate with the projects they test; no ASP.NET Framework web projects were identified. |
| Package Management | Per-Project (defer CPM to post-migration) | Old-style project files and the Framework-to-modern-.NET transition make CPM disruptive before the migration stabilizes. |
| Unsupported Packages | Defer Resolution | The assessment reports 10 incompatible packages, and Bottom-Up favors resolving them after affected tiers build; identified direct replacements are still applied. |
| Unsupported API Handling | Fix Inline | Five source-incompatible APIs were flagged, and no evidence indicated the many complex cross-project changes needed to favor deferral. |
| Assembly Binding Redirects | Document and Review Before Removing | Assessment findings include conflicting manual redirects and redirects that force version downgrades. |

## Tasks

### 01-prerequisites: Verify the .NET 10 toolchain

Confirm that the .NET 10 SDK and required Windows/UWP migration workloads are available, and that repository SDK selection does not pin an incompatible SDK. The assessment spans SDK-style and legacy project systems, so the applicable build tools must be established before project-file changes begin.

**Done when**: The .NET 10 SDK and required Windows tooling are available, and any SDK-selection constraint is understood and addressed.

### 02-sdk-conversion: Convert supported legacy project files

Convert the legacy .NET Framework console and test projects to SDK-style while keeping their current target frameworks. This covers `Mp3Mangler`, `Mp3ManglerTest`, and `FileSorter9000.Tests.WinAppDriver`; the UWP project is excluded because its project-system conversion is part of its later Windows App SDK migration. Preserve project configuration and test discovery, and modernize `packages.config` dependencies to `PackageReference` where conversion requires it.

**Done when**: The three listed projects use supported SDK-style project files on their current TFMs, restore successfully, and their existing tests/builds are validated where the current toolchain permits.

### 03-foundation-projects: Upgrade independent projects and MP3 components

Upgrade the leaf projects `AiSorter` and `MLModelMusicFiling_ConsoleApp1` to .NET 10 and update their dependencies, including replacements for deprecated packages where needed. In the MP3 component group, extract reusable processing functionality from the .NET Framework `Mp3Mangler` executable into a new shared library, preserve the executable as a .NET 10 console app, and migrate `Mp3ManglerTest` with its tested component. Use a temporary compatibility target for the shared library if required so the still-unmigrated Core/UWP consumers can continue using it; remove transitional TFMs after those consumers move.

**Done when**: The independent projects and MP3 executable/tests build on .NET 10, the shared library builds for its required temporary and final consumers, and MP3 behavior is covered by passing tests.

### 04-core-library: Upgrade FileSorter9000.Core

Migrate `FileSorter9000.Core` from .NET Standard 2.1 to .NET 10 after the shared MP3 library is available. Replace its project reference to the .NET Framework executable with a reference to the extracted library, update package versions (including Microsoft.Identity.Client, Newtonsoft.Json, and System.Configuration.ConfigurationManager as appropriate), and resolve the assessed source/API changes. Retain a temporary `netstandard2.1` target only if needed to keep the UWP consumer buildable until its replatforming task completes.

**Done when**: Core builds on .NET 10 against the shared MP3 library, its tests or consuming project checks pass, and any temporary compatibility target is explicitly tracked for removal.

### 05-windows-app: Replatform FileSorter9000 and its UI tests

Migrate the UWP application to a Windows App SDK/WinUI project targeting .NET 10 for Windows. Replace UWP-only packages and APIs identified by the assessment, preserve application behavior and packaging/activation expectations, and update `FileSorter9000.Tests.WinAppDriver` to a supported test target and current test dependencies. This is a platform migration, not only a TFM edit; validate the resulting Windows application and its automation tests on a supported Windows environment.

**Done when**: The app uses the Windows App SDK on .NET 10, incompatible UWP packages/APIs have supported replacements or tracked resolution tasks, the app builds and launches, and applicable WinAppDriver tests pass.

### 06-consolidation-validation: Remove migration bridges and validate the solution

After all consumers have migrated, remove temporary legacy TFMs from Core and the shared MP3 library so every intended project targets .NET 10 (using the Windows-qualified TFM for the Windows app). Finish package replacement/resolution work, review and remove legacy binding redirects according to the confirmed selection, restore and build the full solution, and run all available tests. Record central package management as a post-migration recommendation rather than introducing it during the active migration.

**Done when**: All solution projects target .NET 10, no temporary compatibility TFMs or unresolved package/API stubs remain, the solution builds without warnings or errors, all available tests pass, and deferred package/CPM recommendations are documented.
