using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.Overview
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
                .OfType<NavigationViewItem>()
                .FirstOrDefault();

            UserProfileNavView.SelectedItem
                = UserProfileNavView
                .MenuItems
                .OfType<NavigationViewItem>()
                .FirstOrDefault(x => string.Compare(x.Tag.ToString(), tag?.ToString(), true) == 0)
                ?? defaultItem;
        }

        private void OnUserNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var param = new Models.FrameNavigationParameter()
            {
                Login = ViewModel.User.Login,
            };

            switch (args.InvokedItemContainer.Tag.ToString().ToLower())
            {
                case "overview":
                    navService.Navigate<Views.Users.OverviewPage>(param);
                    break;
                case "repositories":
                    navService.Navigate<Views.Users.RepositoriesPage>(param);
                    break;
                case "stars":
                    navService.Navigate<Views.Users.StarredReposPage>(param);
                    break;
                case "projects":
                    navService.Navigate<Views.Users.ProjectsPage>(param);
                    break;
                case "packages":
                    navService.Navigate<Views.Users.PackagesPage>(param);
                    break;
                case "organizations":
                    navService.Navigate<Views.Users.OrganizationsPage>(param);
                    break;
                case "discussions":
                    navService.Navigate<Views.Users.DiscussionsPage>(param);
                    break;
                case "followers":
                    navService.Navigate<Views.Users.FollowersPage>(param);
                    break;
                case "following":
                    navService.Navigate<Views.Users.FollowingPage>(param);
                    break;
            }
        }

        private void OnUserFollowersButtonClick(object sender, RoutedEventArgs e)
        {
            SelectItemByTag("followers");
            navService.Navigate<Views.Users.FollowersPage>(
            new Models.FrameNavigationParameter()
            {
                Login = ViewModel.User.Login,
            });
        }

        private void OnUserFollowingButtonClick(object sender, RoutedEventArgs e)
        {
            SelectItemByTag("following");
            navService.Navigate<Views.Users.FollowingPage>(
            new Models.FrameNavigationParameter()
            {
                Login = ViewModel.User.Login,
            });
        }

        private async void OnEditProfileButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialogs.UserProfileEditor(ViewModel.User.Login);

            // https://github.com/microsoft/microsoft-ui-xaml/issues/2504
            dialog.XamlRoot = this.Content.XamlRoot;

            _ = await dialog.ShowAsync();
        }

        private void LocationHyperlink()
        {
            var LocationHyperlink = "https://www.bing.com/maps?q=" + ViewModel.User.Location;
        }
    }
}
