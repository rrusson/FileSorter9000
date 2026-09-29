using FileSorter9000.ViewModels;
using FileSorter9000.Services;
using FileSorter9000.Behaviors;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace FileSorter9000.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e) => ViewModel.LoadedCommand.Execute(null);

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) => ViewModel.ItemInvokedCommand.Execute(args);

        private void NavigationService_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (e.Content is Page page)
            {
                navigationView.Header = NavigationViewHeaderBehavior.GetHeaderMode(page) == NavigationViewHeaderMode.Never
                    ? null
                    : NavigationViewHeaderBehavior.GetHeaderContext(page) ?? ViewModel.Selected?.Content;
                navigationView.AlwaysShowHeader = NavigationViewHeaderBehavior.GetHeaderMode(page) == NavigationViewHeaderMode.Always;
                navigationView.HeaderTemplate = NavigationViewHeaderBehavior.GetHeaderTemplate(page) ?? (DataTemplate)Resources["DefaultNavigationHeaderTemplate"];
            }
        }
    }
}
