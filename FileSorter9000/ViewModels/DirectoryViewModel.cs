using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

using FileSorter9000.Helpers;
using FileSorter9000.TemplateSelectors;
using FileSorter9000.Services;

namespace FileSorter9000.ViewModels
{
    public class DirectoryViewModel : ObservableObject
    {
        private List<string> _filePaths = new List<string>();

        private bool _showWaitSpinner;

        public ObservableCollection<ExplorerItem> DataSource;

        public bool ShowWaitSpinner
        {
            get => _showWaitSpinner;
            set
            {
                SetProperty(ref _showWaitSpinner, value);
            }
        }

        public DirectoryViewModel()
        {
            ShowWaitSpinner = true;
            DataSource = AsyncHelper.RunSync<ObservableCollection<ExplorerItem>>(() => GetFolderData());
            ShowWaitSpinner = false;
        }

        //TODO: Move this process to BackGroundTasks and show wait spinner appropriately
        private async Task<ObservableCollection<ExplorerItem>> GetFolderData()
        {
            try
            {
                var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("ExampleFolderToken");

                var list = new ObservableCollection<ExplorerItem>
            {
                await GetExplorerItem(folder.Path).ConfigureAwait(true)
            };

                return list;
            }
            catch (Exception ex)
            {
                var toast = new ToastNotificationsService();
                toast.ShowSimpleToastNotification("No Folders Set!", @"You must set your example, source, and target folders before making corrections.");
            }

            return null;
        }

        private async Task<ExplorerItem> GetExplorerItem(string path)
        {
            StorageFolder storage = await StorageFolder.GetFolderFromPathAsync(path);

            var folder = new ExplorerItem()
            {
                Name = storage.Name,
                Type = ExplorerItem.ExplorerItemType.Folder
            };

            var dirs = await storage.GetFoldersAsync();

            if (dirs.Count > 0)
            {
                foreach (var dir in dirs)
                {
                    ExplorerItem item = await GetExplorerItem(dir.Path).ConfigureAwait(true);
                    folder.Children.Add(item);
                }
            }

            foreach (var file in await storage.GetFilesAsync())
            {
                folder.Children.Add(new ExplorerItem()
                {
                    Name = file.Name,
                    Type = ExplorerItem.ExplorerItemType.File
                });

                _filePaths.Add(file.Path);
            }

            return folder;
        }

        private ExplorerItem GetExplorerItem(DirectoryInfo directory)
        {
            var folder1 = new ExplorerItem()
            {
                Name = directory.Name,
                Type = ExplorerItem.ExplorerItemType.Folder
            };

            DirectoryInfo[] dirs = directory.GetDirectories();

            if (dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    ExplorerItem item = GetExplorerItem(dir);
                    folder1.Children.Add(item);
                }
            }

            foreach (var file in directory.GetFiles())
            {
                folder1.Children.Add(new ExplorerItem()
                {
                    Name = file.Name,
                    Type = ExplorerItem.ExplorerItemType.File
                });
            }

            return folder1;
        }
    }
}
