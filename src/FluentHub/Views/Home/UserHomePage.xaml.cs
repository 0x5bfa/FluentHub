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
            string tag = args.SelectedItemContainer.Tag.ToString();

            switch (tag)
            {
                case "Profile":
                    HomeContentFrame.Navigate(typeof(ProfilePage), App.SignedInUserName);
                    break;
                case "Notifications":
                    HomeContentFrame.Navigate(typeof(NotificationsPage));
                    break;
                case "Activities":
                    HomeContentFrame.Navigate(typeof(ActivityPage));
                    break;

                case "Issues":
                    HomeContentFrame.Navigate(typeof(IssueListPage), App.SignedInUserName);
                    break;
                case "Pulls":
                    HomeContentFrame.Navigate(typeof(PullRequestListPage), App.SignedInUserName);
                    break;
                case "Discussions":
                    //HomeContentFrame.Navigate(typeof());
                    break;
                case "Repositories":
                    HomeContentFrame.Navigate(typeof(RepositoriesPage), App.SignedInUserName);
                    break;
                case "Organizations":
                    //HomeContentFrame.Navigate(typeof());
                    break;
                case "Starred":
                    HomeContentFrame.Navigate(typeof(StarsPage), App.SignedInUserName);
                    break;
            }
        }
    }
}
