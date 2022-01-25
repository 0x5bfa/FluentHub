using Octokit;
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
using muxc = Microsoft.UI.Xaml.Controls;


namespace FluentHub.Views.UserPages
{
    public sealed partial class ProfilePage : Windows.UI.Xaml.Controls.Page
    {
        private string requestedUsername { get; set; } = "";
        private User user { get; set; } = new User();

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            requestedUsername = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            user = await App.Client.User.Get(requestedUsername);

            _=await SetTopLevelUserInfo();
            ViewModel.SetProfileElements(user);
        }

        private void UserNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            if(args.SelectedItem == null || args.SelectedItemContainer == null)
            {
                return;
            }

            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "Overview":
                    UserNavViewContent.Navigate(typeof(Activities));
                    break;
                case "Repositories":
                    UserNavViewContent.Navigate(typeof(Repositories));
                    break;
                case "Stars":
                    UserNavViewContent.Navigate(typeof(Stars));
                    break;
            }
        }

        private async Task<bool> SetTopLevelUserInfo()
        {
            // Avator
            BitmapImage avatorImage = new BitmapImage(new Uri(user.AvatarUrl));
            UserAvatorImage.Source = avatorImage;

            // Username
            if (user.Login != null)
            {
                Username.Text = user.Login;
            }

            // Fullname
            if (user.Name != null)
            {
                FullName.Text = user.Name;
            }

            // Company
            if (user.Company != null)
            {
                try
                {
                    if (user.Company.IndexOf("@") == 0)
                    {
                        CompanyLinkButton.Content = user.Company;
                        string company = user.Company.Replace("@", "");

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
                        CompanyLinkTextBlock.Text = user.Company;
                        CompanyLinkTextBlock.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                    CompanyLinkTextBlock.Text = user.Company;
                    CompanyLinkTextBlock.Visibility = Visibility.Visible;
                }

                CompanyBlock.Visibility = Visibility.Visible;
            }

            // Bio
            if (user.Bio != null)
            {
                UserBioTextBlock.Text = user.Bio;
                UserBioTextBlock.Visibility = Visibility.Visible;
            }

            //Link
            if (user.Blog != null)
            {
                LinkButton.Content = user.Blog;
                var uri = new UriBuilder(user.Blog).Uri;
                LinkButton.NavigateUri = uri;
                LinkBlock.Visibility = Visibility.Visible;
            }

            // Location
            if (user.Location != null)
            {
                LocationTextBlock.Text = user.Location;
                LocationBlock.Visibility = Visibility.Visible;
            }

            // FF
            FollowerCount.Text = user.Followers.ToString();
            FollowingCount.Text = user.Following.ToString();

            if (user.Login == App.SignedInUserName)
            {
                EditProfileButton.Visibility = Visibility.Visible;
            }
            else
            {
                FollowButton.Visibility = Visibility.Visible;
            }

            return true;
        }
    }
}
