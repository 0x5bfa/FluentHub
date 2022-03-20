using FluentHub.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AppearancePage : Page
    {
        public AppearancePage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Appearance", "Appearance settings", "fluenthub://settings/appearance", "\uE713");
            var currentItem = App.Current.Services.GetService<ITabItemView>().NavigationHistory.CurrentItem;
            currentItem.Header = "Appearance";
            currentItem.Description = "Appearance settings";
            currentItem.Url = "fluenthub://settings/appearance";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713",
            };
        }
    }
}