// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.Features.Users.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;

namespace FluentHub.Features.Users.Views
{
	public sealed partial class IssuesPage : NavigableView
	{
		public IssuesViewModel ViewModel { get; }
		private bool _filtersReady;
		private bool _isApplyingFilters;
		private bool _filterChangePending;

		public IssuesPage()
			: base(NavigationPageKind.User, NavigationPageKey.Issues)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<IssuesViewModel>();
			_pageLoadCommand = ViewModel.LoadUserIssuesPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override async void OnActivated(AppRoute route)
		{
			_filtersReady = false;
			var command = ViewModel.LoadUserIssuesPageCommand;
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
				SearchText = SearchIssuesBox.Text,
				State = StateFilterBox.SelectedIndex switch
				{
					1 => RepositoryItemStateFilter.Closed,
					2 => RepositoryItemStateFilter.All,
					_ => RepositoryItemStateFilter.Open,
				},
				Label = GetSelectedValue(LabelFilterBox, 2),
				HasNoLabels = LabelFilterBox.SelectedIndex == 1,
				IssueType = GetSelectedValue(IssueTypeFilterBox, 2),
				HasNoIssueType = IssueTypeFilterBox.SelectedIndex == 1,
				Assignee = GetSelectedValue(AssigneeFilterBox, 2),
				HasNoAssignee = AssigneeFilterBox.SelectedIndex == 1,
				Milestone = GetSelectedValue(MilestoneFilterBox, 2),
				HasNoMilestone = MilestoneFilterBox.SelectedIndex == 1,
				Sort = SortFilterBox.SelectedIndex is >= 0 and <= (int)RepositoryItemSort.MostEyes
					? (RepositoryItemSort)SortFilterBox.SelectedIndex
					: RepositoryItemSort.Newest,
			};

		private static string? GetSelectedValue(ComboBox comboBox, int firstValueIndex)
			=> comboBox.SelectedIndex >= firstValueIndex
				? comboBox.SelectedItem as string
				: null;

		private void ResetFilterSelections()
		{
			SearchIssuesBox.Text = string.Empty;
			StateFilterBox.SelectedIndex = 0;
			LabelFilterBox.SelectedIndex = 0;
			IssueTypeFilterBox.SelectedIndex = 0;
			AssigneeFilterBox.SelectedIndex = 0;
			MilestoneFilterBox.SelectedIndex = 0;
			SortFilterBox.SelectedIndex = 0;
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset
				<= Math.Max(200, scrollViewer.ViewportHeight / 2))
			{
				var command = ViewModel.LoadUserIssuesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
