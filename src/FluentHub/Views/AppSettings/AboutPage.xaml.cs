using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }
        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem($"About", "About FluentHub", "fluenthub://settings/about", "\uE713");
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "About";
            currentItem.Description = "About FluentHub";
            currentItem.Url = "fluenthub://settings?page=about";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713"
            };
        }
    }
}