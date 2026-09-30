# 02.01-mp3-console: Convert Mp3Mangler to SDK style

## Objective
Convert `Mp3Mangler/Mp3Mangler.csproj` from its legacy project format to SDK style without changing its .NET Framework 4.8 (`net48`) target, package versions, or executable behavior.

## Scope and research
The actual pre-conversion project file targeted .NET Framework 4.8 (`<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>`), despite the assessment notes mentioning 4.8.1. It is a classic console executable (`OutputType` `Exe`) with manual binding redirects in `App.config`. Its 16 `packages.config` entries were: CsvHelper 27.2.1, JetBrains.Annotations 2021.3.0, Microsoft.Bcl.AsyncInterfaces 1.0.0, Microsoft.Bcl.HashCode 1.0.0, Microsoft.CSharp 4.3.0, Newtonsoft.Json 13.0.1, System.Buffers 4.4.0, System.CodeDom 4.4.0, System.Collections.Immutable 1.5.0, System.Memory 4.5.3, System.Numerics.Vectors 4.4.0, System.Reflection.Emit.Lightweight 4.3.0, System.Runtime.CompilerServices.Unsafe 4.5.3, System.Threading.Channels 4.7.1, System.Threading.Tasks.Extensions 4.5.4, and taglib 2.1.0.0. `Mp3ManglerTest` references this project, so conversion order is correct. Package version recommendations and binding redirect cleanup belong to later TFM/package tasks, not this structural conversion.

## Steps
1. Use the topological order already retrieved: this project precedes its test dependent.
2. Call the dedicated SDK-style conversion tool for this project only.
3. Build this project directly with restore, using its original TFM.
4. Verify `packages.config` was removed, package versions stayed unchanged, and the project remains an executable.

**Done when**: The project is SDK-style, still targets .NET Framework 4.8 (`net48`), restores and builds without warnings, and its package versions/configuration are preserved.
