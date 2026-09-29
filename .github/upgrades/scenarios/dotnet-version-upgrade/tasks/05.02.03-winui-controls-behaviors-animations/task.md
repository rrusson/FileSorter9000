# 05.02.03-winui-controls-behaviors-animations: Migrate Toolkit controls, XAML behaviors, and connected animations

## Objective
Replace Toolkit controls, Microsoft.Xaml.Interactivity usage, and connected animation APIs with supported WinUI-native or researched alternatives.

## Scope
- `Views/TreeViewPage.xaml` (TreeView controls), `Views/ImageGalleryPage.xaml`, `Views/ImageGalleryDetailPage.xaml`, `Views/ImageGalleryDetailPage.xaml.cs`, `ViewModels/ImageGalleryViewModel.cs`, `Views/ShellPage.xaml`, `Views/MediaPlayerPage.xaml`, `Behaviors/NavigationViewHeaderBehavior.cs`, and related behavior implementation.
- Toolkit packages `Microsoft.Toolkit.Uwp`, `Microsoft.Toolkit.Uwp.UI.Controls`, `Microsoft.Toolkit.Uwp.UI.Animations`, and `Microsoft.Xaml.Behaviors.Uwp.Managed`, subject to 05.02.01 decisions.

## Steps
1. Apply supported control and behavior replacements; remove connected-animation metadata/code if no supported equivalent is available and document the behavior change.
2. Preserve navigation, page header updates, TreeView interaction, and gallery selection.
3. Restore/build with Windows App SDK tooling and record remaining failures.

**Done when**: No XAML or source usage remains for removed package surfaces in this scope, and behavior changes are tested or explicitly documented.
