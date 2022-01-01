using System;
using System.Collections.Generic;

using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace FileSorter9000.Helpers
{
    //This class is currently only used as a placeholder when picking a folder fails
    class StorageFolderFake : IStorageFolder
    {
        public StorageFolderFake()
        {
            _path = "";
        }

        private string _path;

        public string Path
        {
            get => _path;
            set => _path = value;
        }

        public IAsyncOperation<StorageFile> CreateFileAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFile> CreateFileAsync(string desiredName, CreationCollisionOption options)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFolder> CreateFolderAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFolder> CreateFolderAsync(string desiredName, CreationCollisionOption options)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFile> GetFileAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<StorageFolder> GetFolderAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IStorageItem> GetItemAsync(string name)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IReadOnlyList<StorageFile>> GetFilesAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IReadOnlyList<StorageFolder>> GetFoldersAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IReadOnlyList<IStorageItem>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncAction RenameAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public IAsyncAction RenameAsync(string desiredName, NameCollisionOption option)
        {
            throw new NotImplementedException();
        }

        public IAsyncAction DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncAction DeleteAsync(StorageDeleteOption option)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<BasicProperties> GetBasicPropertiesAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsOfType(StorageItemTypes type)
        {
            throw new NotImplementedException();
        }

        public FileAttributes Attributes => throw new NotImplementedException();

        public DateTimeOffset DateCreated => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();
    }
}
