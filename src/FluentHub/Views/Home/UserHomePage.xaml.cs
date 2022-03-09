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
            _ = args.SelectedItemContainer.Tag.ToString() switch
            {
                "Profile" =>       HomeContentFrame.Navigate(typeof(ProfilePage), App.SignedInUserName),
                "Notifications" => HomeContentFrame.Navigate(typeof(NotificationsPage)),
                "Activities" =>    HomeContentFrame.Navigate(typeof(ActivityPage)),
                "Issues" =>        HomeContentFrame.Navigate(typeof(IssuesPage), App.SignedInUserName),
                "Pulls" =>         HomeContentFrame.Navigate(typeof(PullRequestListPage), App.SignedInUserName),
                //"Discussions" => HomeContentFrame.Navigate(typeof()),
                "Repositories" =>  HomeContentFrame.Navigate(typeof(RepositoriesPage), App.SignedInUserName),
                //"Organizations" => HomeContentFrame.Navigate(typeof()),
                "Starred" =>       HomeContentFrame.Navigate(typeof(StarsPage), App.SignedInUserName),
                _ =>               HomeContentFrame.Navigate(typeof(ProfilePage), App.SignedInUserName),
            };
        }
    }
}
