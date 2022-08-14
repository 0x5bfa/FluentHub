using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.Views.Home
{
    public sealed partial class UserHomePage : Page
    {
        public UserHomePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<UserHomeViewModel>();
            navService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navService;
        public UserHomeViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetCurrentTabItem();

            var command = ViewModel.LoadHomeContentsCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void SetCurrentTabItem()
        {
            var currentItem = navService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"Home";
            currentItem.Description = $"Home";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Home.png"))
            };
        }

        private void OnHomeRepositoriesListItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Repository;

            navService.Navigate<Repositories.Code.CodePage>(
                new FrameNavigationArgs()
                {
                    Login = clickedItem.Owner.Login,
                    Name = clickedItem.Name,
                });
        }

        private void OnOneFolderCardClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as Button;

            switch (clickedButton.Tag)
            {
                case "issues":
                    navService.Navigate<Users.IssuesPage>(
                    new FrameNavigationArgs()
                    {
                        Login = App.Settings.SignedInUserName,
                        Parameters = new() { "AsViewer" },
                    });
                    break;
                case "pullrequests":
                    navService.Navigate<Users.PullRequestsPage>(
                    new FrameNavigationArgs()
                    {
                        Login = App.Settings.SignedInUserName,
                        Parameters = new() { "AsViewer" },
                    });
                    break;
                case "discussions":
                    navService.Navigate<Users.DiscussionsPage>(
                    new FrameNavigationArgs()
                    {
                        Login = App.Settings.SignedInUserName,
                        Parameters = new() { "AsViewer" },
                    });
                    break;
                case "repositories":
                    navService.Navigate<Users.RepositoriesPage>(
                    new FrameNavigationArgs()
                    {
                        Login = App.Settings.SignedInUserName,
                        Parameters = new() { "AsViewer" },
                    });
                    break;
                case "organizations":
                    navService.Navigate<Users.OrganizationsPage>(
                    new FrameNavigationArgs()
                    {
                        Login = App.Settings.SignedInUserName,
                        Parameters = new() { "AsViewer" },
                    });
                    break;
                case "stars":
                    navService.Navigate<Users.StarredReposPage>(
                    new FrameNavigationArgs()
                    {
                        Login = App.Settings.SignedInUserName,
                        Parameters = new() { "AsViewer" },
                    });
                    break;
            }
        }

        private void OnSeeFeedsButtonClick(object sender, RoutedEventArgs e)
            => navService.Navigate<ActivitiesPage>();

        private void OnRecentUserNotificationListViewItemClick(object sender, ItemClickEventArgs e)
        {
            var notification = e.ClickedItem as Notification;

            switch (notification.Subject.Type)
            {
                case NotificationSubjectType.IssueClosedAsCompleted:
                case NotificationSubjectType.IssueClosedAsNotPlanned:
                case NotificationSubjectType.IssueOpen:
                    navService.Navigate<Repositories.Issues.IssuePage>(
                    new FrameNavigationArgs()
                    {
                        Login = notification.Repository.Owner.Login,
                        Name = notification.Repository.Name,
                        Number = notification.Subject.Number,
                    });
                    break;
                case NotificationSubjectType.PullRequestClosed:
                case NotificationSubjectType.PullRequestDraft:
                case NotificationSubjectType.PullRequestMerged:
                case NotificationSubjectType.PullRequestOpen:
                    navService.Navigate<Repositories.PullRequests.ConversationPage>(
                    new FrameNavigationArgs()
                    {
                        Login = notification.Repository.Owner.Login,
                        Name = notification.Repository.Name,
                        Number = notification.Subject.Number,
                    });
                    break;
            }
        }
    }
}
