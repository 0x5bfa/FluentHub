// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels.Users;
using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Views.Users
{
	public sealed partial class RepositoriesPage : NavigableView
	{
		public RepositoriesViewModel ViewModel { get; }
		private bool _filtersReady;
		private bool _isApplyingFilters;
		private bool _filterChangePending;

		public RepositoriesPage()
			: base(NavigationPageKind.User, NavigationPageKey.Repositories)
		{
			InitializeComponent();

			ViewModel = GetRequiredService<RepositoriesViewModel>();
			_pageLoadCommand = ViewModel.LoadUserRepositoriesPageCommand;
			_screenViewModel = ViewModel;
		}

		protected override async void OnActivated(AppRoute route)
		{
			_filtersReady = false;
			var command = ViewModel.LoadUserRepositoriesPageCommand;
			if (command.CanExecute(null))
				await command.ExecuteAsync(null);

			ResetFilterSelections();
			_filtersReady = true;
		}

		private void OnFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
			=> _ = ApplySelectedFiltersAsync();

		private void OnSearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
			=> _ = ApplySelectedFiltersAsync();

		private void OnLanguageDropDownOpened(object sender, object e)
		{
			var command = ViewModel.LoadLanguageOptionsCommand;
			if (command.CanExecute(null))
				_ = command.ExecuteAsync(null);
		}

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

		private UserRepositoryListFilters CreateFilters()
			=> new()
			{
				SearchText = SearchRepositoriesTextBox.Text,
				Type = TypeFilterBox.SelectedIndex is >= 0 and <= (int)UserRepositoryTypeFilter.Templates
					? (UserRepositoryTypeFilter)TypeFilterBox.SelectedIndex
					: UserRepositoryTypeFilter.All,
				Language = LanguageFilterBox.SelectedIndex > 0
					? LanguageFilterBox.SelectedItem as string
					: null,
				Sort = SortFilterBox.SelectedIndex is >= 0 and <= (int)UserRepositorySort.Stars
					? (UserRepositorySort)SortFilterBox.SelectedIndex
					: UserRepositorySort.LastUpdated,
			};

		private void ResetFilterSelections()
		{
			SearchRepositoriesTextBox.Text = string.Empty;
			TypeFilterBox.SelectedIndex = 0;
			LanguageFilterBox.SelectedIndex = 0;
			SortFilterBox.SelectedIndex = 0;
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset
				<= Math.Max(200, scrollViewer.ViewportHeight / 2))
			{
				var command = ViewModel.LoadUserRepositoriesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
