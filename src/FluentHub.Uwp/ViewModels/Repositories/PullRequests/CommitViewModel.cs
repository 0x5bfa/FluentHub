using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
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

            LoadCommitPageCommand = new AsyncRelayCommand<string>(LoadRepositoryPullRequestOneCommitAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private PullRequest _pullRequest;
        public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }

        private Commit _commitItem;
        public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

        private ObservableCollection<DiffBlockViewModel> _diffViewModels;
        public ReadOnlyObservableCollection<DiffBlockViewModel> DiffViewModels { get; }

        public IAsyncRelayCommand LoadCommitPageCommand { get; }
        #endregion

        private async Task LoadRepositoryPullRequestOneCommitAsync(string url, CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                await GetPullRequestAsync(url);

                DiffQueries queries = new();
                var response = await queries.GetAllAsync(
                    CommitItem.Repository.Owner.Login,
                    CommitItem.Repository.Name,
                    PullRequest.Number);

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
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryPullRequestOneCommitAsync), ex);
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

        private async Task GetPullRequestAsync(string url)
        {
            var uri = new Uri(url);
            var segments = uri.AbsolutePath.Split("/").ToList();
            segments.RemoveAt(0);

            PullRequestQueries queries = new();
            var response = await queries.GetAsync(
                segments[0],
                segments[1],
                Convert.ToInt32(segments[3]));

            PullRequest = response;
        }
    }
}
