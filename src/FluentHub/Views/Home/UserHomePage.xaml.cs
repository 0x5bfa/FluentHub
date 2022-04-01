using FluentHub.Views.Users;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Home
{
    public sealed partial class UserHomePage : Page
    {
        public UserHomePage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var defaultItem = HomeNavView
                                .MenuItems
                                .OfType<muxc.NavigationViewItem>()
                                .FirstOrDefault();

            HomeNavView.SelectedItem = HomeNavView
                                        .MenuItems
                                        .OfType<muxc.NavigationViewItem>()
                                        .FirstOrDefault(x => string.Compare(x.Tag.ToString(), e.Parameter?.ToString(), true) == 0)
                                        ?? defaultItem;
        }

        private void HomeNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            switch (args.SelectedItemContainer?.Tag?.ToString())
            {
                case "profile":
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Collapsed;
                    HomeContentFrame.Navigate(typeof(ProfilePage), App.Settings.SignedInUserName);
                    break;
                case "notifications":
                    NavViewFrameTitleTextBlock.Text = "Your Notifications";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(NotificationsPage), App.Settings.SignedInUserName);
                    break;
                case "activities":
                    NavViewFrameTitleTextBlock.Text = "Your Activities";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(ActivitiesPage), App.Settings.SignedInUserName);
                    break;
                case "issues":
                    NavViewFrameTitleTextBlock.Text = "Your Issues";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(IssuesPage), App.Settings.SignedInUserName);
                    break;
                case "pullrequests":
                    NavViewFrameTitleTextBlock.Text = "Your Pull Requests";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(PullRequestsPage), App.Settings.SignedInUserName);
                    break;
                case "discussions":
                    NavViewFrameTitleTextBlock.Text = "Your Discussions";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(DiscussionsPage), App.Settings.SignedInUserName);
                    break;
                case "repositories":
                    NavViewFrameTitleTextBlock.Text = "Your Repositories";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(RepositoriesPage), App.Settings.SignedInUserName);
                    break;
                case "organizations":
                    NavViewFrameTitleTextBlock.Text = "Your Organizations";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(OrganizationsPage), App.Settings.SignedInUserName);
                    break;
                case "starred":
                    NavViewFrameTitleTextBlock.Text = "Your Starred Repositories";
                    NavViewFrameTitleTextBlock.Visibility = Visibility.Visible;
                    HomeContentFrame.Navigate(typeof(StarredReposPage), App.Settings.SignedInUserName);
                    break;
            }
        }
    }
}
