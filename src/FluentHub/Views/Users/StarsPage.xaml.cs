// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.ViewModels.Users;
using FluentHub.Core.Queries.Users;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.Views.Users
{
	public sealed partial class StarsPage : LocatablePage
	{
		public StarredReposViewModel ViewModel { get; }
		private bool _filtersReady;
		private bool _isApplyingFilters;
		private bool _filterChangePending;

		public StarsPage()
			: base(NavigationPageKind.User, NavigationPageKey.Stars)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<StarredReposViewModel>();
			_pageLoadCommand = ViewModel.LoadUserStarredRepositoriesPageCommand;
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			_filtersReady = false;
			var command = ViewModel.LoadUserStarredRepositoriesPageCommand;
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

		private StarredRepositoryListFilters CreateFilters()
			=> new()
			{
				SearchText = SearchStarsTextBox.Text,
				Type = TypeFilterBox.SelectedIndex is >= 0 and <= (int)UserRepositoryTypeFilter.Templates
					? (UserRepositoryTypeFilter)TypeFilterBox.SelectedIndex
					: UserRepositoryTypeFilter.All,
				Language = LanguageFilterBox.SelectedIndex > 0
					? LanguageFilterBox.SelectedItem as string
					: null,
				Sort = SortFilterBox.SelectedIndex is >= 0 and <= (int)StarredRepositorySort.MostStars
					? (StarredRepositorySort)SortFilterBox.SelectedIndex
					: StarredRepositorySort.RecentlyStarred,
			};

		private void ResetFilterSelections()
		{
			SearchStarsTextBox.Text = string.Empty;
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
				var command = ViewModel.LoadUserStarredRepositoriesFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
