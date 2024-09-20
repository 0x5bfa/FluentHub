using FluentHub.App.Models;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Searches;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Searches
{
	public class UsersViewModel : ObservableObject
	{
		public UsersViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;

			_resultItems = new();
			ResultItems = new(_resultItems);

			LoadSearchUsersPageCommand = new AsyncRelayCommand(LoadSearchUsersPageAsync);
			LoadSearchResultsFurtherCommand = new AsyncRelayCommand(LoadFurtherSearchResultsAsync);
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private int _loadedItemCount = 0;
		private int _loadedPageCount = 0;
		private bool _loadedToTheEnd = false;
		private const int _itemCountPerPage = 100;

		private string _searchTerm;
		public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value); }

		private readonly ObservableCollection<UserBlockButtonViewModel> _resultItems;
		public ReadOnlyObservableCollection<UserBlockButtonViewModel> ResultItems { get; }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadSearchUsersPageCommand { get; }
		public IAsyncRelayCommand LoadSearchResultsFurtherCommand { get; }
		#endregion

		private async Task LoadSearchUsersPageAsync()
		{
			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			bool faulted = false;

			string _currentTaskingMethodName = nameof(LoadSearchUsersPageAsync);

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
			if (_loadedToTheEnd) return;

			UserSearches searches = new();
			var response = await searches.GetAllAsync(term);

			_loadedItemCount += response.Count();
			_loadedPageCount++;

			// TODO: Fix this
			if (response.Count() < 100)
				_loadedToTheEnd = true;

			_resultItems.Clear();
			foreach (var item in response)
			{
				UserBlockButtonViewModel viewModel = new()
				{
					User = item,
				};

				_resultItems.Add(viewModel);
			}
		}

		public async Task LoadFurtherSearchResultsAsync()
		{
			// TODO: Fix UserSearchQuery.cs to allow for custom api options
		}

		private void SetCurrentTabItem()
		{
			INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

			var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
			currentItem.Header = "User Results";
			currentItem.Description = "User Results for \"" + SearchTerm + "\"";
			currentItem.Icon = new ImageIconSource
			{
				ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Search.png"))
			};
		}
	}
}
