using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.Blocks;
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

            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(RefreshPullRequestPageAsync);
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

        private async Task RefreshPullRequestPageAsync(PullRequest pull)
        {
            try
            {
                if (pull != null) PullItem = pull;

                DiffQueries queries = new DiffQueries();
                var files = await queries.GetAllAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                if (!files.Any()) return;

                _diffViewModels.Clear();
                foreach (var item in files)
                {
                    DiffBlockViewModel viewModel = new()
                    {
                        ChangedFile = item,
                    };

                    _diffViewModels.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshPullRequestPageAsync", ex);
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
