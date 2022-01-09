using FileSorter9000.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FileSorter9000.Core.Services;
using FileSorter9000.Services;

namespace FileSorter9000.Views
{
    public sealed partial class TreeViewPage : Page
    {
        public DirectoryViewModel ViewModel { get; } = new DirectoryViewModel();

        public TreeViewPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private async void AnalyzeButtonClick(object sender, RoutedEventArgs e)
        {
            AnalyzeButton.IsEnabled = false;
            ViewModel.ShowWaitSpinner = true;
            PathUpdateService update = new PathUpdateService();
            //TODO: Need to loop through selected files and send each one for prediction
            var toast = new ToastNotificationsService();
            string tempTest = @"C:\Temp\Mp3s\ExampleFile.mp3";

            try
            {
                //string updatedPath = await update.GetAiPredictedPathAsync(tempTest).ConfigureAwait(true);

                //toast.ShowSimpleToastNotification("Files Processed!", "Updated Path:" + updatedPath);
            }
            catch (System.Exception ex)
            {
                toast.ShowSimpleToastNotification("That didn't work out.", "Updated Path:" + ex.Message);
            }
        }
    }
}
