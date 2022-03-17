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
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Users
{
    public sealed partial class ProfilePage : Page
    {
        private string login;

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            login = e.Parameter as string;

            Helpers.NavigationHelpers.AddPageInfoToTabItem($"{login}'s profile", $"https://github.com/{login}", $"https://github.com/{login}", "\uE77B");

            await ViewModel.GetUser(login);
            UpdateVisibility();

            base.OnNavigatedTo(e);
        }

        private void UserNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem == null || args.SelectedItemContainer == null)
            {
                return;
            }

            _ = args.SelectedItemContainer.Tag.ToString() switch
            {
                "Overview" =>     UserNavViewContent.Navigate(typeof(OverviewPage), login, args.RecommendedNavigationTransitionInfo),
                "Repositories" => UserNavViewContent.Navigate(typeof(RepositoriesPage), login, args.RecommendedNavigationTransitionInfo),
                "Stars" =>        UserNavViewContent.Navigate(typeof(StarredReposPage), login, args.RecommendedNavigationTransitionInfo),
                "Followers" =>    UserNavViewContent.Navigate(typeof(FollowersPage), login, args.RecommendedNavigationTransitionInfo),
                "Following" =>    UserNavViewContent.Navigate(typeof(FollowingPage), login, args.RecommendedNavigationTransitionInfo),
                _ =>              UserNavViewContent.Navigate(typeof(OverviewPage), login, args.RecommendedNavigationTransitionInfo)
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
