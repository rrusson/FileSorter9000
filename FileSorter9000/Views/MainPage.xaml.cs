using FileSorter9000.Core.Services;
using FileSorter9000.Helpers;
using FileSorter9000.Services;
using FileSorter9000.ViewModels;

using System.Threading.Tasks;

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

            var toast = new ToastNotificationsService();
            toast.ShowSimpleToastNotification("Please wait...", "TrainingStarted".GetLocalized());

            //Fire and forget (until a Toast message lets user know the data is trained and ready)
            string directoryPath = TxtExample.Text;

            //TODO: WIP to get files to save in UWP's jail
            //_ = Mp3DataService.DumpMp3DataAsync(TxtExample.Text);
            //Task.Run(() => Mp3DataService.DumpMp3DataAsync(directoryPath)).ConfigureAwait(true);
            //var t = Task.Run(() => DoStuff(directoryPath));
            //t.Wait();

            StartButton.IsEnabled = false;
            this.Frame.Navigate(typeof(TreeViewPage));
        }

        private async Task<bool> DoStuff(string directoryPath)
        {
            await Mp3DataService.DumpMp3DataAsync(directoryPath).ConfigureAwait(true);
            return true;
        }

        private void ToggleStartButton()
        {
            StartButton.IsEnabled = !string.IsNullOrEmpty(TxtExample.Text)
                && !string.IsNullOrEmpty(TxtSource.Text)
                && !string.IsNullOrEmpty(TxtTarget.Text);
        }
    }
}
