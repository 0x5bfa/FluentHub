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
                Owner = ViewModel.Repository.Owner,
                Repository = ViewModel.Repository,
            };

            if (ViewModel.RepositoryDetails != null)
            {
                repoContextViewModel.RepositoryDetails = ViewModel.RepositoryDetails;
                repoContextViewModel.BranchName = ViewModel.RepositoryDetails.DefaultBranchName;
            }

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
                            Owner = ViewModel.Repository.Owner,
                            Repository = ViewModel.Repository,
                        };

                        if (ViewModel.RepositoryDetails != null)
                        {
                            repoContextViewModel.RepositoryDetails = ViewModel.RepositoryDetails;
                            repoContextViewModel.BranchName = ViewModel.RepositoryDetails.DefaultBranchName;
                        }

                        RepoPageNavViewFrame.Navigate(typeof(CodePage), repoContextViewModel);
                        break;
                    }
                case "Issues":
                    RepoPageNavViewFrame.Navigate(typeof(IssuesPage), $"{ViewModel.Repository.Owner}/{ViewModel.Repository.Name}");
                    break;
                case "PRs":
                    RepoPageNavViewFrame.Navigate(typeof(PullRequestsPage), $"{ViewModel.Repository.Owner}/{ViewModel.Repository.Name}");
                    break;
                case "Settings":
                    RepoPageNavViewFrame.Navigate(typeof(Settings), $"{ViewModel.Repository.Owner}/{ViewModel.Repository.Name}");
                    break;
            }
        }

        private void OnRepoOwnerButtonClick(object sender, RoutedEventArgs e)
        {
            var service = App.Current.Services.GetRequiredService<INavigationService>();

            if (ViewModel.Repository.OwnerIsOrganization)
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
