using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class PullRequestViewModel : ObservableObject
    {
        public PullRequestViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            RefreshPullRequestPageCommand = new AsyncRelayCommand(LoadRepositoryOnePullRequestAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private PullRequest _pullItem;
        public PullRequest PullItem { get => _pullItem; private set => SetProperty(ref _pullItem, value); }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task LoadRepositoryOnePullRequestAsync(CancellationToken token)
        {
            try
            {
                PullRequestQueries queries = new();
                PullItem = await queries.GetAsync(
                    Repository.Owner.Login,
                    Repository.Name,
                    Number
                    );
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryOnePullRequestAsync), ex);
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

                    SelectedTag = "code",
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
