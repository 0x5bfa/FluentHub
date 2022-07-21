using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Commits
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

            LoadCommitPageCommand = new AsyncRelayCommand(LoadRepositoryOneCommitAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private Commit _commitItem;
        public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

        private ObservableCollection<DiffBlockViewModel> _diffViewModels;
        public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

        public IAsyncRelayCommand LoadCommitPageCommand { get; }
        #endregion

        private async Task LoadRepositoryOneCommitAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                DiffQueries queries = new();
                var response = await queries.GetAllAsync(
                    CommitItem.Repository.Owner.Login,
                    CommitItem.Repository.Name,
                    CommitItem.Oid);

                _diffViewModels.Clear();
                foreach (var item in response.Files)
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
                _logger?.Error(nameof(LoadRepositoryOneCommitAsync), ex);
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
