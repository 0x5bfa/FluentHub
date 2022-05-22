using FluentHub.Services;
using FluentHub.ViewModels.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
            // e.g.) https://github.com/onein528
            string url = e.Parameter as string;
            var uri = new Uri(url);
            string login = uri.Segments[1];
            DataContext = login;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{login}'s profile";
            currentItem.Description = $"{login}'s profile";
            currentItem.Url = url;
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Profile.png"))
            };

            //var defaultItem = UserNavView
            //            .MenuItems
            //            .OfType<muxc.NavigationViewItem>()
            //            .FirstOrDefault();

            //UserNavView.SelectedItem = UserNavView
            //                               .MenuItems
            //                               .OfType<muxc.NavigationViewItem>()
            //                               .FirstOrDefault(x => string.Compare(x.Tag.ToString(), e.Parameter?.ToString(), true) == 0)
            //                               ?? defaultItem;

            var command = ViewModel.RefreshUserCommand;
            if (command.CanExecute(login))
                command.Execute(login);
        }

        private void UserNavView_SelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            // workaround
            string url = $"{App.DefaultGitHubDomain}/{DataContext}";

            _ = args.SelectedItemContainer.Tag.ToString() switch
            {
                "Overview" => UserNavViewContent.Navigate(typeof(OverviewPage), $"{url}", args.RecommendedNavigationTransitionInfo),
                "Repositories" => UserNavViewContent.Navigate(typeof(RepositoriesPage), $"{url}?tab=repositories", args.RecommendedNavigationTransitionInfo),
                "Stars" => UserNavViewContent.Navigate(typeof(StarredReposPage), $"{url}?tab=stars", args.RecommendedNavigationTransitionInfo),
                "Followers" => UserNavViewContent.Navigate(typeof(FollowersPage), $"{url}?tab=followers", args.RecommendedNavigationTransitionInfo),
                "Following" => UserNavViewContent.Navigate(typeof(FollowingPage), $"{url}?tab=following", args.RecommendedNavigationTransitionInfo),
                _ => UserNavViewContent.Navigate(typeof(OverviewPage), DataContext, args.RecommendedNavigationTransitionInfo)
            };
        }

        private void UserFollowersButton_Click(object sender, RoutedEventArgs e)
            => UserNavViewItemFollowers.IsSelected = true;

        private void UserFollowingButton_Click(object sender, RoutedEventArgs e)
            => UserNavViewItemFollowing.IsSelected = true;

        private async void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.UserProfileEditor();
            _ = await dialog.ShowAsync();
        }
    }
}
