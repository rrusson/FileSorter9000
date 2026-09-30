using System;

using FileSorter9000.Core.Helpers;
using FileSorter9000.Core.Services;
using FileSorter5000.Services;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Windows.ApplicationModel.Activation;
using Microsoft.UI.Xaml;

namespace FileSorter5000
{
    public sealed partial class App : Application
    {
        private IIdentityService IdentityService => Singleton<FakeIdentityService>.Instance;

        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // App center info https://docs.microsoft.com/appcenter/sdk/getting-started/uwp
            AppCenter.Start("e62e719c-2b3d-42e0-992d-8284294ed315", typeof(Analytics), typeof(Crashes));
            UnhandledException += OnAppUnhandledException;

            _activationService = new Lazy<ActivationService>(CreateActivationService);
            IdentityService.LoggedOut += OnLoggedOut;
        }

        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            await ActivationService.ActivateAsync(args).ConfigureAwait(false);
        }


        private void OnAppUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        private async void OnLoggedOut(object sender, EventArgs e)
        {
            ActivationService.SetShell(new Lazy<UIElement>(CreateShell));
            await ActivationService.RedirectLoginPageAsync().ConfigureAwait(false);
        }
    }
}
