# File Sorter 9000
This utility is a work in progress to automatically sort (and optionally move) all kinds of files using machine learning and some manual heuristics.
The big idea is to automate away some of the druggery of manually organizing files on your PC.
_____

Initially I'm limiting the utility to .MP3 files as a proof-of-concept. Later other file types will be added.

**Current Screens:**
1. File Folder Picker - Select folder with examples of how you file various file types, folder with source to be sorted, and optionally a folder to contain the new paths and moved files
2. A media player to preview media files (to verify contents before moving files)
3. An displayer gallery to preview image files (to verify contents before moving files)
4. A file tree to select/omit moving files to their sorted paths
5. A settings screen for customization


**TODO: Expand system to sort any file type **
**TODO: Clean-up unused code and move more functionality out of UWP project to FileSorter9000.Core **
**TODO: Put all long-running processes in background, with progress estimate visible in UI **
**TODO: Train ML functions for various file types **
**TODO: Create a feedback system where rejected path suggestions are used to improve ML model training **
**TODO: Wire media player and image gallery to automatically play/show select file/folder (only demo material currently) **
**TODO: Add dependency injection for various service implementations **
**TODO: Add more asynchronous plumbing **

