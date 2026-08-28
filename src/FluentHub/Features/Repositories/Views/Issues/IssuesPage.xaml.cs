// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Services;
using FluentHub.Features.Repositories.ViewModels.Issues;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using Microsoft.UI.Xaml;

namespace FluentHub.Features.Repositories.Views.Issues
{
	public sealed partial class IssuesPage : NavigableView
	{
		public IssuesViewModel ViewModel;
		private bool _filtersReady;
		private bool _isApplyingFilters;
		private bool _filterChangePending;

		public IssuesPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Issues)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<IssuesViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryIssuesPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override async void OnActivated(AppRoute route)
		{
			_filtersReady = false;
			var command = ViewModel.LoadRepositoryIssuesPageCommand;
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
				SearchText = SearchIssuesBox.Text,
				Label = GetSelectedValue(LabelFilterBox, 2),
				HasNoLabels = LabelFilterBox.SelectedIndex == 1,
				IssueType = GetSelectedValue(IssueTypeFilterBox, 2),
				HasNoIssueType = IssueTypeFilterBox.SelectedIndex == 1,
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
			SearchIssuesBox.Text = string.Empty;
			StateFilterBox.SelectedIndex = 0;
			LabelFilterBox.SelectedIndex = 0;
			IssueTypeFilterBox.SelectedIndex = 0;
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
				var command = ViewModel.LoadRepositoryIssuesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}

		private async void OnCreateIssueClicked(object sender, RoutedEventArgs e)
		{
			var titleBox = new TextBox
			{
				Header = "Title",
				PlaceholderText = "Issue title",
			};
			var bodyBox = new TextBox
			{
				Header = "Description",
				AcceptsReturn = true,
				MinHeight = 160,
				TextWrapping = TextWrapping.Wrap,
			};
			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(titleBox);
			content.Children.Add(bodyBox);

			var dialog = new ContentDialog
			{
				Title = "New issue",
				Content = content,
				PrimaryButtonText = "Create",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.CreateIssueAsync(titleBox.Text, bodyBox.Text);
		}
	}
}
