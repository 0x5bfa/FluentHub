using FluentHub.Octokit.Searches;

namespace FluentHub.App.ViewModels.Searches
{
	public class CodeViewModel : BaseViewModel
	{
		private string _searchTerm;
		public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value); }

		private readonly ObservableCollection<FluentHub.Octokit.Models.v3.Searches.SearchCode> _resultItems;
		public ReadOnlyObservableCollection<FluentHub.Octokit.Models.v3.Searches.SearchCode> ResultItems { get; }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		public IAsyncRelayCommand LoadSearchCodePageCommand { get; }

		public CodeViewModel() : base()
		{
			_resultItems = new();
			ResultItems = new(_resultItems);

			LoadSearchCodePageCommand = new AsyncRelayCommand(LoadSearchCodePageAsync);
		}

		private async Task LoadSearchCodePageAsync()
		{
			SetTabInformation("Code", "Code", "Code");
			SetLoadingProgress(false);

			_currentTaskingMethodName = nameof(LoadSearchCodePageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadSearchResultsAsync);
				await LoadSearchResultsAsync(SearchTerm);

				SetTabInformation("Code", "Code", "Code");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(true);
			}
		}

		private async Task LoadSearchResultsAsync(string term)
		{
			if (_loadedToTheEnd)
				return;

			CodeSearches searches = new();
			var response = await searches.GetAllAsync(term);

			_loadedItemCount += response.Count;
			_loadedPageCount++;

			// TODO: Fix this
			if (response.Count < 100)
				_loadedToTheEnd = true;

			_resultItems.Clear();
			foreach (var item in response)
			{
				_resultItems.Add(item);
			}
		}

		public async Task LoadFurtherSearchResultsAsync()
		{
			// TODO: Fix RepoSearchQuery.cs to allow for custom api options
		}
	}
}
