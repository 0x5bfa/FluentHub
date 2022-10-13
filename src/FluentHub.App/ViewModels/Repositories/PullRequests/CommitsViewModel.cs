using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Media.Imaging;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
    public class CommitsViewModel : ObservableObject
    {
        public CommitsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            LoadRepositoryPullRequestCommitsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestCommitsPageAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private string _login;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
        public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private readonly ObservableCollection<CommitBlockButtonViewModel> _items;
        public ReadOnlyObservableCollection<CommitBlockButtonViewModel> Items { get; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadRepositoryPullRequestCommitsPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestCommitsPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitsPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadRepositoryAsync);
                await LoadRepositoryAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadPullRequestAsync);
                await LoadPullRequestAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommitsAsync);
                await LoadRepositoryPullRequestCommitsAsync(Login, Name);
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

        private async Task LoadRepositoryPullRequestCommitsAsync(string owner, string name)
        {
            PullRequestCommitQueries queries = new();
            var items = await queries.GetAllAsync(owner, name, PullItem.Number);

            _items.Clear();
            foreach (var item in items)
            {
                CommitBlockButtonViewModel viewModel = new()
                {
                    CommitItem = item,
                    PullRequest = pullItem,
                };

                _items.Add(viewModel);
            }
        }

        private async Task LoadPullRequestAsync(string owner, string name)
        {
            PullRequestQueries queries = new();
            PullItem = await queries.GetAsync(owner, name, Number);

            PullRequestOverviewViewModel = new()
            {
                PullRequest = PullItem,
                SelectedTag = "commits",
            };
        }

        private async Task LoadRepositoryAsync(string owner, string name)
        {
            RepositoryQueries queries = new();
            Repository = await queries.GetDetailsAsync(owner, name);

            RepositoryOverviewViewModel = new()
            {
                Repository = Repository,
                RepositoryName = Repository.Name,
                RepositoryOwnerLogin = Repository.Owner.Login,
                ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                SelectedTag = "pullrequests",
            };
        }

        private void SetCurrentTabItem()
        {
            var provider = App.Current.Services;
            INavigationService navigationService = provider.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Discussions";
            currentItem.Description = "Discussions";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };
        }
    }
}
