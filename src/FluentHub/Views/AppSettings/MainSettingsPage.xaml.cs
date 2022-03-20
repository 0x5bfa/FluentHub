using FluentHub.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class MainSettingsPage : Page
    {
        public MainSettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Helpers.NavigationHelpers.AddPageInfoToTabItem($"Settings", "FluentHub settings", "fluenthub://settings/appearance", "\uE713");
        }

        private void SettingsNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                default:
                case "Appearance":
                    SettingsContentFrame.Navigate(typeof(AppearancePage));
                    NavViewFrameTitleTextBlock.Text = "Appearance";
                    break;
                case "About":
                    SettingsContentFrame.Navigate(typeof(AboutPage));
                    NavViewFrameTitleTextBlock.Text = "About";
                    break;
                case "Accounts":
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
            var currentItem = App.Current.Services.GetService<ITabItemView>();
            currentItem.Header = "Settings";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE115"
            };
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
