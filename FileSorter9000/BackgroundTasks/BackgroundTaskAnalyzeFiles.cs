using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.System.Threading;

namespace FileSorter9000.BackgroundTasks
{
    public sealed class BackgroundTaskAnalyzeFiles : BackgroundTask
    {
        private volatile bool _cancelRequested = false;
        private IBackgroundTaskInstance _taskInstance;
        private BackgroundTaskDeferral _deferral;
        private List<string> _filePaths = new List<string>();

        public static string Message { get; set; }

        public override void Register()
        {
            var taskName = GetType().Name;
            var taskRegistration = BackgroundTaskRegistration.AllTasks.FirstOrDefault(t => t.Value.Name == taskName).Value;

            if (taskRegistration == null)
            {
                var builder = new BackgroundTaskBuilder()
                {
                    Name = taskName
                };

                // TODO WTS: Define the trigger for your background task and set any (optional) conditions
                // More details at https://docs.microsoft.com/windows/uwp/launch-resume/create-and-register-an-inproc-background-task
                builder.SetTrigger(new TimeTrigger(15, false));
                builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));

                builder.Register();
            }
        }

        public override async Task RunAsyncInternal(IBackgroundTaskInstance taskInstance)
        {
            if (taskInstance == null)
            {
                return;
            }

            _deferral = taskInstance.GetDeferral();
            await SetAllFilePathsAsync().ConfigureAwait(false);
        }

        public override void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _cancelRequested = true;

            // TODO WTS: Insert code to handle the cancelation request here.
            // Documentation: https://docs.microsoft.com/windows/uwp/launch-resume/handle-a-cancelled-background-task
        }

        private void SampleTimerCallback(ThreadPoolTimer timer)
        {
            if (_cancelRequested == false && _taskInstance.Progress < 100)
            {
                _taskInstance.Progress += 10;
                Message = $"Background Task {_taskInstance.Task.Name} running";
            }
            else
            {
                timer.Cancel();

                if (_cancelRequested)
                {
                    Message = $"Background Task {_taskInstance.Task.Name} cancelled";
                }
                else
                {
                    Message = $"Background Task {_taskInstance.Task.Name} finished";
                }

                _deferral?.Complete();
            }
        }

        private async Task SetAllFilePathsAsync()
        {
            try
            {
                var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("SourceFolderToken");

                await SetPathsAsync(folder.Path).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                //TODO: Catch more specific exception for when SourceFolderToken hasn't been set yet
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            //TODO: Create some sort of pub/sub to inform the UI that the files have been cataloged and analyzed
        }

        /// <summary>
        /// Recursively gets all files from all subdirectories under <paramref name="path"/>
        /// </summary>
        /// <param name="path">Starting folder path</param>
        private async Task SetPathsAsync(string path)
        {
            StorageFolder storage = await StorageFolder.GetFolderFromPathAsync(path);

            var dirs = await storage.GetFoldersAsync();

            if (dirs.Count > 0)
            {
                foreach (var dir in dirs)
                {
                    await SetPathsAsync(dir.Path).ConfigureAwait(true);
                }
            }

            foreach (var file in await storage.GetFilesAsync())
            {
                _filePaths.Add(file.Path);
            }
        }
    }
}
