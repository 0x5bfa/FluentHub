using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Users
{
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ProfilePageViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public ProfilePageViewModel ViewModel { get; }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{DataContext}'s profile";
            currentItem.Description = $"{DataContext}'s profile";
            currentItem.Url = $"https://github.com/{DataContext}";
            currentItem.Icon = new muxc.FontIconSource
            {
                Glyph = "\uE77B"
            };

            // This is a workaround. TODO: Implement ItemToVisibility converter
            // Wait for the command to finish executing before updating visibility
            var command = ViewModel.LoadUserCommand;
            if (command.CanExecute(DataContext))
                await command.ExecuteAsync(DataContext);
            UpdateVisibility();            
        }

        private void UserNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            _ = args.SelectedItemContainer.Tag.ToString() switch
            {
                "Overview" => UserNavViewContent.Navigate(typeof(OverviewPage), DataContext, args.RecommendedNavigationTransitionInfo),
                "Repositories" => UserNavViewContent.Navigate(typeof(RepositoriesPage), DataContext, args.RecommendedNavigationTransitionInfo),
                "Stars" => UserNavViewContent.Navigate(typeof(StarredReposPage), DataContext, args.RecommendedNavigationTransitionInfo),
                "Followers" => UserNavViewContent.Navigate(typeof(FollowersPage), DataContext, args.RecommendedNavigationTransitionInfo),
                "Following" => UserNavViewContent.Navigate(typeof(FollowingPage), DataContext, args.RecommendedNavigationTransitionInfo),
                _ => UserNavViewContent.Navigate(typeof(OverviewPage), DataContext, args.RecommendedNavigationTransitionInfo)
            };
        }

        private void UpdateVisibility()
        {
            if (!string.IsNullOrEmpty(CompanyLinkTextBlock.Text))
            {
                CompanyBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(UserBioTextBlock.Text))
            {
                UserBioTextBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LocationTextBlock.Text))
            {
                LocationBlock.Visibility = Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(LinkButton.Content as string))
            {
                var uri = new UriBuilder(LinkButton.Content as string).Uri;
                LinkButton.NavigateUri = uri;
                LinkBlock.Visibility = Visibility.Visible;
            }
        }

        private void UserFollowersButton_Click(object sender, RoutedEventArgs e)
        {
            UserNavViewItemFollowers.IsSelected = true;
        }

        private void UserFollowingButton_Click(object sender, RoutedEventArgs e)
        {
            UserNavViewItemFollowing.IsSelected = true;
        }
    }
}