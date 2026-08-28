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
using FluentHub.Core.Infrastructure.GitHub.Queries.Discussions;

namespace FluentHub.ViewModels.Repositories.Discussions
{
	public class DiscussionsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> Items { get; }

		public ObservableCollection<string> StateFilterOptions { get; } =
		[
			"Open",
			"Closed",
			"Locked",
			"Unlocked",
			"Answered",
			"Unanswered",
			"Verified",
			"All",
		];

		public ObservableCollection<string> LabelFilterOptions { get; } = ["All labels"];

		public ObservableCollection<string> SortFilterOptions { get; } =
		[
			"Latest activity",
			"Date created",
			"Top: Past day",
			"Top: Past week",
			"Top: Past month",
			"Top: Past year",
			"Top: All time",
		];

		private DiscussionListFilters _filters = new();

		public IAsyncRelayCommand LoadRepositoryDiscussionsPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryDiscussionsFurtherCommand { get; }

		public DiscussionsViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryDiscussionsPageCommand = new AsyncRelayCommand(LoadRepositoryDiscussionsPageAsync);
			LoadRepositoryDiscussionsFurtherCommand = new AsyncRelayCommand(LoadRepositoryDiscussionsFurtherAsync);
		}

		private async Task LoadRepositoryDiscussionsPageAsync()
		{
			SetTabInformation("Discussions", "Discussions", "Discussions");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();
			_filters = new DiscussionListFilters();

			_currentTaskingMethodName = nameof(LoadRepositoryDiscussionsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				var owner = Repository.Owner.Login;
				var name = Repository.Name;

				_currentTaskingMethodName = nameof(LoadFilterOptionsAsync);
				var filterOptionsTask = LoadFilterOptionsAsync(owner, name);

				_currentTaskingMethodName = nameof(LoadRepositoryDiscussionsAsync);
				var discussionsTask = LoadRepositoryDiscussionsAsync(owner, name);
				await Task.WhenAll(filterOptionsTask, discussionsTask);

				SetTabInformation($"Discussions \u2022 {Login}/{Name}", $"Discussions \u2022 {Login}/{Name}");

				IsEmpty = Items.Count == 0;
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

		private async Task LoadRepositoryDiscussionsAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Discussions;

			var result = await queries.GetPageAsync(owner, name, PageRequest.Forward(20), _filters);

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			ReplaceItems(items);
		}

		private async Task LoadFilterOptionsAsync(string owner, string name)
		{
			var labels = await _gitHub.Repositories.Discussions.GetLabelNamesAsync(owner, name);
			var options = new[] { "All labels" }.Concat(labels)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();
			if (LabelFilterOptions.SequenceEqual(options, StringComparer.Ordinal))
				return;

			LabelFilterOptions.Clear();
			foreach (var label in options)
				LabelFilterOptions.Add(label);
		}

		public async Task ApplyFiltersAsync(DiscussionListFilters filters)
		{
			ArgumentNullException.ThrowIfNull(filters);

			_filters = filters;
			InitializeNodePagingInfo();
			SetLoadingProgress(true);
			_currentTaskingMethodName = nameof(ApplyFiltersAsync);

			try
			{
				await LoadRepositoryDiscussionsAsync(Repository.Owner.Login, Repository.Name);
				IsEmpty = Items.Count == 0;
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

		private async Task LoadRepositoryDiscussionsFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Repositories.Discussions;

				var result = await queries.GetPageAsync(
					Repository.Owner.Login,
					Repository.Name,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					_filters);

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				AppendItems(items);
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

		private void ReplaceItems(IEnumerable<Discussion> discussions)
		{
			_items.Clear();
			AppendItems(discussions);
		}

		private void AppendItems(IEnumerable<Discussion> discussions)
		{
			foreach (var discussion in discussions)
				_items.Add(new DiscussionBlockButtonViewModel { Item = discussion });
		}
	}
}
