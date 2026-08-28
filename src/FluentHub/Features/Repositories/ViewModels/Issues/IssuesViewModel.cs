using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Shared.Controls.ViewModels.Overview;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Mutations;

namespace FluentHub.Features.Repositories.ViewModels.Issues
{
	public class IssuesViewModel : BaseViewModel
	{
		private readonly ObservableCollection<IssueBlockButtonViewModel> _pinnedItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> PinnedItems { get; }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

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

		public ObservableCollection<string> IssueTypeFilterOptions { get; } = ["All types", "No type"];

		public ObservableCollection<string> AuthorFilterOptions { get; } = ["All authors"];

		public ObservableCollection<string> AssigneeFilterOptions { get; } = ["All assignees", "Unassigned"];

		public ObservableCollection<string> MilestoneFilterOptions { get; } = ["All milestones", "No milestone"];

		private RepositoryItemListFilters _filters = new();

		public IAsyncRelayCommand LoadRepositoryIssuesPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryIssuesFurtherCommand { get; }

		private bool _isIssueMutationRunning;
		public bool IsIssueMutationRunning
		{
			get => _isIssueMutationRunning;
			private set
			{
				if (SetProperty(ref _isIssueMutationRunning, value))
					OnPropertyChanged(nameof(CanCreateIssue));
			}
		}

		public bool CanCreateIssue
			=> !IsIssueMutationRunning
			&& Repository is not null
			&& Repository.HasIssuesEnabled
			&& !Repository.IsArchived;

		public IssuesViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			_issueItems = new();
			IssueItems = new(_issueItems);

			_pinnedItems = new();
			PinnedItems = new(_pinnedItems);

			LoadRepositoryIssuesPageCommand = new AsyncRelayCommand(LoadRepositoryIssuesPageAsync);
			LoadRepositoryIssuesFurtherCommand = new AsyncRelayCommand(LoadRepositoryIssuesFurtherAsync);
		}

		private async Task LoadRepositoryIssuesPageAsync()
		{
			SetTabInformation("Issues", "Issues", "Issues");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryIssuesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadFilterOptionsAsync);
				await LoadFilterOptionsAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryIssuesAsync);
				await LoadRepositoryIssuesAsync(Login, Name);

				SetTabInformation($"Issues \u2022 {Login}/{Name}", $"Issues \u2022 {Login}/{Name}");

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

		private async Task LoadRepositoryIssuesAsync(string owner, string name, bool loadPinned = true)
		{
			var queries = _gitHub.Repositories.Issues;

			var result = await queries.GetPageAsync(owner, name, PageRequest.Forward(20), _filters);

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

			if (!loadPinned)
				return;

			var pinnedIssues = await queries.GetPinnedAllAsync(owner, name);
			if (pinnedIssues == null)
				return;

			_pinnedItems.Clear();
			foreach (var item in pinnedIssues)
			{
				IssueBlockButtonViewModel viewModel = new()
				{
					IssueItem = item,
				};

				_pinnedItems.Add(viewModel);
			}
		}

		private async Task LoadFilterOptionsAsync(string owner, string name)
		{
			var repositories = _gitHub.Repositories.Repositories;
			var issues = _gitHub.Repositories.Issues;
			var optionsTask = repositories.GetIssueListOptionsAsync(owner, name);
			var authorsTask = issues.GetAuthorLoginsAsync(owner, name);
			var issueTypesTask = issues.GetIssueTypeNamesAsync(owner, name);

			await Task.WhenAll(optionsTask, authorsTask, issueTypesTask);

			var options = await optionsTask;
			ReplaceOptions(
				LabelFilterOptions,
				["All labels", "No labels"],
				options.Labels?.Nodes?.OfType<Label>().Select(label => label.Name) ?? []);
			ReplaceOptions(
				IssueTypeFilterOptions,
				["All types", "No type"],
				await issueTypesTask);
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
				await LoadRepositoryIssuesAsync(Login, Name, false);
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

		private async Task LoadRepositoryIssuesFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Repositories.Issues;

				var result = await queries.GetPageAsync(
					Login,
					Name,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					_filters);

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					IssueBlockButtonViewModel viewModel = new()
					{
						IssueItem = item,
					};

					_issueItems.Add(viewModel);
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
			OnPropertyChanged(nameof(CanCreateIssue));
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

		public async Task CreateIssueAsync(string title, string body)
		{
			if (!CanCreateIssue || string.IsNullOrWhiteSpace(title))
				return;

			IsIssueMutationRunning = true;

			try
			{
				var response = await _gitHub.Mutations.Issues.CreateIssueAsync(new CreateIssueRequest
				{
					RepositoryId = Repository.Id,
					Title = title.Trim(),
					Body = body,
				});

				if (response.Issue is null)
					throw new InvalidOperationException("The create issue mutation did not return an issue.");

				await LoadRepositoryIssuesAsync(Login, Name, false);
				IsEmpty = IssueItems.Count == 0;
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(CreateIssueAsync), ex);
				_messenger?.Send(new UserNotificationMessage("Something went wrong", ex.Message, UserNotificationType.Error));
			}
			finally
			{
				IsIssueMutationRunning = false;
			}
		}
	}
}
