// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Core.Contracts;
using FluentHub.Core.Queries.Discussions;

namespace FluentHub.ViewModels.Users
{
	public class DiscussionsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _discussions;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> DiscussionItems { get; }

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

		public IAsyncRelayCommand LoadUserDiscussionsPageCommand { get; }
		public IAsyncRelayCommand LoadUserDiscussionsFurtherCommand { get; }

		public DiscussionsViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_discussions = new();
			DiscussionItems = new(_discussions);

			LoadUserDiscussionsPageCommand = new AsyncRelayCommand(LoadUserDiscussionsPageAsync);
			LoadUserDiscussionsFurtherCommand = new AsyncRelayCommand(LoadUserDiscussionsFurtherAsync);
		}

		private async Task LoadUserDiscussionsPageAsync()
		{
			SetTabInformation("Discussions", "Discussions", "Discussions");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();
			_filters = new DiscussionListFilters();

			_currentTaskingMethodName = nameof(LoadUserDiscussionsPageAsync);

			try
			{
				await Task.WhenAll(
					LoadUserAsync(Login),
					LoadFilterOptionsAsync(Login),
					LoadUserDiscussionsAsync(Login));

				SetTabInformation("Discussions", "Discussions");

				IsEmpty = DiscussionItems.Count == 0;
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

		private async Task LoadUserDiscussionsAsync(string login)
		{
			var queries = _gitHub.Users.Discussions;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20), _filters);

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_discussions.Clear();
			foreach (var item in items)
			{
				DiscussionBlockButtonViewModel viewModel = new()
				{
					Item = item,
				};

				_discussions.Add(viewModel);
			}
		}

		private async Task LoadFilterOptionsAsync(string login)
		{
			var labels = await _gitHub.Users.Discussions.GetLabelNamesAsync(login);
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
				await LoadUserDiscussionsAsync(Login);
				IsEmpty = DiscussionItems.Count == 0;
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

		private async Task LoadUserDiscussionsFurtherAsync()
		{
			if (IsTaskLoading || _lastPageInfo is null || !_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.Discussions;

				var result = await queries.GetPageAsync(
					Login,
					PageRequest.Forward(20, _lastPageInfo.EndCursor),
					_filters);

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					DiscussionBlockButtonViewModel viewmodel = new()
					{
						Item = item,
					};

					_discussions.Add(viewmodel);
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
	}
}
