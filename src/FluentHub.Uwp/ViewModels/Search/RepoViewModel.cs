using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Queries.Search;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks.Search;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.Search
{
    public class RepoViewModel : ObservableObject
    {
        public RepoViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _logger = logger;
            _messenger = messenger;

            _results = new();
            ResultItems = new(_results);
            
            LoadSearchResultsPageCommand = new AsyncRelayCommand(LoadSearchResultsPageAsync);
            LoadFurtherSearchResultsPageCommand = new AsyncRelayCommand(LoadFurtherSearchResultsAsync);
        }

        #region Fields and Properties

        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private int _loadedItemCount = 0;
        private int _loadedPageCount = 0;
        private bool _loadedToTheEnd = false;
        private const int _itemCountPerPage = 30;
        public String query;
        public IAsyncRelayCommand LoadSearchResultsPageCommand { get; }
        public IAsyncRelayCommand LoadFurtherSearchResultsPageCommand { get; }
        
        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }
        
        private readonly ObservableCollection<SearchRepoButtonBlockViewModel> _results;
        public ReadOnlyObservableCollection<SearchRepoButtonBlockViewModel> ResultItems { get; }
        
        #endregion

        private async Task LoadSearchResultsPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadSearchResultsPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadSearchResultsAsync);
                await LoadSearchResultsAsync(query);
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

                _messenger?.Send(
                    new TaskStateMessaging(faulted
                        ? TaskStatusType.IsFaulted
                        : TaskStatusType.IsCompletedSuccessfully));
            }
        }

        private async Task LoadSearchResultsAsync(String query)
        {
            if (_loadedToTheEnd) return;

            RepoSearchQueries queries = new();
            var response = await queries.GetAll(
                query);

            _loadedItemCount += response.Count();
            _loadedPageCount++;
            if (response.Count() < 100) // TODO: Fix this
                _loadedToTheEnd = true;
            _results.Clear();
            foreach (var item in response)
            {
                SearchRepoButtonBlockViewModel viewmodel = new()
                {
                    Item = item,
                };
                _results.Add(viewmodel);
            }
        }

        public async Task LoadFurtherSearchResultsAsync()
        {
            // TODO: Fix RepoSearchQuery.cs to allow for custom api options
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Repository Results";
            currentItem.Description = "Repository Results for \"" + query + "\"";
            currentItem.Url = "fluenthub://search/repos/" + query.Replace(" ", "&");
            currentItem.DisplayUrl = $"Search / Repos";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Search.png"))
            };
        }
    }
}