using System;
using System.Threading.Tasks;

using FileSorter9000.Activation;

using Microsoft.Toolkit.Uwp.Notifications;

using Windows.ApplicationModel.Activation;
using Windows.UI.Notifications;

namespace FileSorter9000.Services
{
    internal partial class ToastNotificationsService : ActivationHandler<ToastNotificationActivatedEventArgs>
    {
        public void ShowToastNotification(ToastNotification toastNotification)
        {
            try
            {
                ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
            }
            catch (Exception)
            {
                //TODO: ToastNotification can fail in rare conditions, handle exceptions as appropriate.
            }
        }

        public void ShowSimpleToastNotification(string title, string message)
        {
            // Create the toast content
            var content = new ToastContent()
            {
                // More about the Launch property at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastcontent
                Launch = "ToastContentActivationParams",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText() { Text = title },
                            new AdaptiveText() { Text = message }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        // More about Toast Buttons at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastbutton
                        //new ToastButton("OK", "ToastButtonActivationArguments")
                        //{
                        //    ActivationType = ToastActivationType.Foreground
                        //},
                        new ToastButtonDismiss("OK")
                    }
                }
            };

            // Add the content to the toast
            var toast = new ToastNotification(content.GetXml())
            {
                // TODO: Set a unique identifier for this notification within the notification group. (optional)
                // More details at https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastnotification.tag
                Tag = "ToastTag"
            };

            ShowToastNotification(toast);
        }

        protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
        {
            // TODO: Handle activation from toast notification
            // More details at https://docs.microsoft.com/windows/uwp/design/shell/tiles-and-notifications/send-local-toast

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
