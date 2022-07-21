using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.Repositories.PullRequests
{
    public class PullRequestViewModel : ObservableObject
    {
        public PullRequestViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            RefreshPullRequestPageCommand = new AsyncRelayCommand<string>(LoadRepositoryOnePullRequestAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private PullRequest _pullItem;
        public PullRequest PullItem { get => _pullItem; private set => SetProperty(ref _pullItem, value); }

        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        private async Task LoadRepositoryOnePullRequestAsync(string url)
        {
            try
            {
                var uri = new Uri(url);
                var pathSegments = uri.AbsolutePath.Split("/").ToList();
                pathSegments.RemoveAt(0);

                PullRequestQueries queries = new();
                PullItem = await queries.GetAsync(pathSegments[0], pathSegments[1], Convert.ToInt32(pathSegments[3]));
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
    }
}
