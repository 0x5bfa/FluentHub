using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.PullRequests
{
    public class ConversationViewModel : ObservableObject
    {
        public ConversationViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _timelineItems = new();
            TimelineItems = new(_timelineItems);

            LoadRepositoryPullRequestConversationPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestConversationPageAsync);
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

        private readonly ObservableCollection<object> _timelineItems;
        public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

        private Exception _taskException;
        public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

        public IAsyncRelayCommand LoadRepositoryPullRequestConversationPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestConversationPageAsync()
        {
            _messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
            bool faulted = false;

            string _currentTaskingMethodName = nameof(LoadRepositoryPullRequestConversationPageAsync);

            try
            {
                _currentTaskingMethodName = nameof(LoadRepositoryAsync);
                await LoadRepositoryAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadPullRequestAsync);
                await LoadPullRequestAsync(Login, Name);

                _currentTaskingMethodName = nameof(LoadRepositoryPullRequestCommentsAsync);
                await LoadRepositoryPullRequestCommentsAsync(Login, Name);
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

        private async Task LoadRepositoryPullRequestCommentsAsync(string owner, string name)
        {
            PullRequestQueries pullRequestQueries = new();
            PullRequestEventQueries queries = new();
            _timelineItems.Clear();

            // Get pull request body comment
            var bodyComment = await pullRequestQueries.GetBodyAsync(owner, name, Number);
            _timelineItems.Add(bodyComment);

            // Get all pull request event timeline items
            var pullEvents = await queries.GetAllAsync(owner, name, Number);
            foreach (var item in pullEvents)
                _timelineItems.Add(item);
        }

        private async Task LoadPullRequestAsync(string owner, string name)
        {
            PullRequestQueries queries = new();
            PullItem = await queries.GetAsync(owner, name, Number);

            PullRequestOverviewViewModel = new()
            {
                PullRequest = PullItem,
                SelectedTag = "conversation",
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
            INavigationService navigationService = Ioc.Default.GetRequiredService<INavigationService>();

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.UserLogin = Repository.Owner.Login;
            currentItem.RepositoryName = Repository.Name;
            currentItem.Header = "Discussions";
            currentItem.Description = "Discussions";
            currentItem.Icon = new ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Discussions.png"))
            };
        }
    }
}
