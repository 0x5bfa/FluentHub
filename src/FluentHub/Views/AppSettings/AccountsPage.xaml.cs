using FluentHub.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.AppSettings
{
    public sealed partial class AccountsPage : Page
    {
        public AccountsPage()
        {
            InitializeComponent();
            Loaded += AccountsPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Helpers.NavigationHelpers.AddPageInfoToTabItem("Accounts", "Users signed in FluentHub", "fluenthub://settings/accounts", "\uE713");
            var currentItem = App.Current.Services.GetService<ITabItemView>().NavigationHistory.CurrentItem;
            currentItem.Header = "Accounts";
            currentItem.Description = "Users signed in FluentHub";
            currentItem.Url = "fluenthub://settings/accounts";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE713",
            };
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