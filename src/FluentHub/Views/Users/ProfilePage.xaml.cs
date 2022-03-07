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

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            UserName = e.Parameter as string;
            await ViewModel.GetUser(UserName);
            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UserNavView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem == null || args.SelectedItemContainer == null)
            {
                return;
            }

            // C#9.0
            _ = args.SelectedItemContainer.Tag.ToString() switch
            {
                "Overview" =>     UserNavViewContent.Navigate(typeof(OverviewPage), UserName),
                "Repositories" => UserNavViewContent.Navigate(typeof(RepoListPage), UserName),
                "Stars" =>        UserNavViewContent.Navigate(typeof(StarListPage), UserName),
                "Followers" =>    UserNavViewContent.Navigate(typeof(FollowersPage), UserName),
                "Following" =>    UserNavViewContent.Navigate(typeof(FollowingPage), UserName),
                _ =>              UserNavViewContent.Navigate(typeof(OverviewPage), UserName)
            };
        }

        private void UpdateVisibility()
        {
            if (!string.IsNullOrEmpty(CompanyLinkTextBlock.Text))
            {
                CompanyBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(UserBioTextBlock.Text))
            {
                UserBioTextBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LocationTextBlock.Text))
            {
                LocationBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LinkButton.Content as string))
            {
                var uri = new UriBuilder(LinkButton.Content as string).Uri;
                LinkButton.NavigateUri = uri;
                LinkBlock.Visibility = Visibility.Visible;
            }

            if (LoginNameTextBlock.Text == App.SignedInUserName)
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
