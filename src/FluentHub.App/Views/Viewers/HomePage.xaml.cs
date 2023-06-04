using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Viewers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Viewers
{
    public sealed partial class UserHomePage : Page
    {
        public UserHomePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<UserHomeViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService _navigation;
        public UserHomeViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var command = ViewModel.LoadUserHomePageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }

        private void OnHomeRepositoriesListItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Repository;

            var param = new FrameNavigationParameter()
            {
                UserLogin = clickedItem.Owner.Login,
                RepositoryName = clickedItem.Name,
            };

            if (App.AppSettings.UseDetailsView)
                _navigation.Navigate<Repositories.Code.DetailsLayoutView>(param);
            else
                _navigation.Navigate<Repositories.Code.TreeLayoutView>(param);
        }

        private void OnOneFolderCardClick(object sender, RoutedEventArgs e)
        {
            var parameter = _navigation.TabView.SelectedItem.NavigationBar.Parameter;
            parameter.UserLogin = App.AppSettings.SignedInUserName;
            parameter.AsViewer = true;

            var clickedButton = sender as Button;

            switch (clickedButton.Tag)
            {
                case "issues":
                    _navigation.Navigate<Users.IssuesPage>();
                    break;
                case "pullrequests":
                    _navigation.Navigate<Users.PullRequestsPage>();
                    break;
                case "discussions":
                    _navigation.Navigate<Users.DiscussionsPage>();
                    break;
                case "repositories":
                    _navigation.Navigate<Users.RepositoriesPage>();
                    break;
                case "organizations":
                    _navigation.Navigate<Users.OrganizationsPage>();
                    break;
                case "stars":
                    _navigation.Navigate<Users.StarredReposPage>();
                    break;
            }
        }

        private void OnSeeFeedsButtonClick(object sender, RoutedEventArgs e)
            => _navigation.Navigate<ActivitiesPage>();

		private void FeaturedNotificationsListItemButton_Click(object sender, RoutedEventArgs e)
		{
			var notification = (Notification)((Button)sender).Tag;

			switch (notification.Subject.Type)
			{
				case NotificationSubjectType.IssueClosedAsCompleted:
				case NotificationSubjectType.IssueClosedAsNotPlanned:
				case NotificationSubjectType.IssueOpen:
					_navigation.Navigate<Repositories.Issues.IssuePage>(
					new FrameNavigationParameter()
					{
						UserLogin = notification.Repository.Owner.Login,
						RepositoryName = notification.Repository.Name,
						Number = notification.Subject.Number,
					});
					break;
				case NotificationSubjectType.PullRequestClosed:
				case NotificationSubjectType.PullRequestDraft:
				case NotificationSubjectType.PullRequestMerged:
				case NotificationSubjectType.PullRequestOpen:
					_navigation.Navigate<Repositories.PullRequests.ConversationPage>(
					new FrameNavigationParameter()
					{
						UserLogin = notification.Repository.Owner.Login,
						RepositoryName = notification.Repository.Name,
						Number = notification.Subject.Number,
					});
					break;
			}
		}
	}
}
