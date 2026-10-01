using System;

using FileSorter5000.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace FileSorter5000.Views
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
