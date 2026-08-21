// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Contracts;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
	public class ChecksViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel = default!;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem = default!;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<CheckSuite> _items;
		public ReadOnlyObservableCollection<CheckSuite> Items { get; }

		private CheckRun _selectedCheckRun = default!;
		public CheckRun SelectedCheckRun { get => _selectedCheckRun; set => SetProperty(ref _selectedCheckRun, value); }

		public IAsyncRelayCommand LoadRepositoryPullRequestChecksPageCommand { get; }

		public ChecksViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryPullRequestChecksPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestChecksPageAsync);
		}

		private async Task LoadRepositoryPullRequestChecksPageAsync()
		{
			SetTabInformation("Checks", "Checks", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestChecksPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadPullRequestAsync);
				await LoadPullRequestAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestChecksAsync);
				await LoadRepositoryPullRequestChecksAsync(Login, Name);

				SetTabInformation("Checks", "Checks");
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

		private async Task LoadRepositoryPullRequestChecksAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequestChecks;
			var response = await queries.GetAllAsync(owner, name, Number);

			// Remove elements that doesn't have any CheckRuns
			response.RemoveAll(p => p.CheckRuns?.Nodes?.Count is null or 0);

			foreach (var item in response)
			{
				_items.Add(item);
			}
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequests;
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "checks",
			};
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
