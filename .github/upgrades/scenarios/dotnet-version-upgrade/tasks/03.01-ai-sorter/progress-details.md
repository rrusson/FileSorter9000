# Progress: 03.01-ai-sorter

## Changes
- Retargeted `AiSorter/AiSorter.csproj` from `netstandard2.0` to `net10.0`.
- Updated OpenAI from 1.2.0 to 2.14.0, confirmed supported for `net10.0`.
- Migrated the dormant `OpenAiPathPredictor` implementation to the new `OpenAI.Chat.ChatClient` API using `gpt-4o-mini` and `OPENAI_API_KEY` from the environment; no API key is embedded. The only in-repository integration point was commented out.
- Preserved Microsoft.ML 1.6.0 and the model zip's output-copy configuration.

## Validation
- Restore succeeded using an isolated temporary NuGet config because the user-level package source mapping did not resolve OpenAI 2.14.0. No global NuGet settings were changed.
- `dotnet build .\AiSorter\AiSorter.csproj --no-restore` succeeded and produced `AiSorter\bin\Debug\net10.0\AiSorter.dll`.
- One non-blocking NU1903 warning remains: transitive Newtonsoft.Json 10.0.3 via Microsoft.ML 1.6.0 is reported with a high-severity vulnerability advisory (GHSA-5crp-9r3c-p9vr). Package update/dependency resolution should be considered in a later package-focused task; the warning was not suppressed.
- No live OpenAI request was made; runtime behavior requires valid `OPENAI_API_KEY` and network access.
