# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: `net10.0` (.NET 10 LTS)
- **Scope**: Upgrade all projects and dependencies in the solution, starting with `FileSorter9000.Core`.
- **Line endings**: Always enforce Windows CRLF for every repository text file, including code, configuration, documentation, and workflow artifacts; repository `.gitattributes` must enforce CRLF.

## Source Control
- **Source Branch**: `master`
- **Working Branch**: `upgrade-projects`
- **Pending Changes**: Committed as `291c2c2` before creating the working branch.
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)
- **Warning handling**: Do not let non-blocking warnings stall progress; record them accurately. Do not suppress warnings.

## Key Decisions Log
- Accepted the default initialization settings, including committing the detected pending changes before starting the upgrade.
- Confirmed Bottom-Up strategy, in-place migration for legacy class libraries, per-project package management during migration, deferred resolution of unsupported packages, inline API fixes, and review of binding redirects.
- Chose to extract reusable MP3 functionality into a library while preserving the Mp3Mangler command-line executable; FileSorter9000.Core will depend on the library.
- Confirmed `master` as the source branch for sync; authorized discarding the uncommitted `Mp3Mangler/Program.cs` edit as nonessential.

## Upgrade Options

### Strategy
- Upgrade Strategy: Bottom-Up

### Project Structure
- Project Approach: Class Libraries — In-place
- Package Management: Per-Project (defer CPM to post-migration)

### Compatibility
- Unsupported Packages: Defer Resolution (10 incompatible packages reported)
- Unsupported API Handling: Fix Inline

### Modernization
- Assembly Binding Redirects: Document and Review Before Removing

## Strategy
**Selected**: Bottom-Up (Dependency-First)
**Rationale**: The solution contains .NET Framework 4.8/4.8.1 projects and a three-level dependency graph, so dependency-ordered tasks allow tier-level validation.

### Execution Constraints
- Complete and validate each dependency tier before proceeding to the next.
- Convert supported legacy project formats separately from target-framework changes; handle the UWP-to-Windows-App-SDK project conversion in its own migration task.
- Keep tests with the project they validate, or immediately after it, and run them at each tier boundary.
- Apply direct package replacements, defer unresolved incompatible packages to follow-up work, and fix API changes inline.
- After migration stabilizes, include central package management as a deferred recommendation and run full-solution build and tests.

## User Preferences
### Execution Style
- Always use Windows CRLF line endings in all repository and workflow files. User works on Windows and does not use Linux.
