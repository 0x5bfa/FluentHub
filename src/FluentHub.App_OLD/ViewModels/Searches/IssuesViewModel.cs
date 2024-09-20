using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Searches
{
	public class IssuesViewModel : ObservableObject
	{
		public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			_resultItems = new();
			ResultItems = new(_resultItems);

			LoadSearchIssuesPageCommand = new AsyncRelayCommand(LoadSearchIssuesPageAsync);
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private int _loadedItemCount = 0;
		private int _loadedPageCount = 0;
		private bool _loadedToTheEnd = false;
		private const int _itemCountPerPage = 30;

		private string _searchTerm;
		public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value); }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _resultItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> ResultItems { get; }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadSearchIssuesPageCommand { get; }
		#endregion

		private async Task LoadSearchIssuesPageAsync()
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			string _currentTaskingMethodName = nameof(LoadSearchIssuesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadSearchResultsAsync);
				await LoadSearchResultsAsync(SearchTerm);
			}
			catch (Exception ex)
			{
				TaskException = ex;
				faulted = true;

				_logger?.Error(_currentTaskingMethodName, ex);
				throw;
			}
			finally
			{
				SetCurrentTabItem();

				_messenger?.Send(new TaskStateMessaging(faulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadSearchResultsAsync(string term)
		{
			if (_loadedToTheEnd)
				return;

			IssueSearches searches = new();
			var response = await searches.GetAllAsync(term);

			_loadedItemCount += response.Count;
			_loadedPageCount++;

			// TODO: Fix this
			if (response.Count < 100)
				_loadedToTheEnd = true;

			_resultItems.Clear();

			foreach (var item in response)
			{
				IssueBlockButtonViewModel viewModel = new()
				{
					IssueItem = item,
				};

				_resultItems.Add(viewModel);
			}
		}

		public async Task LoadFurtherSearchResultsAsync()
		{
			// TODO: Fix IssueSearchQuery.cs to allow for custom api options
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Issue Results";
			currentItem.Description = "Issue Results for \"" + SearchTerm + "\"";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Search.png"))
			};
		}
	}
}