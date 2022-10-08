using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Imaging;

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
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Repositories.png"))
            };
        }
    }
}
