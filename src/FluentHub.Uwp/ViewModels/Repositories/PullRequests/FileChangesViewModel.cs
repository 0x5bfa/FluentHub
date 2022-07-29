using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class FileChangesViewModel : ObservableObject
    {
        public FileChangesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _diffViewModels = new();
            DiffViewModels = new(_diffViewModels);

            RefreshPullRequestPageCommand = new AsyncRelayCommand(LoadRepositoryPullRequestFileChangesAsync);
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

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private ObservableCollection<DiffBlockViewModel> _diffViewModels;
        public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestFileChangesAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                DiffQueries queries = new();
                var response = await queries.GetAllAsync(PullItem.Repository.Owner.Login, PullItem.Repository.Name, PullItem.Number);

                if (response.Any() is false) return;

                _diffViewModels.Clear();
                foreach (var item in response)
                {
                    DiffBlockViewModel viewModel = new()
                    {
                        ChangedPullRequestFile = item,
                    };

                    _diffViewModels.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryPullRequestFileChangesAsync), ex);
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
                var response = await queries.GetAsync(
                    Repository.Owner.Login,
                    Repository.Name,
                    Number);

                PullItem = response;
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
