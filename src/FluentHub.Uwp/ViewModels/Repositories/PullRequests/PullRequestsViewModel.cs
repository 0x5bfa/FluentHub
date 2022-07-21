using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class PullRequestsViewModel : ObservableObject
    {
        public PullRequestsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _pullRequests = new();
            PullItems = new(_pullRequests);

            RefreshPullRequestsPageCommand = new AsyncRelayCommand<string>(LoadRepositoryPullRequestsAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<PullButtonBlockViewModel> _pullRequests;
        public ReadOnlyObservableCollection<PullButtonBlockViewModel> PullItems { get; }

        public IAsyncRelayCommand RefreshPullRequestsPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestsAsync(string nameWithOwner, CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                PullRequestQueries queries = new();
                var items = await queries.GetAllAsync(nameWithOwner.Split("/")[1], nameWithOwner.Split("/")[0]);
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
    }
}
