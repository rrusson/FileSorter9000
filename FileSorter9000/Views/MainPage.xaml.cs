using FileSorter9000.Helpers;
using FileSorter9000.ViewModels;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FileSorter9000.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            ViewModel.ShowWaitSpinner = false;

            TxtExample.Text = ViewModel.ExamplePath ?? "";
            TxtSource.Text = ViewModel.SourcePath ?? "";
            TxtTarget.Text = ViewModel.TargetPath ?? "";
            ToggleStartButton();
        }


        private async void PickExampleFolderButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string folder = await FolderHelper.SetStorageFolder("ExampleFolderToken").ConfigureAwait(true);

            TxtExample.Text = folder;
            ViewModel.ExamplePath = folder;
            ToggleStartButton();
        }

        private async void PickSourceFolderButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string folder = await FolderHelper.SetStorageFolder("SourceFolderToken").ConfigureAwait(true);

            TxtSource.Text = folder;
            ViewModel.SourcePath = folder;
            ToggleStartButton();
        }

        private async void PickTargetFolderButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string folder = await FolderHelper.SetStorageFolder("TargetFolderToken").ConfigureAwait(true);

            TxtTarget.Text = folder;
            ViewModel.TargetPath = folder;
            ToggleStartButton();
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowWaitSpinner = true;

            //waitSpinner.IsActive = true;
            StartButton.IsEnabled = false;
            this.Frame.Navigate(typeof(TreeViewPage));
        }

        private void ToggleStartButton()
        {
            StartButton.IsEnabled = !string.IsNullOrEmpty(TxtExample.Text)
                && !string.IsNullOrEmpty(TxtSource.Text)
                && !string.IsNullOrEmpty(TxtTarget.Text);
        }
    }
}
