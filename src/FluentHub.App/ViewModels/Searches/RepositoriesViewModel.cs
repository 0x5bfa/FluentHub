using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Searches
{
	public class RepositoriesViewModel : ObservableObject
	{
		public RepositoriesViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			_resultItems = new();
			ResultItems = new(_resultItems);

			LoadSearchRepositoriesPageCommand = new AsyncRelayCommand(LoadSearchRepositoriesPageAsync);
			LoadSearchResultsFurtherCommand = new AsyncRelayCommand(LoadFurtherSearchResultsAsync);
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

		private readonly ObservableCollection<RepoBlockButtonViewModel> _resultItems;
		public ReadOnlyObservableCollection<RepoBlockButtonViewModel> ResultItems { get; }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadSearchRepositoriesPageCommand { get; }
		public IAsyncRelayCommand LoadSearchResultsFurtherCommand { get; }
		#endregion

		private async Task LoadSearchRepositoriesPageAsync()
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			string _currentTaskingMethodName = nameof(LoadSearchRepositoriesPageAsync);

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

			RepositorySearches searches = new();
			var response = await searches.GetAllAsync(term);

			_loadedItemCount += response.Count;
			_loadedPageCount++;

			// TODO: Fix this
			if (response.Count < 100)
				_loadedToTheEnd = true;

			_resultItems.Clear();
			foreach (var item in response)
			{
				RepoBlockButtonViewModel viewModel = new()
				{
					Repository = item,
				};

				_resultItems.Add(viewModel);
			}
		}

		public async Task LoadFurtherSearchResultsAsync()
		{
			// TODO: Fix RepoSearchQuery.cs to allow for custom api options
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "Repository Results";
			currentItem.Description = "Repository Results for \"" + SearchTerm + "\"";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Search.png"))
			};
		}
	}
}
