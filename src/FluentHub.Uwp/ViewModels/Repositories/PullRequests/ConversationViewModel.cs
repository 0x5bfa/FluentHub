using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls.Overview;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class ConversationViewModel : ObservableObject
    {
        public ConversationViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _timelineItems = new();
            TimelineItems = new(_timelineItems);

            RefreshPullRequestPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestCommentsAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private PullRequestOverviewViewModel _pullRequestOverviewViewModel;
        public PullRequestOverviewViewModel PullRequestOverviewViewModel { get => _pullRequestOverviewViewModel; set => SetProperty(ref _pullRequestOverviewViewModel, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private readonly ObservableCollection<object> _timelineItems;
        public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestCommentsAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                PullRequestQueries pullRequestQueries = new();
                PullRequestEventQueries queries = new();
                _timelineItems.Clear();

                // Get pull request body comment
                var bodyComment = await pullRequestQueries.GetBodyAsync(Repository.Owner.Login, Repository.Name, Number);
                _timelineItems.Add(bodyComment);

                // Get all pull request event timeline items
                var pullEvents = await queries.GetAllAsync(Repository.Owner.Login, Repository.Name, Number);
                foreach (var item in pullEvents)
                    _timelineItems.Add(item);
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryPullRequestCommentsAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _messenger?.Send(new LoadingMessaging(false));
            }
        }

        public async Task LoadPullRequestAsync()
        {
            try
            {
                PullRequestQueries queries = new();
                PullItem = await queries.GetAsync(Repository.Owner.Login, Repository.Name, Number);

                PullRequestOverviewViewModel = new()
                {
                    PullRequest = PullItem,
                    SelectedTag = "conversation",
                };
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadPullRequestAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }

        public async Task LoadRepositoryAsync(string owner, string name)
        {
            try
            {
                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(owner, name);

                RepositoryOverviewViewModel = new()
                {
                    Repository = Repository,
                    RepositoryName = Repository.Name,
                    RepositoryOwnerLogin = Repository.Owner.Login,
                    RepositoryVisibilityLabel = new()
                    {
                        Name = Repository.IsPrivate ? "Private" : "Public",
                        Color = "#64000000",
                    },
                    ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                    SelectedTag = "pullrequests",
                };
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
