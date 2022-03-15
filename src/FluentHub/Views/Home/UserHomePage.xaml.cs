using FluentHub.Views.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Home
{
    public sealed partial class UserHomePage : Page
    {
        public UserHomePage()
        {
            this.InitializeComponent();
        }

        private void HomeNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer.Tag.ToString())
            {
                case "Profile":
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Collapsed;
                    HomeContentFrame.Navigate(typeof(ProfilePage), App.SignedInUserName);
                    break;
                case "Notifications":
                    NavViewFrameTitleTextBlock.Text = "Your Notifications";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(NotificationsPage), App.SignedInUserName);
                    break;
                case "Activities":
                    NavViewFrameTitleTextBlock.Text = "Your Activities";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(ActivityPage), App.SignedInUserName);
                    break;
                case "Issues":
                    NavViewFrameTitleTextBlock.Text = "Your Issues";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(IssuesPage), App.SignedInUserName);
                    break;
                case "Pulls":
                    NavViewFrameTitleTextBlock.Text = "Your Pull Requests";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(PullRequestsPage), App.SignedInUserName);
                    break;
                case "Discussions":
                    NavViewFrameTitleTextBlock.Text = "Your Discussions";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(DiscussionsPage), App.SignedInUserName);
                    break;
                case "Repositories":
                    NavViewFrameTitleTextBlock.Text = "Your Repositories";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(RepositoriesPage), App.SignedInUserName);
                    break;
                case "Organizations":
                    NavViewFrameTitleTextBlock.Text = "Your Organizations";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(OrganizationsPage), App.SignedInUserName);
                    break;
                case "Starred":
                    NavViewFrameTitleTextBlock.Text = "Your Starred Repositories";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(StarredReposPage), App.SignedInUserName);
                    break;
            }
        }
    }
}
