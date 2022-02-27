using FluentHub.Helpers;
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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
    public sealed partial class ProfilePage : Windows.UI.Xaml.Controls.Page
    {
        private string UserName { get; set; }
        private User User { get; set; }

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserName = e.Parameter as string;

            base.OnNavigatedTo(e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            User = await App.Client.User.Get(UserName);

            if (User == null)
            {
                return;
            }

            await SetUserInfo();
        }

        private void UserNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem == null || args.SelectedItemContainer == null)
            {
                return;
            }

            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "Overview":
                    UserNavViewContent.Navigate(typeof(OverviewPage), UserName);
                    break;
                case "Repositories":
                    UserNavViewContent.Navigate(typeof(RepoListPage), UserName);
                    break;
                case "Stars":
                    UserNavViewContent.Navigate(typeof(StarListPage), UserName);
                    break;
                case "Followers":
                    UserNavViewContent.Navigate(typeof(FollowersPage), UserName);
                    break;
                case "Following":
                    UserNavViewContent.Navigate(typeof(FollowingPage), UserName);
                    break;
            }
        }

        private async Task SetUserInfo()
        {
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

            // Bio
            if (!string.IsNullOrEmpty(User.Bio))
            {
                MentionHelpers mentionHelpers = new MentionHelpers();
                await mentionHelpers.GetTextBlock(User.Bio, ref UserBioTextBlock);

                UserBioTextBlock.Visibility = Visibility.Visible;
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

            // FF
            FollowerCount.Text = User.Followers.ToString();
            FollowingCount.Text = User.Following.ToString();

            if (User.Login == App.SignedInUserName)
            {
                EditProfileButton.Visibility = Visibility.Visible;
            }
            else
            {
                FollowButton.Visibility = Visibility.Visible;
            }
        }

        private void UserFollowersButton_Click(object sender, RoutedEventArgs e)
        {
            UserNavViewItemFollowers.IsSelected = true;
        }

        private void UserFollowingButton_Click(object sender, RoutedEventArgs e)
        {
            UserNavViewItemFollowing.IsSelected = true;
        }
    }
}
