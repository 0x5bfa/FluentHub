using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AppearancePage : Page
    {
        public AppearancePage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"Appearance", "Appearance settings", "fluenthub://settings/appearance", "\uE713");
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Appearance";
            currentItem.Description = "Appearance settings";
            currentItem.Url = "fluenthub://settings?page=appearance";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713",
            };
        }
    }
}