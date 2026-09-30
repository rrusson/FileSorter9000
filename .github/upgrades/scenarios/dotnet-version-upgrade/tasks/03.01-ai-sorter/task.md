# 03.01-ai-sorter: Upgrade AiSorter and assess OpenAI API transition

## Objective
Upgrade the independent `AiSorter` library from `netstandard2.0` to `net10.0`, preserve its ML.NET behavior, and address or explicitly defer the legacy OpenAI package/API issue according to the saved unsupported-package decision.

## Research confirmed
- `AiSorter.csproj` targets `netstandard2.0` and references Microsoft.ML 1.6.0 and OpenAI 1.2.0.
- Assessment reports one optional deprecated OpenAI package issue, recommending OpenAI 2.14.0; the code in `OpenAiPathPredictor.cs` uses the prior `OpenAI_API` API and `Engine.Davinci`.
- Files include `IPathPredictor.cs`, `MLModelMusicFiling.consumption.cs`, `MLModelMusicFiling.training.cs`, `OpenAiPathPredictor.cs`, and model assets. The model zip is copied to output.
- `get_supported_package_version` confirms OpenAI 2.14.0 is supported for `net10.0`. Repository search found no active consumers of `OpenAiPathPredictor`; Core's former integration is commented out. The 1.2.0 API uses legacy completion models that are no longer a dependable service path, so this task will update the direct dependency and migrate the unused implementation to the official chat API rather than carrying forward a known obsolete path. The implementation will use `gpt-4o-mini` and read `OPENAI_API_KEY` from the environment, avoiding embedded credentials. This is an intentional endpoint/model change and needs documenting for future activation.

## Steps
1. Decide based on compatibility and supported package availability whether to defer the OpenAI package or update it together with its API usage; document rationale.
2. Update TFM and required package references while preserving model asset behavior.
3. Build the project and address blocking compile issues; document warnings and any deferred incompatibility.

**Done when**: AiSorter targets `net10.0`, builds, and any OpenAI migration/deferment is recorded accurately.
