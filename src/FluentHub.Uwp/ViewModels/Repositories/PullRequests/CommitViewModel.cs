using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class CommitViewModel : ObservableObject
    {
        public CommitViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            _diffViewModels = new();
            DiffViewModels = new(_diffViewModels);

            LoadCommitPageCommand = new AsyncRelayCommand(LoadCommitPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private Commit _commitItem;
        public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

        private CommitDetails _commitDetails;
        public CommitDetails CommitDetails { get => _commitDetails; set => SetProperty(ref _commitDetails, value); }

        private ObservableCollection<DiffBlockViewModel> _diffViewModels;
        public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

        public IAsyncRelayCommand LoadCommitPageCommand { get; }
        #endregion

        private async Task LoadCommitPageAsync(CancellationToken token)
        {
            try
            {
                DiffQueries queries = new();
                var files = await queries.GetAllAsync(
                    CommitItem.Owner,
                    CommitItem.Name,
                    CommitItem.PullRequestNumber);

                if (CommitDetails.ChangedFiles.Count() == 0) return;

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
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadCommitPageAsync), ex);
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
