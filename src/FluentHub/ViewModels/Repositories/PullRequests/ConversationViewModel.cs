// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
	public class ConversationViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel = default!;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem = default!;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<object> _timelineItems;
		public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

		public IAsyncRelayCommand LoadRepositoryPullRequestConversationPageCommand { get; }

		public ConversationViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText ?? string.Empty;
			Name = parameter.SecondaryText ?? string.Empty;
			Number = parameter.Number;

			_timelineItems = new();
			TimelineItems = new(_timelineItems);

			LoadRepositoryPullRequestConversationPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestConversationPageAsync);
		}

		private async Task LoadRepositoryPullRequestConversationPageAsync()
		{
			SetTabInformation("Pull request", "Pull request", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestConversationPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommentsAsync);
				await LoadRepositoryPullRequestCommentsAsync(Login, Name);

				SetTabInformation($"{PullItem.Title}", $"{PullItem.Title}");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
				PullRequestOverviewViewModel.Loaded = true;
			}
		}

		private async Task LoadRepositoryPullRequestCommentsAsync(string owner, string name)
		{
			var pullRequestQueries = _gitHub.Repositories.PullRequests;
			var queries = _gitHub.Repositories.PullRequestEvents;
			_timelineItems.Clear();

			// Get pull request body comment
			var bodyComment = await pullRequestQueries.GetBodyAsync(owner, name, Number);
			_timelineItems.Add(bodyComment);

			// Get all pull request event timeline items
			var pullEvents = await queries.GetAllAsync(owner, name, Number);
			foreach (var item in pullEvents)
				_timelineItems.Add(item);
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequests;
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "conversation",
			};
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
