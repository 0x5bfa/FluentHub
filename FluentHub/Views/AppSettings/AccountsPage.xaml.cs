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

            // Company
            if (!string.IsNullOrEmpty(User.Company))
            {
                try
                {
                    if (User.Company.IndexOf("@") == 0)
                    {
                        CompanyLinkButton.Content = User.Company;
                        string company = User.Company.Replace("@", "");

                        var userCompany = await App.Client.User.Get(company);
                        var org = await App.Client.Organization.Get(company);
                        string navigateUrl = null;
                        navigateUrl = App.DefaultHost + "/" + company;
                        var uri = new UriBuilder(navigateUrl).Uri;
                        CompanyLinkButton.NavigateUri = uri;
                        CompanyLinkButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CompanyLinkTextBlock.Text = User.Company;
                        CompanyLinkTextBlock.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                    CompanyLinkTextBlock.Text = User.Company;
                    CompanyLinkTextBlock.Visibility = Visibility.Visible;
                }

                CompanyBlock.Visibility = Visibility.Visible;
            }

            // Link
            if (!string.IsNullOrEmpty(User.Blog))
            {
                LinkButton.Content = User.Blog;
                var uri = new UriBuilder(User.Blog).Uri;
                LinkButton.NavigateUri = uri;
                LinkBlock.Visibility = Visibility.Visible;
            }

            // Location
            if (!string.IsNullOrEmpty(User.Location))
            {
                LocationTextBlock.Text = User.Location;
                LocationBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
