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

        private readonly INavigationService navigationService;
        private Octokit.Models.Repository Repository { get; set; }
        OverviewViewModel ViewModel = new();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Repository = e.Parameter as Octokit.Models.Repository;
            ViewModel.Repository = Repository;
            this.DataContext = ViewModel;
        }

        private void RepoPageNavView_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString())
            {
                case "Code":
                    {
                        var commonRepoViewModel = new CommonRepoViewModel()
                        {
                            IsRootDir = true,
                            Name = Repository.Name,
                            Owner = Repository.Owner,
                            BranchName = Repository.DefaultBranchName,
                        };

                        RepoPageNavViewFrame.Navigate(typeof(CodePage), commonRepoViewModel);
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
        }
    }
}
