# 05.02.03-winui-controls-behaviors-animations: Migrate Toolkit controls, XAML behaviors, and connected animations

## Objective
Replace Toolkit controls, Microsoft.Xaml.Interactivity usage, and connected animation APIs with supported WinUI-native or researched alternatives.

## Scope
- `Views/TreeViewPage.xaml` (TreeView controls), `Views/ImageGalleryPage.xaml`, `Views/ImageGalleryDetailPage.xaml`, `Views/ImageGalleryDetailPage.xaml.cs`, `ViewModels/ImageGalleryViewModel.cs`, `Views/ShellPage.xaml`, `Views/MediaPlayerPage.xaml`, `Behaviors/NavigationViewHeaderBehavior.cs`, and related behavior implementation.
- Toolkit packages `Microsoft.Toolkit.Uwp`, `Microsoft.Toolkit.Uwp.UI.Controls`, `Microsoft.Toolkit.Uwp.UI.Animations`, and `Microsoft.Xaml.Behaviors.Uwp.Managed`, subject to 05.02.01 decisions.

## Confirmed task research
- App project is `FileSorter9000/FileSorter9000.csproj`, targets `r`net10.0-windows10.0.26100.0`, and still directly references Microsoft.Toolkit.Uwp 7.0.2, UI.Animations 7.0.2, and UI.Controls 7.0.2. The earlier full-solution restore failed with NU1202 specifically for Microsoft.Toolkit.Uwp and UI.Animations.
- Toolkit animation use is confined to `Views/ImageGalleryPage.xaml`, `Views/ImageGalleryDetailPage.xaml`, `Views/ImageGalleryDetailPage.xaml.cs`, and `ViewModels/ImageGalleryViewModel.cs`. It marks connected elements and sets the next connected-animation item during gallery navigation. Removing this will retain gallery navigation but intentionally drop the connected zoom transition.
- XAML interaction behaviors occur in `Views/ShellPage.xaml` (Loaded, NavigationView header, ItemInvoked) and `Views/ImageGalleryPage.xaml` (GridView ItemClick). These can be replaced by native event handlers; the header helper needs explicit attachment rather than Behavior<T>.
- `Behaviors/TreeViewCollapseBehavior.cs` and `NavigationViewHeaderBehavior.cs` derive from `Microsoft.Xaml.Interactivity.Behavior<T>`. No XAML usage of the collapse behavior was found; the header behavior is used only by ShellPage and its attached HeaderMode is set by MediaPlayerPage.
- TreeView itself is already written using `Microsoft.UI.Xaml.Controls.TreeView`; template selector creates TreeViewItems. `Microsoft.Toolkit.Uwp.UI.Controls` is the package providing this API in the current code. Must verify supported WinUI TreeView availability before removing that package.
- Build validation must use Visual Studio MSBuild for the WinUI/XAML app. Current restore blocks compilation until incompatible UWP packages are removed/replaced.

## Steps
1. Apply supported control and behavior replacements; remove connected-animation metadata/code if no supported equivalent is available and document the behavior change.
2. Preserve navigation, page header updates, TreeView interaction, and gallery selection.
3. Restore/build with Windows App SDK tooling and record remaining failures.

**Done when**: No XAML or source usage remains for removed package surfaces in this scope, and behavior changes are tested or explicitly documented.

## Execution findings
- Gallery item click and Shell navigation events now use native WinUI event handlers. The gallery continues navigating to detail pages and no longer requests connected animation; this intentionally drops the shared-element zoom transition.
- Navigation header configuration now uses WinUI attached properties for per-page HeaderMode/HeaderContext/HeaderTemplate, with shell event handling. Shell loaded/item-invoked commands are wired via native events.
- Removed references to Microsoft.Toolkit.Uwp and UWP Toolkit UI.Animations/UI.Controls packages; Microsoft.Toolkit.Uwp.Notifications remains for the separate notification migration. TreeView now relies on the WinUI controls namespace.
- Converted application C# XAML `Windows.UI.Xaml` imports to `Microsoft.UI.Xaml` where found; this unblocked the main WinUI project compiler enough to reveal follow-up lifecycle errors. Replaced the first-run `SystemInformation` helper with LocalSettings tracking and attached dialog XamlRoot.
- Build tool is Visual Studio 2026 MSBuild at `C:\Program Files\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin\MSBuild.exe`, with `/p:Platform=x64` for Win2D. It now completes with zero errors and emits the app assembly.
- Most recent build still reports 360 CA1416 platform-compatibility warnings, NETSDK1206 for SQLitePCLRaw's alpine-x64 RID, NU1903 for SQLitePCLRaw.lib.e_sqlite3 2.0.2, and NU1904 for System.Drawing.Common 4.7.0. No warnings were suppressed. The app project targets Windows, so the CA1416 warnings are platform-analysis noise that needs deliberate cleanup/annotation work rather than suppression.
- UI runtime behavior has not been verified. Prior WinAppDriver testhost execution also aborts before running tests because the Appium dependency assembly is missing.
