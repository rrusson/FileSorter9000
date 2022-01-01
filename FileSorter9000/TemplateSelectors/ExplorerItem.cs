using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FileSorter9000.TemplateSelectors
{
    public class ExplorerItem : INotifyPropertyChanged
    {
        private bool _isExpanded;
        private bool _isSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public enum ExplorerItemType { Folder, File };

        public string Name { get; set; }

        public ExplorerItemType Type { get; set; }

        public ObservableCollection<ExplorerItem> Children { get; } = new ObservableCollection<ExplorerItem>();

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }

            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }

        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
