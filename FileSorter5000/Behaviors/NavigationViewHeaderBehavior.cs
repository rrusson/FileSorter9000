using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FileSorter5000.Behaviors
{
    public static class NavigationViewHeaderBehavior
    {
        public static readonly DependencyProperty HeaderModeProperty = DependencyProperty.RegisterAttached(
            "HeaderMode",
            typeof(NavigationViewHeaderMode),
            typeof(NavigationViewHeaderBehavior),
            new PropertyMetadata(defaultValue: NavigationViewHeaderMode.Always));

        public static NavigationViewHeaderMode GetHeaderMode(DependencyObject element) =>
            (NavigationViewHeaderMode)element.GetValue(HeaderModeProperty);

        public static void SetHeaderMode(DependencyObject element, NavigationViewHeaderMode value) =>
            element.SetValue(HeaderModeProperty, value);

        public static readonly DependencyProperty HeaderContextProperty = DependencyProperty.RegisterAttached(
            "HeaderContext",
            typeof(object),
            typeof(NavigationViewHeaderBehavior),
            new PropertyMetadata(null));

        public static object GetHeaderContext(DependencyObject element) => element.GetValue(HeaderContextProperty);

        public static void SetHeaderContext(DependencyObject element, object value) => element.SetValue(HeaderContextProperty, value);

        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.RegisterAttached(
            "HeaderTemplate",
            typeof(DataTemplate),
            typeof(NavigationViewHeaderBehavior),
            new PropertyMetadata(null));

        public static DataTemplate GetHeaderTemplate(DependencyObject element) => (DataTemplate)element.GetValue(HeaderTemplateProperty);

        public static void SetHeaderTemplate(DependencyObject element, DataTemplate value) => element.SetValue(HeaderTemplateProperty, value);
    }
}
