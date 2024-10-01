// Copyright (c) 2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.Octokit.Queries.Repositories;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
	public class ChecksViewModel : BaseViewModel
	{
		private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
		public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

		private PullRequest pullItem;
		public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

		private readonly ObservableCollection<CheckSuite> _items;
		public ReadOnlyObservableCollection<CheckSuite> Items { get; }

		private CheckRun _selectedCheckRun;
		public CheckRun SelectedCheckRun { get => _selectedCheckRun; set => SetProperty(ref _selectedCheckRun, value); }

		public IAsyncRelayCommand LoadRepositoryPullRequestChecksPageCommand { get; }

		public ChecksViewModel() : base()
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
			PullRequestCheckQueries queries = new();
			var response = await queries.GetAllAsync(owner, name, Number);

			// Remove elements that doesn't have any CheckRuns
			response.RemoveAll(p => p.CheckRuns.Nodes.Count == 0);

			foreach (var item in response)
			{
				_items.Add(item);
			}
		}

		private async Task LoadPullRequestAsync(string owner, string name)
		{
			PullRequestQueries queries = new();
			PullItem = await queries.GetAsync(owner, name, Number);

			PullRequestOverviewViewModel = new()
			{
				PullRequest = PullItem,
				SelectedTag = "checks",
			};
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
