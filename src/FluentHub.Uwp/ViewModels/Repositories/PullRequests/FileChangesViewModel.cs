using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
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

            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(LoadRepositoryPullRequestFileChangesAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private ObservableCollection<DiffBlockViewModel> _diffViewModels;
        public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestFileChangesAsync(PullRequest pull)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                if (pull != null) PullItem = pull;

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
    }
}
