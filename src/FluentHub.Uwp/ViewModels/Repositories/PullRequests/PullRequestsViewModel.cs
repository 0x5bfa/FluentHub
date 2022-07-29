using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class PullRequestsViewModel : ObservableObject
    {
        public PullRequestsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _pullRequests = new();
            PullItems = new(_pullRequests);

            RefreshPullRequestsPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestsAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private readonly ObservableCollection<PullButtonBlockViewModel> _pullRequests;
        public ReadOnlyObservableCollection<PullButtonBlockViewModel> PullItems { get; }

        public IAsyncRelayCommand RefreshPullRequestsPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestsAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                PullRequestQueries queries = new();
                var items = await queries.GetAllAsync(
                    Repository.Owner.Login,
                    Repository.Name
                    );
                if (items == null) return;

                _pullRequests.Clear();
                foreach (var item in items)
                {
                    PullButtonBlockViewModel viewModel = new()
                    {
                        PullItem = item,
                    };

                    _pullRequests.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryPullRequestsAsync), ex);
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
