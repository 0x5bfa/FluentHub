using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.Views.Repositories.Codes;
using FluentHub.Views.Repositories.Issues;
using FluentHub.Views.Repositories.PullRequests;
using FluentHub.ViewModels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Repositories
{
    public sealed partial class OverviewPage : Page
    {
        public OverviewPage()
        {
            InitializeComponent();
            navigationService = App.Current.Services.GetService<INavigationService>();
            MainPageViewModel.RepositoryContentFrame.Navigating += OnRepositoryContentFrameNavigating;
        }

        private readonly INavigationService navigationService;
        private Octokit.Models.Repository Repository { get; set; }
        OverviewViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Repository = Repository = e.Parameter as Octokit.Models.Repository;
            this.DataContext = ViewModel;

            // Initialize frame
            var repoContextViewModel = new RepoContextViewModel()
            {
                Repository = Repository,
                IsRootDir = true,
                Name = Repository.Name,
                Owner = Repository.Owner,
                BranchName = Repository.DefaultBranchName,
            };

            RepoPageNavViewFrame.Navigate(typeof(CodePage), repoContextViewModel);
        }

        private void RepoPageNavView_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Code":
                    {
                        var repoContextViewModel = new RepoContextViewModel()
                        {
                            Repository = Repository,
                            IsRootDir = true,
                            Name = Repository.Name,
                            Owner = Repository.Owner,
                            BranchName = Repository.DefaultBranchName,
                        };

                        RepoPageNavViewFrame.Navigate(typeof(CodePage), repoContextViewModel);
                        break;
                    }
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(IssuesPage), $"{Repository.Owner}/{Repository.Name}");
                    break;
                case "PRs":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequestsPage), $"{Repository.Owner}/{Repository.Name}");
                    break;
                case "Settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings), $"{Repository.Owner}/{Repository.Name}");
                    break;
            }
        }

        private void OnRepoOwnerButtonClick(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            if (ViewModel.Repository.IsInOrganization)
            {
                service.Navigate<Views.Organizations.ProfilePage>(ViewModel.Repository.Owner);
            }
            else
            {
                service.Navigate<Views.Users.ProfilePage>(ViewModel.Repository.Owner);
            }
        }

        private void OnRepositoryContentFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;

            RepoPageNavViewFrame.Navigate(e.SourcePageType, e.Parameter);
        }
    }
}
