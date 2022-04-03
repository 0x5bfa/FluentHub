using FluentHub.Services;
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
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Settings";
            currentItem.Description = "FluentHub settings";
            currentItem.Url = "fluenthub://settings";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713"
            };

            var defaultItem = SettingsNavView
                                    .MenuItems
                                    .OfType<muxc.NavigationViewItem>()
                                    .FirstOrDefault();

            SettingsNavView.SelectedItem = SettingsNavView
                                                .MenuItems
                                                .Concat(SettingsNavView.FooterMenuItems)
                                                .OfType<muxc.NavigationViewItem>()
                                                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), e.Parameter?.ToString(), true) == 0)
                                                ?? defaultItem;
        }

        private void SettingsNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer?.Tag?.ToString())
            {
                case "appearance":
                    SettingsContentFrame.Navigate(typeof(AppearancePage));
                    NavViewFrameTitleTextBlock.Text = "Appearance";
                    break;
                case "about":
                    SettingsContentFrame.Navigate(typeof(AboutPage));
                    NavViewFrameTitleTextBlock.Text = "About";
                    break;
                case "accounts":
                    SettingsContentFrame.Navigate(typeof(AccountsPage));
                    NavViewFrameTitleTextBlock.Text = "Accounts";
                    break;
                case "codepreview":
                    SettingsContentFrame.Navigate(typeof(CodePreviewPage));
                    NavViewFrameTitleTextBlock.Text = "Code Preview";
                    break;
            }
        }

        private void OnAccountButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsNavViewItemAccount.IsSelected = true;
            NavViewFrameTitleTextBlock.Text = "Accounts";
        }
    }
}
