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
                case "TextEditor":
                    //SettingsContentFrame.Navigate(typeof(TextEditorPage));
                    NavViewFrameTitleTextBlock.Text = "Text editor";
                    break;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var authedUser = await App.Client.User.Get(App.SignedInUserName);

            AppSignedInUserAvatar.Source = new BitmapImage(new Uri(authedUser.AvatarUrl));

            SignedInLoginName.Text = authedUser.Login;

            SignedInUserName.Text = authedUser.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsNavViewItemAccount.IsSelected = true;
            NavViewFrameTitleTextBlock.Text = "Accounts";
        }
    }
}