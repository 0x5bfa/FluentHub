using FluentHub.Services;
using FluentHub.ViewModels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Octokit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories
{
    public sealed partial class OverviewPage : Windows.UI.Xaml.Controls.Page
    {
        public OverviewPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetService<INavigationService>();
        }

        private long RepoId { get; set; }
        private Repository Repository { get; set; }

        private readonly INavigationService navigationService;

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
            var commonRepoViewModel = new CommonRepoViewModel();

            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Code":
                    commonRepoViewModel.RepositoryId = RepoId;
                    commonRepoViewModel.IsRootDir = true;
                    commonRepoViewModel.Name = Repository.Name;
                    commonRepoViewModel.Owner = Repository.Owner.Login;
                    commonRepoViewModel.BranchName = Repository.DefaultBranch;
                    RepoPageNavViewFrame.Navigate(typeof(CodePage), commonRepoViewModel);
                    break;
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(IssuesPage), RepoId.ToString());
                    break;
                case "PRs":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequestsPage), RepoId.ToString());
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
                Type type = null;
                object args = Repository.Owner.Login;
                if (Repository.Owner.Type == AccountType.User)
                {
                    type = typeof(Users.ProfilePage);
                }
                else
                {
                    type = typeof(Organizations.ProfilePage);
                }
                navigationService.Navigate(type, args);
            }
        }

        private async Task SetRepoInfo()
        {
            RepoOwnerName.Text = Repository.Owner.Login;

            RepoName.Text = Repository.Name;

            // Avatar block will be replaced by user control
            if (Repository.Owner.Type == AccountType.Organization)
            {
                RepoOwnerAvatarBorder.CornerRadius = new CornerRadius(6);
            }

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

            var commonRepoViewModel = new CommonRepoViewModel();

            commonRepoViewModel.RepositoryId = RepoId;
            commonRepoViewModel.IsRootDir = true;
            commonRepoViewModel.Name = Repository.Name;
            commonRepoViewModel.Owner = Repository.Owner.Login;
            commonRepoViewModel.BranchName = Repository.DefaultBranch;

            RepoPageNavViewFrame.Navigate(typeof(CodePage), commonRepoViewModel);
        }
    }
}
