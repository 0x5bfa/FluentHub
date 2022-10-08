using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories.Discussions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Uwp.Views.Repositories.Settings
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Settings";
            currentItem.Description = "Settings";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
            };
        }
    }
}
