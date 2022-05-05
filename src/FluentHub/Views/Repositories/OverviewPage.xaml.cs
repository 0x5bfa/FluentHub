using FluentHub.Services;
using FluentHub.ViewModels;
using FluentHub.Views.Repositories.Codes;
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
            MainPageViewModel.RepositoryContentFrame.Navigating += OnRepositoryContentFrameNavigating;
        }

        OverviewViewModel ViewModel = new();

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Repository = e.Parameter as Octokit.Models.Repository;
            DataContext = ViewModel;

            var command = ViewModel.LoadOverviewPageCommand;
            if (command.CanExecute(null))
                await command.ExecuteAsync(null);

            var repoContextViewModel = new RepoContextViewModel()
            {
                IsRootDir = true,
                Name = ViewModel.Repository.Name,
                Owner = ViewModel.Repository.Owner.Login,
                Repository = ViewModel.Repository,
                BranchName = ViewModel.Repository.DefaultBranchName ?? null,
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
                            IsRootDir = true,
                            Name = ViewModel.Repository.Name,
                            Owner = ViewModel.Repository.Owner.Login,
                            Repository = ViewModel.Repository,
                            BranchName = ViewModel.Repository.DefaultBranchName ?? null,
                        };

                        RepoPageNavViewFrame.Navigate(typeof(CodePage), repoContextViewModel);
                        break;
                    }
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(Issues.IssuesPage), $"{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}");
                    break;
                case "PullRequests":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequests.PullRequestsPage), $"{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}");
                    break;
                case "Discussions":
                    RepoPageNavViewFrame.Navigate(typeof(Discussions.DiscussionsPage), $"{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}");
                    break;
                case "Projects":
                    RepoPageNavViewFrame.Navigate(typeof(Projects.ProjectsPage), $"{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}");
                    break;
                case "Insights":
                    RepoPageNavViewFrame.Navigate(typeof(Insights.InsightsPage), $"{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}");
                    break;
                case "Settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings.SettingsPage), $"{ViewModel.Repository.Owner.Login}/{ViewModel.Repository.Name}");
                    break;
            }
        }

        private void OnRepoOwnerButtonClick(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            if (ViewModel.Repository.IsInOrganization)
            {
                service.Navigate<Views.Organizations.ProfilePage>(ViewModel.Repository.Owner.Login);
            }
            else
            {
                service.Navigate<Views.Users.ProfilePage>(ViewModel.Repository.Owner.Login);
            }
        }

        private void OnRepositoryContentFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;
            RepoPageNavViewFrame.Navigate(e.SourcePageType, e.Parameter);
        }
    }
}
