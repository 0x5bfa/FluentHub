using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Home
{
    public sealed partial class NotificationsPage : Page
    {
        public NotificationsPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem("Notifications", "Viewer's notifications", "https://github.com/notifications", "\uEA8F");
            var currentTab = navigationService.TabView.SelectedItem;
            currentTab.Header = "HomeNavViewItemNotifications/Content".GetLocalized();
            currentTab.Description = "Viewer's notifications";
            currentTab.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uEA8F"
            };

            await ViewModel.GetNotifications();
        }
    }
}