using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.ViewModels.Controls.Overview;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
	public class PullRequestsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<PullBlockButtonViewModel> _pullRequests;
		public ReadOnlyObservableCollection<PullBlockButtonViewModel> PullItems { get; }

		public ObservableCollection<string> StateFilterOptions { get; } = ["Open", "Closed", "All"];

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

		public ObservableCollection<string> LabelFilterOptions { get; } = ["All labels", "No labels"];

		public ObservableCollection<string> AuthorFilterOptions { get; } = ["All authors"];

		public ObservableCollection<string> AssigneeFilterOptions { get; } = ["All assignees", "Unassigned"];

		public ObservableCollection<string> MilestoneFilterOptions { get; } = ["All milestones", "No milestone"];

		private RepositoryItemListFilters _filters = new();

		public IAsyncRelayCommand LoadRepositoryPullRequestsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryPullRequestsFurtherCommand { get; }

		public PullRequestsViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			_pullRequests = new();
			PullItems = new(_pullRequests);

			LoadRepositoryPullRequestsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestsPageAsync);
			LoadRepositoryPullRequestsFurtherCommand = new AsyncRelayCommand(LoadRepositoryPullRequestsFurtherAsync);
		}

		private async Task LoadRepositoryPullRequestsPageAsync()
		{
			SetTabInformation("Pull requests", "Pull requests", "PullRequests");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryPullRequestsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadFilterOptionsAsync);
				await LoadFilterOptionsAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryPullRequestsAsync);
				await LoadRepositoryPullRequestsAsync(Login, Name);

				SetTabInformation($"Pull requests \u2022 {Login}/{Name}", $"Pull requests \u2022 {Login}/{Name}");

				IsEmpty = PullItems.Count == 0;
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;

				if (PullItems.Count == 0)
					IsEmpty = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadRepositoryPullRequestsAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.PullRequests;

			var result = await queries.GetPageAsync(owner, name, PageRequest.Forward(20), _filters);

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

		private async Task LoadFilterOptionsAsync(string owner, string name)
		{
			var repositories = _gitHub.Repositories.Repositories;
			var pullRequests = _gitHub.Repositories.PullRequests;
			var optionsTask = repositories.GetIssueListOptionsAsync(owner, name);
			var authorsTask = pullRequests.GetAuthorLoginsAsync(owner, name);

			await Task.WhenAll(optionsTask, authorsTask);

			var options = await optionsTask;
			ReplaceOptions(
				LabelFilterOptions,
				["All labels", "No labels"],
				options.Labels?.Nodes?.OfType<Label>().Select(label => label.Name) ?? []);
			ReplaceOptions(
				AuthorFilterOptions,
				["All authors"],
				await authorsTask);
			ReplaceOptions(
				AssigneeFilterOptions,
				["All assignees", "Unassigned"],
				options.AssignableUsers?.Nodes?.OfType<User>().Select(user => user.Login) ?? []);
			ReplaceOptions(
				MilestoneFilterOptions,
				["All milestones", "No milestone"],
				options.Milestones?.Nodes?.OfType<Milestone>().Select(milestone => milestone.Title) ?? []);
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
				await LoadRepositoryPullRequestsAsync(Login, Name);
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

		private async Task LoadRepositoryPullRequestsFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Repositories.PullRequests;

				var result = await queries.GetPageAsync(
					Login,
					Name,
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

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
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
			foreach (var item in options)
			{
				target.Add(item);
			}
		}
	}
}
