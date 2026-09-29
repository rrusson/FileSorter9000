using System;
using System.Threading.Tasks;
using FileSorter9000.Views;
using Microsoft.UI.Xaml;
using Windows.Storage;

namespace FileSorter9000.Services
{
    public static class FirstRunDisplayService
    {
        private const string HasShownFirstRunKey = "HasShownFirstRun";
        private static bool _shown;

        internal static async Task ShowIfAppropriateAsync()
        {
            var settings = ApplicationData.Current.LocalSettings;
            if (!_shown && settings.Values[HasShownFirstRunKey] is not true)
            {
                _shown = true;
                var dialog = new FirstRunDialog
                {
                    XamlRoot = (Window.Current.Content as FrameworkElement)?.XamlRoot
                };

                await dialog.ShowAsync();
                settings.Values[HasShownFirstRunKey] = true;
            }
        }
    }
}
