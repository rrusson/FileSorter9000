# 06-consolidation-validation: Remove migration bridges and validate the solution

After all consumers have migrated, remove temporary legacy TFMs from Core and the shared MP3 library so every intended project targets .NET 10 (using the Windows-qualified TFM for the Windows app). Finish package replacement/resolution work, review and remove legacy binding redirects according to the confirmed selection, restore and build the full solution, and run all available tests. Record central package management as a post-migration recommendation rather than introducing it during the active migration.

**Done when**: All solution projects target .NET 10, no temporary compatibility TFMs or unresolved package/API stubs remain, the solution builds without warnings or errors, all available tests pass, and deferred package/CPM recommendations are documented.
