using FluentHub.Services;
using FluentHub.ViewModels.AppSettings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class MainSettingsPage : Page
    {
        public MainSettingsPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<MainSettingsViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public MainSettingsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = e.Parameter as string;
            var uri = new Uri(url);
            var pathSegments = url.Split("/").ToList();
            pathSegments.RemoveRange(0, 3);

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Settings";
            currentItem.Description = "FluentHub settings";
            currentItem.Url = "fluenthub://settings";
            currentItem.DisplayUrl = "Settings";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Settings.png"))
            };

            var defaultItem
                = SettingsNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            SettingsNavView.SelectedItem
                = SettingsNavView
                .MenuItems
                .Concat(SettingsNavView.FooterMenuItems)
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), pathSegments.FirstOrDefault(), true) == 0)
                ?? defaultItem;

            OnPageChanged(string.Join("/", pathSegments));

            var command = ViewModel.LoadSignedInLoginsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SettingsNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            OnPageChanged(args?.SelectedItemContainer?.Tag?.ToString());
        }

        private void OnPageChanged(string tag)
        {
            string newUrl = $"fluenthub://settings";

            switch (tag.ToLower())
            {
                case "appearance":
                    SettingsContentFrame.Navigate(typeof(AppearancePage), $"{newUrl}/appearance");
                    NavViewFrameTitleTextBlock.Text = "Appearance";
                    break;
                case "about":
                    SettingsContentFrame.Navigate(typeof(AboutPage), $"{newUrl}/about");
                    NavViewFrameTitleTextBlock.Text = "About";
                    break;
                case "account":
                    SettingsContentFrame.Navigate(typeof(Accounts.AccountPage), $"{newUrl}/account");
                    NavViewFrameTitleTextBlock.Text = "Account";
                    break;
                case "account/otherusers":
                    SettingsContentFrame.Navigate(typeof(Accounts.OtherUsersPage), $"{newUrl}/account/otherusers");
                    NavViewFrameTitleTextBlock.Text = "Account > Other users";
                    break;
                case "repositories":
                    SettingsContentFrame.Navigate(typeof(RepositoryPage), $"{newUrl}/repositories");
                    NavViewFrameTitleTextBlock.Text = "Repositories";
                    break;
                case "notifications":
                    SettingsContentFrame.Navigate(typeof(NotificationsPage), $"{newUrl}/notifications");
                    NavViewFrameTitleTextBlock.Text = "Notifications";
                    break;
            }
        }

        private void OnAccountButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsNavViewItemAccount.IsSelected = true;
            NavViewFrameTitleTextBlock.Text = "Account";
        }
    }
}
