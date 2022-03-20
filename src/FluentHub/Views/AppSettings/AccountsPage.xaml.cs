using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AccountsPage : Page
    {
        public AccountsPage()
        {
            this.InitializeComponent();
            Loaded += AccountsPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Helpers.NavigationHelpers.AddPageInfoToTabItem("Accounts", "Users signed in FluentHub", "fluenthub://settings/accounts", "\uE713");
        }

        private async void AccountsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await SetUserInfo();
        }

        private async Task SetUserInfo()
        {
            var User = await App.Client.User.Get(App.SignedInUserName);

            // Avator
            BitmapImage avatorImage = new BitmapImage(new Uri(User.AvatarUrl));
            UserAvatorImage.Source = avatorImage;

            // Login name
            if (!string.IsNullOrEmpty(User.Login))
            {
                LoginNameTextBlock.Text = User.Login;
            }

            // User name
            if (!string.IsNullOrEmpty(User.Name))
            {
                UserNameTextBlock.Text = User.Name;
            }
        }
    }
}
