using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.Overview
{
    public sealed partial class UserProfileOverview : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(UserProfileOverviewViewModel),
                typeof(UserProfileOverviewViewModel),
                new PropertyMetadata(null));

        public UserProfileOverviewViewModel ViewModel
        {
            get => (UserProfileOverviewViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                if (ViewModel is not null)
                    SelectItemByTag(ViewModel.SelectedTag);
            }
        }
        #endregion

        public UserProfileOverview()
        {
            InitializeComponent();
            navService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navService;

        private void SelectItemByTag(string tag)
        {
            var defaultItem
                = UserProfileNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault();

            UserProfileNavView.SelectedItem
                = UserProfileNavView
                .MenuItems
                .OfType<muxc.NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }

        private void OnUserNavViewItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItemContainer.Tag.ToString().ToLower())
            {
                case "overview":
                    navService.Navigate<Views.Users.OverviewPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "repositories":
                    navService.Navigate<Views.Users.RepositoriesPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "stars":
                    navService.Navigate<Views.Users.StarredReposPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "projects":
                    navService.Navigate<Views.Users.ProjectsPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "packages":
                    navService.Navigate<Views.Users.PackagesPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "organizations":
                    navService.Navigate<Views.Users.OrganizationsPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "discussions":
                    navService.Navigate<Views.Users.DiscussionsPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "followers":
                    navService.Navigate<Views.Users.FollowersPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
                case "following":
                    navService.Navigate<Views.Users.FollowingPage>(
                    new Models.FrameNavigationArgs()
                    {
                        Login = ViewModel.User.Login,
                    });
                    break;
            }
        }

        private void OnUserFollowersButtonClick(object sender, RoutedEventArgs e)
        {
            SelectItemByTag("followers");
            navService.Navigate<Views.Users.FollowersPage>(
            new Models.FrameNavigationArgs()
            {
                Login = ViewModel.User.Login,
            });
        }

        private void OnUserFollowingButtonClick(object sender, RoutedEventArgs e)
        {
            SelectItemByTag("following");
            navService.Navigate<Views.Users.FollowingPage>(
            new Models.FrameNavigationArgs()
            {
                Login = ViewModel.User.Login,
            });
        }

        private async void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.UserProfileEditor();
            _ = await dialog.ShowAsync();
        }

        private void LocationHyperlink()
        {
            var LocationHyperlink = "https://www.bing.com/maps?q=" + ViewModel.User.Location;
        }
    }
}
