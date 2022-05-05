using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{DataContext}'s profile";
            currentItem.Description = $"{DataContext}'s profile";
            currentItem.Url = $"https://github.com/{DataContext}";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Profile.png"))
            };

            var command = ViewModel.RefreshUserCommand;
            if (command.CanExecute(DataContext))
                command.Execute(DataContext);
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

        private void UserFollowersButton_Click(object sender, RoutedEventArgs e)
            => UserNavViewItemFollowers.IsSelected = true;

        private void UserFollowingButton_Click(object sender, RoutedEventArgs e)
            => UserNavViewItemFollowers.IsSelected = false;

        private async void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.UserProfileEditor();
            _ = await dialog.ShowAsync();
        }
    }
}
