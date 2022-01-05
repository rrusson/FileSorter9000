using System;
using System.Threading.Tasks;

using FileSorter9000.Core.Helpers;
using FileSorter9000.Services;

using Windows.ApplicationModel.Activation;

namespace FileSorter9000.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        private readonly Type _navElement;

        public DefaultActivationHandler(Type navElement)
        {
            _navElement = navElement;
        }

        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.Navigate(_navElement, arguments);

            // TODO: Add toast notifications when long-running events complete
            //string msg = @"Click OK to see how activation from a toast notification can be handled in the ToastNotificationService.";
            //Singleton<ToastNotificationsService>.Instance.ShowSimpleToastNotification("Sample Toast Notification", msg);

            await Task.CompletedTask.ConfigureAwait(false);
        }

        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return NavigationService.Frame.Content == null && _navElement != null;
        }
    }
}
