using System;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;

using FileSorter5000.Services;

namespace FileSorter5000.Helpers
{
    internal static class FolderHelper
    {
        internal static async Task<IStorageFolder> GetFolder()
        {
            var picker = new FolderPicker
            {
                CommitButtonText = "Pick Folder",
                SuggestedStartLocation = PickerLocationId.ComputerFolder
            };

            picker.FileTypeFilter.Add("*");

            var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(ActivationService.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandle);

            IStorageFolder folder = await picker.PickSingleFolderAsync();
            return folder ?? new StorageFolderFake();
        }

        internal static async Task<string> SetStorageFolder(string tokenName)
        {
            IStorageFolder folder = await GetFolder().ConfigureAwait(true);

            if (folder != null && folder is StorageFolder)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace(tokenName, folder);
            }

            return folder?.Path ?? "";
        }
    }
}
