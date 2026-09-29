# 05.02.05-app-api-integration-validation: Integrate and validate the Windows app API migration

## Objective
Validate the combined package, XAML, source, and platform migration from 05.02.02–05.02.04.

## Scope
- `FileSorter9000/FileSorter9000.csproj` and all app sources/XAML; Core dependency remains a project reference.
- Use VS MSBuild because the app is a Windows App SDK XAML project.

## Steps
1. Restore and build the app with default configuration, fixing migration-related errors and all warnings in modified scope without suppression.
2. Search for unresolved legacy Toolkit/UWP XAML namespaces and package references; verify the manifest/MSIX configuration remains present.
3. Run available tests/smoke checks; document blocked package launch/signing or environment validations.
4. Write final 05.02 progress details.

**Done when**: App build and relevant tests pass, or remaining platform/environment blockers and unsupported behaviors are precisely documented; no incompatible dependency/API is left untracked.
