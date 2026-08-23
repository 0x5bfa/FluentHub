using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Discussions;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.Core.Queries.Discussions;

namespace FluentHub.Views.Repositories.Discussions
{
	public sealed partial class DiscussionsPage : LocatablePage
	{
		public DiscussionsViewModel ViewModel { get; }
		private bool _filtersReady;
		private bool _isApplyingFilters;
		private bool _filterChangePending;

		public DiscussionsPage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Discussions)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<DiscussionsViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryDiscussionsPageCommand;
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			_filtersReady = false;
			var command = ViewModel.LoadRepositoryDiscussionsPageCommand;
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

		private DiscussionListFilters CreateFilters()
			=> new()
			{
				SearchText = SearchDiscussionsBox.Text,
				State = StateFilterBox.SelectedIndex is >= 0 and <= (int)DiscussionStateFilter.All
					? (DiscussionStateFilter)StateFilterBox.SelectedIndex
					: DiscussionStateFilter.Open,
				Label = LabelFilterBox.SelectedIndex > 0
					? LabelFilterBox.SelectedItem as string
					: null,
				Sort = SortFilterBox.SelectedIndex is >= 0 and <= (int)DiscussionSort.TopAllTime
					? (DiscussionSort)SortFilterBox.SelectedIndex
					: DiscussionSort.LatestActivity,
			};

		private void ResetFilterSelections()
		{
			SearchDiscussionsBox.Text = string.Empty;
			StateFilterBox.SelectedIndex = 0;
			LabelFilterBox.SelectedIndex = 0;
			SortFilterBox.SelectedIndex = 0;
		}

		private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset
				<= Math.Max(200, scrollViewer.ViewportHeight / 2))
			{
				var command = ViewModel.LoadRepositoryDiscussionsFurtherCommand;
				if (command.CanExecute(null))
					command.Execute(null);
			}
		}
	}
}
