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
                    NavViewFrameTitleTextBlock.Text = "Yours Notifications";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(NotificationsPage));
                    break;
                case "Activities":
                    NavViewFrameTitleTextBlock.Text = "Yours Activities";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(ActivityPage));
                    break;
                case "Issues":
                    NavViewFrameTitleTextBlock.Text = "Yours Issues";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(IssuesPage), App.SignedInUserName);
                    break;
                case "Pulls":
                    NavViewFrameTitleTextBlock.Text = "Yours Pull Requests";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(PullRequestsPage), App.SignedInUserName);
                    break;
                case "Discussions":
                    NavViewFrameTitleTextBlock.Text = "Yours Discussions";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(DiscussionsPage));
                    break;
                case "Repositories":
                    NavViewFrameTitleTextBlock.Text = "Yours Repositories";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(RepositoriesPage), App.SignedInUserName);
                    break;
                case "Organizations":
                    NavViewFrameTitleTextBlock.Text = "Yours Organizations";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(OrganizationsPage));
                    break;
                case "Starred":
                    NavViewFrameTitleTextBlock.Text = "Yours Starred Repositories";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(StarredReposPage), App.SignedInUserName);
                    break;
            }
        }
    }
}
