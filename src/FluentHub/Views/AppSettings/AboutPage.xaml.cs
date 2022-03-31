using FluentHub.Services;
using FluentHub.ViewModels.AppSettings;
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

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AboutViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }
        private readonly INavigationService navigationService;
        public AboutViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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