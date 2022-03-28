using FluentHub.Services;
using FluentHub.ViewModels.AppSettings;
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

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<AppearanceViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();            
        }

        private readonly INavigationService navigationService;
        public AppearanceViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
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