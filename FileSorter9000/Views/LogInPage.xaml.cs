using System;

using FileSorter9000.ViewModels;

using Windows.UI.Xaml.Controls;

namespace FileSorter9000.Views
{
    public sealed partial class LogInPage : Page
    {
        public LogInViewModel ViewModel { get; } = new LogInViewModel();

        public LogInPage()
        {
            InitializeComponent();
        }
    }
}
