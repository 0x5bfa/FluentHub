using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Commits
{
    public class CommitsViewModel : ObservableObject
    {
        public CommitsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            _items = new();
            Items = new(_items);

            LoadCommitsPageCommand = new AsyncRelayCommand(LoadCommitsPageAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private RepoContextViewModel contextViewModel;
        private readonly ObservableCollection<CommitButtonBlockViewModel> _items;

        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }
        public ReadOnlyObservableCollection<CommitButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand LoadCommitsPageCommand { get; }
        #endregion

        private async Task LoadCommitsPageAsync(CancellationToken token)
        {
            try
            {
                CommitQueries queries = new();
                var items = await queries.GetAllAsync(
                    ContextViewModel.Repository.Name,
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                _items.Clear();
                foreach (var item in items)
                {
                    CommitButtonBlockViewModel viewModel = new()
                    {
                        CommitItem = item,
                    };

                    _items.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadCommitsPageAsync), ex);
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
