# 02.03-mp3mangler-tests: Convert Mp3ManglerTest to SDK style

## Objective
Convert `Mp3ManglerTest/Mp3ManglerTest.csproj` to SDK style without changing its .NET Framework 4.8 target, MSTest package versions, or reference to Mp3Mangler.

## Scope and research
The assessment identifies a classic MSTest library with five issues. Its two MSTest V1 packages (TestAdapter and TestFramework 1.2.1) are defined in `packages.config`; the project imports the legacy test targets and directly references `Mp3Mangler`. The assessment also notes a missing `GenerateBindingRedirectsOutputType` setting. Preserve versions and binding behavior during this format-only conversion; test-package modernization and binding redirect resolution belong to later migration work.

## Steps
1. Convert after `Mp3Mangler`, following the retrieved project topological order.
2. Call the dedicated SDK-style conversion tool for this project only.
3. Build the project directly with Visual Studio MSBuild and restore on its original `net48` TFM.
4. Verify `packages.config` is migrated/removed and run the existing MSTest suite if the installed runner supports the preserved package versions.

**Done when**: The test project is SDK-style, still targets .NET Framework 4.8, restores/builds without warnings, retains the `Mp3Mangler` project reference, and tests are discoverable/run where supported.
