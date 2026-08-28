// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Models;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;

namespace FluentHub.Features.Users.ViewModels
{
	public class IssuesViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

		public ObservableCollection<string> StateFilterOptions { get; } = ["Open", "Closed", "All"];

		public ObservableCollection<string> LabelFilterOptions { get; } = ["All labels", "No labels"];

		public ObservableCollection<string> IssueTypeFilterOptions { get; } = ["All types", "No type"];

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

		public IAsyncRelayCommand LoadUserIssuesPageCommand { get; }
		public IAsyncRelayCommand LoadUserIssuesFurtherCommand { get; }

		public IssuesViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			AsViewer = CurrentRoute is UserRoute { AsViewer: true };

			_issueItems = new();
			IssueItems = new(_issueItems);

			LoadUserIssuesPageCommand = new AsyncRelayCommand(LoadUserIssuesPageAsync);
			LoadUserIssuesFurtherCommand = new AsyncRelayCommand(LoadUserIssuesFurtherAsync);
		}

		private async Task LoadUserIssuesPageAsync()
		{
			SetTabInformation("Issues", "Issues", "Issues");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();
			_filters = new RepositoryItemListFilters();

			_currentTaskingMethodName = nameof(LoadUserIssuesPageAsync);

			try
			{
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadFilterOptionsAsync(Login),
					LoadUserIssuesAsync(Login));

				SetTabInformation("Issues", "Issues");

				IsEmpty = IssueItems.Count == 0;
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

		private async Task LoadUserIssuesAsync(string login)
		{
			var queries = _gitHub.Users.Issues;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20), _filters);

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_issueItems.Clear();
			foreach (var item in items)
			{
				IssueBlockButtonViewModel viewModel = new()
				{
					IssueItem = item,
				};

				_issueItems.Add(viewModel);
			}
		}

		private async Task LoadFilterOptionsAsync(string login)
		{
			var options = await _gitHub.Users.Issues.GetFilterOptionsAsync(login);
			ReplaceOptions(LabelFilterOptions, ["All labels", "No labels"], options.Labels);
			ReplaceOptions(IssueTypeFilterOptions, ["All types", "No type"], options.IssueTypes);
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
				await LoadUserIssuesAsync(Login);
				IsEmpty = IssueItems.Count == 0;
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

		private async Task LoadUserIssuesFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.Issues;

				var result = await queries.GetPageAsync(
					Login,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					_filters);

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					IssueBlockButtonViewModel viewmodel = new()
					{
						IssueItem = item,
					};

					_issueItems.Add(viewmodel);
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
