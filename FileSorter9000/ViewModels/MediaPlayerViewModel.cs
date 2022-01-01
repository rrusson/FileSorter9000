using System;
using System.Threading.Tasks;

using FileSorter9000.Helpers;

using Microsoft.Toolkit.Mvvm.ComponentModel;

using Windows.ApplicationModel;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace FileSorter9000.ViewModels
{
    public class MediaPlayerViewModel : ObservableObject
    {
        //private string DefaultSource = @"ms-appx:///../../../Mp3ManglerTest/TestItems/NerdRockFromTheSun.mp3";

        // The poster image is displayed until video content is started
        private const string DefaultPoster = @"Assets\Logo.jpg";
        private IMediaPlaybackSource _source;

        public IMediaPlaybackSource Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }

        private string _posterSource;

        public string PosterSource
        {
            get { return _posterSource; }
            set { SetProperty(ref _posterSource, value); }
        }

        public MediaPlayerViewModel()
        {
            AsyncHelper.RunSync(() => SetMediaPlayerSource());
        }

        private async Task SetMediaPlayerSource()
        {
            string fileName = "NerdRockFromTheSun.mp3";
            Source = await GetMedia(fileName).ConfigureAwait(true);
            PosterSource = DefaultPoster;
        }

        private async Task<IMediaPlaybackSource> GetMedia(string fileName)
        {
            try
            {
                var folder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets\");
                var file = await folder.GetFileAsync(fileName);

                return MediaSource.CreateFromStorageFile(file);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DisposeSource()
        {
            var mediaSource = Source as MediaSource;
            mediaSource?.Dispose();
            Source = null;
        }
    }
}
