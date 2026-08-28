// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Models;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;

namespace FluentHub.Features.Users.ViewModels
{
	public class PullRequestsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public ObservableCollection<string> StateFilterOptions { get; } = ["Open", "Closed", "All"];

		public ObservableCollection<string> LabelFilterOptions { get; } = ["All labels", "No labels"];

		public ObservableCollection<string> AssigneeFilterOptions { get; } = ["All assignees", "Unassigned"];

		public ObservableCollection<string> MilestoneFilterOptions { get; } = ["All milestones", "No milestone"];

		public ObservableCollection<string> SortFilterOptions { get; } =
		[
			"Newest",
			"Oldest",
			"Most commented",
			"Least commented",
			"Recently updated",
			"Least recently updated",
			"Best match",
			"Most 👍 reactions",
			"Most 👎 reactions",
			"Most 😄 reactions",
			"Most 🎉 reactions",
			"Most 😕 reactions",
			"Most ❤️ reactions",
			"Most 🚀 reactions",
			"Most 👀 reactions",
		];

		private RepositoryItemListFilters _filters = new();

		public IAsyncRelayCommand LoadUserPullRequestsPageCommand { get; }
		public IAsyncRelayCommand LoadUserPullRequestsFurtherCommand { get; }

		public PullRequestsViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			AsViewer = CurrentRoute is UserRoute { AsViewer: true };

			_pullRequests = new();
			PullItems = new(_pullRequests);

			LoadUserPullRequestsPageCommand = new AsyncRelayCommand(LoadUserPullRequestsPageAsync);
			LoadUserPullRequestsFurtherCommand = new AsyncRelayCommand(LoadUserPullRequestsFurtherAsync);
		}

		private async Task LoadUserPullRequestsPageAsync()
		{
			SetTabInformation("Pull Requests", "Pull Requests", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();
			_filters = new RepositoryItemListFilters();

			_currentTaskingMethodName = nameof(LoadUserPullRequestsPageAsync);

			try
			{
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadFilterOptionsAsync(Login),
					LoadUserPullRequestsAsync(Login));

				SetTabInformation("Pull Requests", "Pull Requests");

				IsEmpty = PullItems.Count == 0;
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

		private async Task LoadUserPullRequestsAsync(string login)
		{
			var queries = _gitHub.Users.PullRequests;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20), _filters);

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_pullRequests.Clear();
			foreach (var item in items)
			{
				PullBlockButtonViewModel viewModel = new()
				{
					PullItem = item,
				};

				_pullRequests.Add(viewModel);
			}
		}

		private async Task LoadFilterOptionsAsync(string login)
		{
			var options = await _gitHub.Users.PullRequests.GetFilterOptionsAsync(login);
			ReplaceOptions(LabelFilterOptions, ["All labels", "No labels"], options.Labels);
			ReplaceOptions(AssigneeFilterOptions, ["All assignees", "Unassigned"], options.Assignees);
			ReplaceOptions(MilestoneFilterOptions, ["All milestones", "No milestone"], options.Milestones);
		}

		public async Task ApplyFiltersAsync(RepositoryItemListFilters filters)
		{
			ArgumentNullException.ThrowIfNull(filters);

			_filters = filters;
			InitializeNodePagingInfo();
			SetLoadingProgress(true);
			_currentTaskingMethodName = nameof(ApplyFiltersAsync);

			try
			{
				await LoadUserPullRequestsAsync(Login);
				IsEmpty = PullItems.Count == 0;
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

		private async Task LoadUserPullRequestsFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.PullRequests;

				var result = await queries.GetPageAsync(
					Login,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					_filters);

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					PullBlockButtonViewModel viewModel = new()
					{
						PullItem = item,
					};

					_pullRequests.Add(viewModel);
				}
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

		private static void ReplaceOptions(
			ObservableCollection<string> target,
			IEnumerable<string> defaults,
			IEnumerable<string> values)
		{
			var options = defaults.Concat(values)
				.Where(value => !string.IsNullOrWhiteSpace(value))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();
			if (target.SequenceEqual(options, StringComparer.Ordinal))
				return;

			target.Clear();
			foreach (var option in options)
				target.Add(option);
		}
	}
}
