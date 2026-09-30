## Detected Hints

### hint: test-project-lifecycle
- **Status**: active
- **Priority**: MUST
- **Evidence**: `Mp3ManglerTest` references `Mp3Mangler`; both that test project and the independent WinAppDriver test project are in the SDK-style conversion scope.
- **Detected**: During task 02-sdk-conversion research.

## Breakdown Decisions

### task: 02-sdk-conversion
- Broken into 3 subtasks based on hint: test-project-lifecycle; each project conversion is isolated, ordered by the topological output, and validated before the next conversion.
