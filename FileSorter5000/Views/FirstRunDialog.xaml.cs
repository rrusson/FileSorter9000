using System;

using FileSorter5000.Services;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FileSorter5000.Views
{
    public sealed partial class FirstRunDialog : ContentDialog
    {
        public FirstRunDialog()
        {
            // TODO WTS: Update the contents of this dialog with any important information you want to show when the app is used for the first time.
            RequestedTheme = (ActivationService.MainWindow.Content as FrameworkElement)?.RequestedTheme ?? ElementTheme.Default;
            InitializeComponent();
        }
    }
}
