# Copilot Instructions

## Project Guidelines
- During the active .NET upgrade workflow, do not let non-blocking warnings stall progress; keep moving and document warnings accurately, without suppressing them.
- PLEASE, for the love of God, use Windows line endings and not that Linux crap that gives me constant warnings.
- Always use Windows CRLF line endings in source, configuration, and workflow files; do not use LF-only line endings.
- Enforce CRLF for every repository text file with `.gitattributes` (`* text=auto eol=crlf`); new and modified files must be written with CRLF before staging or committing.
