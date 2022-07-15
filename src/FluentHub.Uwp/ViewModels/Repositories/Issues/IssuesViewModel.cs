using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Issues
{
    public class IssuesViewModel : ObservableObject
    {
        public IssuesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            _issueItems = new();
            IssueItems = new(_issueItems);

            _pinnedItems = new();
            PinnedItems = new(_pinnedItems);

            RefreshIssuesPageCommand = new AsyncRelayCommand<string>(LoadRepositoryIssuesAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private readonly ObservableCollection<IssueButtonBlockViewModel> _issueItems;
        public ReadOnlyObservableCollection<IssueButtonBlockViewModel> IssueItems { get; }

        private readonly ObservableCollection<IssueButtonBlockViewModel> _pinnedItems;
        public ReadOnlyObservableCollection<IssueButtonBlockViewModel> PinnedItems { get; }

        public IAsyncRelayCommand RefreshIssuesPageCommand { get; }
        #endregion

        private async Task LoadRepositoryIssuesAsync(string url, CancellationToken token)
        {
            try
            {
                var uri = new Uri(url);
                var pathSegments = uri.AbsolutePath.Split("/").ToList();
                pathSegments.RemoveAt(0);

                IssueQueries queries = new();
                var items = await queries.GetAllAsync(pathSegments[1], pathSegments[0]);
                if (items == null) return;

                _issueItems.Clear();
                foreach (var item in items)
                {
                    IssueButtonBlockViewModel viewModel = new()
                    {
                        IssueItem = item,
                    };

                    _issueItems.Add(viewModel);
                }

                var pinnedIssues = await queries.GetPinnedAllAsync(pathSegments[0], pathSegments[1]);
                if (pinnedIssues == null) return;

                _pinnedItems.Clear();
                foreach (var item in pinnedIssues)
                {
                    IssueButtonBlockViewModel viewModel = new()
                    {
                        IssueItem = item,
                        CompactMode = true,
                    };

                    _pinnedItems.Add(viewModel);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryIssuesAsync), ex);
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
