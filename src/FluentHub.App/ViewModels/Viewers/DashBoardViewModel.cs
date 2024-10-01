// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Dialogs;
using FluentHub.App.ViewModels.UserControls.FeedBlocks;
using FluentHub.Octokit.Queries.Users;
using System.Windows.Input;

namespace FluentHub.App.ViewModels.Viewers
{
	public class DashBoardViewModel : BaseViewModel
	{
		private readonly ObservableCollection<Repository> _TopRepositories;
		public ReadOnlyObservableCollection<Repository> TopRepositories { get; }

		private readonly ObservableCollection<Notification> _RecentActivities;
		public ReadOnlyObservableCollection<Notification> RecentActivities { get; }

		private readonly ObservableCollection<ActivityBlockViewModel> _Feeds;
		public ReadOnlyObservableCollection<ActivityBlockViewModel> Feeds { get; }

		public ICommand CreateNewRepositoryCommand { get; }
		public ICommand GoToSidebarRepositoryCommand { get; }
		public ICommand GoToSidebarActivityCommand { get; }
		public ICommand LoadUserHomePageCommand { get; }

		public DashBoardViewModel()
		{
			_TopRepositories = new();
			TopRepositories = new(_TopRepositories);

			_RecentActivities = new();
			RecentActivities = new(_RecentActivities);

			_Feeds = new();
			Feeds = new(_Feeds);

			CreateNewRepositoryCommand = new AsyncRelayCommand(CreateNewRepository);
			GoToSidebarRepositoryCommand = new RelayCommand<Repository>(GoToSidebarRepository);
			GoToSidebarActivityCommand = new RelayCommand<Notification>(GoToSidebarActivity);
			LoadUserHomePageCommand = new AsyncRelayCommand(LoadUserHomePageAsync);
		}

		private async Task LoadUserHomePageAsync()
		{
			SetTabInformation("Dashboard", "Dashboard", "Home");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserHomePageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadHomeContentsAsync);
				await LoadHomeContentsAsync();

				SetTabInformation("Dashboard", "Dashboard");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadHomeContentsAsync()
		{
			RepositoryQueries repositoryQueries = new();

			var repositoryResult = await repositoryQueries.GetAllAsync(App.AppSettings.SignedInUserName, 20);
			if (repositoryResult.Response is null || repositoryResult.PageInfo is null)
				return;

			var items = (List<Repository>)repositoryResult.Response;

			_TopRepositories.Clear();

			if (items.Count < 6)
			{
				foreach (var item in items)
					_TopRepositories.Add(item);
			}
			else
			{
				foreach (var item in items.GetRange(0, 6))
					_TopRepositories.Add(item);
			}

			NotificationQueries notificationQueries = new();
			var notificationResponse = await notificationQueries.GetAllAsync(
				new() { All = true },
				new()
				{
					PageCount = 1, // Constant
					PageSize = 30,
					StartPage = 1,
				});

			// Filter the notifications; remove closed Issue & Pull Requests
			notificationResponse.RemoveAll(x =>
				x.Subject.Type != NotificationSubjectType.IssueOpen &&
				x.Subject.Type != NotificationSubjectType.PullRequestOpen);

			_RecentActivities.Clear();

			if (notificationResponse.Count < 8)
			{
				foreach (var item in notificationResponse)
					_RecentActivities.Add(item);
			}
			else
			{
				foreach (var item in notificationResponse.GetRange(0, 8))
					_RecentActivities.Add(item);
			}

			ActivityQueries activityQueries = new();

			var activityResponse = await activityQueries.GetAllAsync(App.AppSettings.SignedInUserName);
			if (activityResponse == null)
				return;

			foreach (var item in activityResponse.Where(x =>
				//x.Type == ActivityPayloadType.CheckRunEvent ||
				//x.Type == ActivityPayloadType.CheckSuiteEvent ||
				//x.Type == ActivityPayloadType.CommitComment ||
				//x.Type == ActivityPayloadType.CreateEvent ||
				//x.Type == ActivityPayloadType.DeleteEvent ||
				x.Type == ActivityPayloadType.ForkEvent ||
				x.Type == ActivityPayloadType.IssueCommentEvent ||
				x.Type == ActivityPayloadType.IssueEvent ||
				x.Type == ActivityPayloadType.PullRequestComment ||
				x.Type == ActivityPayloadType.PullRequestEvent ||
				//x.Type == ActivityPayloadType.PullRequestReviewEvent||
				//x.Type == ActivityPayloadType.PushEvent||
				//x.Type == ActivityPayloadType.PushWebhookCommit||
				//x.Type == ActivityPayloadType.PushWebhookCommitter||
				//x.Type == ActivityPayloadType.PushWebhook ||
				x.Type == ActivityPayloadType.ReleaseEvent ||
				x.Type == ActivityPayloadType.WatchEvent /*||
				x.Type == ActivityPayloadType.StatusEvent*/).ToList())
			{
				ActivityBlockViewModel viewModel = new()
				{
					Payload = item,
				};

				_Feeds.Add(viewModel);
			}
		}

		private async Task CreateNewRepository()
		{
			var dialog = new CreateNewRepositoryDialog()
			{
				XamlRoot = MainWindow.Instance.Content.XamlRoot,
			};

			var result = await dialog.ShowAsync();
		}

		private void GoToSidebarRepository(Repository? repo)
		{
			if (repo is null)
				return;

			var navBar = _navigation.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = repo.Owner.Login,
				SecondaryText = repo.Name,
			};

			if (App.AppSettings.UseDetailsView)
				_navigation.Navigate<Views.Repositories.Code.DetailsLayoutView>();
			else
				_navigation.Navigate<Views.Repositories.Code.TreeLayoutView>();
		}

		private void GoToSidebarActivity(Notification? notification)
		{
			if (notification is null)
				return;

			var navBar = _navigation.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = notification.Repository.Owner.Login,
				SecondaryText = notification.Repository.Name,
				Number = notification.Subject.Number,
			};

			switch (notification.Subject.Type)
			{
				case NotificationSubjectType.IssueClosedAsCompleted:
				case NotificationSubjectType.IssueClosedAsNotPlanned:
				case NotificationSubjectType.IssueOpen:
					_navigation.Navigate<Views.Repositories.Issues.IssuePage>();
					break;
				case NotificationSubjectType.PullRequestOpen:
				case NotificationSubjectType.PullRequestClosed:
				case NotificationSubjectType.PullRequestMerged:
				case NotificationSubjectType.PullRequestDraft:
					_navigation.Navigate<Views.Repositories.PullRequests.ConversationPage>();
					break;
			}
		}
	}
}
