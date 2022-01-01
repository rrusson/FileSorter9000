using System;
using System.Threading.Tasks;

using FileSorter9000.Helpers;
using FileSorter9000.Services;

using Microsoft.Toolkit.Mvvm.ComponentModel;

using Windows.Storage.AccessCache;

namespace FileSorter9000.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private bool _showWaitSpinner;

        public string ExamplePath { get; set; }

        public string SourcePath { get; set; }

        public string TargetPath { get; set; }

        public bool ShowWaitSpinner
        {
            get => _showWaitSpinner;
            set
            {
                SetProperty(ref _showWaitSpinner, value);
            }
        }

        public MainViewModel()
        {
            AsyncHelper.RunSync(() => SetFolders());
        }

        private async Task SetFolders()
        {
            ExamplePath = await GetFolderPath("ExampleFolderToken").ConfigureAwait(true);
            SourcePath = await GetFolderPath("SourceFolderToken").ConfigureAwait(true);
            TargetPath = await GetFolderPath("TargetFolderToken").ConfigureAwait(true);
        }

        private async Task<string> GetFolderPath(string tokenName)
        {
            try
            {
                var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(tokenName);
                return folder?.Path ?? @"";
            }
            catch (Exception ex)
            {
                //var toast = new ToastNotificationsService();
                //toast.ShowSimpleToastNotification("No Folder Set", ex.Message);
            }

            return "";
        }
    }
}
