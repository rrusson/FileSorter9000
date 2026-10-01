using System.Collections.Generic;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace FileSorter5000.Behaviors
{
    public sealed class TreeViewCollapseBehavior
    {
        private readonly WinUI.TreeView _treeView;

        public ICommand CollapseAllCommand { get; }

        public TreeViewCollapseBehavior(WinUI.TreeView treeView)
        {
            _treeView = treeView;
            CollapseAllCommand = new RelayCommand(() => CollapseNodes(AssociatedObject.RootNodes));
        }

        private WinUI.TreeView AssociatedObject => _treeView;

        private void CollapseNodes(IList<WinUI.TreeViewNode> nodes)
        {
            foreach (var node in nodes)
            {
                CollapseNodes(node.Children);
                AssociatedObject.Collapse(node);
            }
        }
    }
}
