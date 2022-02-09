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

namespace FluentHub.Views.RepoPages
{
    public sealed partial class OverviewPage : Windows.UI.Xaml.Controls.Page
    {
        private long RepoId { get; set; }
        private Repository Repository { get; set; }

        public OverviewPage()
        {
            this.InitializeComponent();

            App.MainViewModel.RepoMainFrame.Navigating += RepoMainFrameNavigating;
        }

        private void RepoMainFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            RepoPageNavViewFrame.Navigate(e.SourcePageType, e.Parameter);

            e.Cancel = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Repository = await App.Client.Repository.Get(RepoId);

            if (Repository == null)
            {
                return;
            }

            await SetRepoInfo();
        }

        private void RepoPageNavView_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Code":
                    RepoPageNavViewFrame.Navigate(typeof(CodePage), RepoId.ToString());
                    break;
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(IssueListPage), RepoId.ToString());
                    break;
                case "PRs":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequestListPage), RepoId.ToString());
                    break;
                case "Settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings), RepoId.ToString());
                    break;
            }
        }

        private void OwnerButton_Click(object sender, RoutedEventArgs e)
        {
            if (Repository != null)
            {
                if (Repository.Owner.Type == AccountType.User)
                {
                    App.MainViewModel.MainFrame.Navigate(typeof(UserPages.ProfilePage), Repository.Owner.Login);
                }
                else
                {
                    App.MainViewModel.MainFrame.Navigate(typeof(OrganizationPages.ProfilePage), Repository.Owner.Login);
                }
            }
        }

        private async Task SetRepoInfo()
        {
            RepoOwnerName.Text = Repository.Owner.Login;

            RepoName.Text = Repository.Name;

            RepoOwnerAvatar.Source = new BitmapImage(new Uri(Repository.Owner.AvatarUrl));

            // I dare to do it like this. Don't change.
            WatchersCountBadge.Value = Repository.SubscribersCount;

            ForksCountBadge.Value = Repository.ForksCount;

            StargazersCountBadge.Value = Repository.StargazersCount;

            var pulls = await App.Client.PullRequest.GetAllForRepository(RepoId);

            if (Repository.OpenIssuesCount != 0)
            {
                IssuesCountBadge.Value = Repository.OpenIssuesCount - pulls.Count();
                IssuesCountBadge.Visibility = Visibility.Visible;
            }

            if (pulls.Count() != 0)
            {
                PullsCountBadge.Value = pulls.Count();
                PullsCountBadge.Visibility = Visibility.Visible;
            }

            RepoPageNavViewFrame.Navigate(typeof(CodePage), RepoId.ToString());
        }
    }
}
