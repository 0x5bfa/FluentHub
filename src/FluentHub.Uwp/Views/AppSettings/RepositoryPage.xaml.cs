using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.AppSettings
{
    public sealed partial class RepositoryPage : Page
    {
        public RepositoryPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Repositories settings";
            currentItem.Description = "Repositories settings";
            currentItem.Url = "fluenthub://settings/repositories";
            currentItem.DisplayUrl = $"Settings / Repositories";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
        }
    }
}
