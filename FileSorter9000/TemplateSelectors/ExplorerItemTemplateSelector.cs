using System;
using System.Resources;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FileSorter9000.TemplateSelectors
{
    public class ExplorerItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FolderTemplate { get; set; }

        public DataTemplate FileTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            var explorerItem = item as ExplorerItem;

            if (explorerItem == null)
            {
                ResourceManager rm = new ResourceManager("AppResources", typeof(ExplorerItemTemplateSelector).Assembly);
                string errorMsg = rm.GetString("ExplorerItemError", System.Globalization.CultureInfo.CurrentUICulture);
                throw new ArgumentException(errorMsg);
            }

            return explorerItem.Type == ExplorerItem.ExplorerItemType.Folder ? FolderTemplate : FileTemplate;
        }
    }
}
