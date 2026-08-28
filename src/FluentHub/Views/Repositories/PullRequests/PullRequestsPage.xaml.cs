// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.PullRequests;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.Views.Repositories.PullRequests
{
	public sealed partial class PullRequestsPage : NavigableView
	{
		public PullRequestsViewModel ViewModel { get; }
		private bool _filtersReady;
		private bool _isApplyingFilters;
		private bool _filterChangePending;

		public PullRequestsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<PullRequestsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryPullRequestsPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override async void OnActivated(AppRoute route)
		{
			_filtersReady = false;
			var command = ViewModel.LoadRepositoryPullRequestsPageCommand;
			if (command.CanExecute(null))
				await command.ExecuteAsync(null);

			ResetFilterSelections();
			_filtersReady = true;
		}

		private void OnFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
			=> _ = ApplySelectedFiltersAsync();

		private void OnSearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
			=> _ = ApplySelectedFiltersAsync();

		private async Task ApplySelectedFiltersAsync()
		{
			if (!_filtersReady)
				return;

			if (_isApplyingFilters)
			{
				_filterChangePending = true;
				return;
			}

			_isApplyingFilters = true;
			try
			{
				do
				{
					_filterChangePending = false;
					await ViewModel.ApplyFiltersAsync(CreateFilters());
				}
				while (_filterChangePending);
			}
			finally
			{
				_isApplyingFilters = false;
			}
		}

		private RepositoryItemListFilters CreateFilters()
			=> new()
			{
				State = StateFilterBox.SelectedIndex switch
				{
					1 => RepositoryItemStateFilter.Closed,
					2 => RepositoryItemStateFilter.All,
					_ => RepositoryItemStateFilter.Open,
				},
				Sort = GetSelectedSort(),
				SearchText = SearchPullRequestsBox.Text,
				Label = GetSelectedValue(LabelFilterBox, 2),
				HasNoLabels = LabelFilterBox.SelectedIndex == 1,
				Author = GetSelectedValue(AuthorFilterBox, 1),
				Assignee = GetSelectedValue(AssigneeFilterBox, 2),
				HasNoAssignee = AssigneeFilterBox.SelectedIndex == 1,
				Milestone = GetSelectedValue(MilestoneFilterBox, 2),
				HasNoMilestone = MilestoneFilterBox.SelectedIndex == 1,
			};

		private RepositoryItemSort GetSelectedSort()
			=> SortFilterBox.SelectedIndex is >= 0 and <= (int)RepositoryItemSort.MostEyes
				? (RepositoryItemSort)SortFilterBox.SelectedIndex
				: RepositoryItemSort.Newest;

		private static string? GetSelectedValue(ComboBox comboBox, int firstValueIndex)
			=> comboBox.SelectedIndex >= firstValueIndex
				? comboBox.SelectedItem as string
				: null;

		private void ResetFilterSelections()
		{
			SearchPullRequestsBox.Text = string.Empty;
			StateFilterBox.SelectedIndex = 0;
			LabelFilterBox.SelectedIndex = 0;
			AuthorFilterBox.SelectedIndex = 0;
			AssigneeFilterBox.SelectedIndex = 0;
			MilestoneFilterBox.SelectedIndex = 0;
			SortFilterBox.SelectedIndex = 0;
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
			{
				var command = ViewModel.LoadRepositoryPullRequestsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
