using FluentHub.Services;
using FluentHub.ViewModels.Home;
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

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<NotificationsViewModel>();             
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public NotificationsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem("Notifications", "Viewer's notifications", "https://github.com/notifications", "\uEA8F");
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "HomeNavViewItemNotifications/Content".GetLocalized();
            currentItem.Description = "Viewer's notifications";
            currentItem.Url = "https://github.com/notifications";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.FontIconSource
            {
                Glyph = "\uEA8F"
            };

            var command = ViewModel.RefreshNotificationsCommand;

            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}