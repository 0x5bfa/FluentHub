using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class CommitsViewModel : ObservableObject
    {
        public CommitsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _items = new();
            Items = new(_items);

            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(LoadRepositoryPullRequestCommitsAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private PullRequest pullItem;
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }

        private readonly ObservableCollection<CommitButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<CommitButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestCommitsAsync(PullRequest pull)
        {
            try
            {
                if (pull != null)
                    PullItem = pull;

                CommitQueries queries = new();
                var items = await queries.GetAllAsync(
                    PullItem.Repository.Owner.Login,
                    PullItem.Repository.Name,
                    PullItem.Number);

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
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryPullRequestCommitsAsync), ex);
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
