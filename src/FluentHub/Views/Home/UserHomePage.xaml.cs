using FluentHub.Services;
using FluentHub.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Home
{
    public sealed partial class UserHomePage : Page
    {
        public UserHomePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<UserHomeViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public UserHomeViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Home";
            currentItem.Description = $"Home";
            currentItem.Url = $"fluenthub://home";
            currentItem.DisplayUrl = $"Home";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Home.targetsize-96.png"))
            };

            var command = ViewModel.LoadHomeContentsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnHomeRepositoriesListItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Repository;
            navigationService.Navigate<Repositories.OverviewPage>($"{App.DefaultGitHubDomain}/{clickedItem.Owner.Login}/{clickedItem.Name}");
        }

        private void OnIssueButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Users.IssuesPage>("https://github.com/issues");

        private void OnPullRequestsButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Users.PullRequestsPage>("https://github.com/pulls");

        private void OnDiscussionsButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Users.DiscussionsPage>("fluenthub://discussions");

        private void OnRepositoriesButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Users.RepositoriesPage>("fluenthub://repositories");

        private void OnOrganizationsButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Users.OrganizationsPage>("fluenthub://organizations");

        private void OnStarsButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Users.StarredReposPage>("fluenthub://stars");

        private void OnMoreActivitiesButtonClick(object sender, RoutedEventArgs e)
            => navigationService.Navigate<Home.ActivitiesPage>("fluenthub://activities");
    }
}
