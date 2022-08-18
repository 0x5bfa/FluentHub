using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Issues
{
    public class IssueViewModel : ObservableObject
    {
        public IssueViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _timelineItems = new();
            TimelineItems = new(_timelineItems);

            RefreshIssuePageCommand = new AsyncRelayCommand(LoadRepositoryOneIssueAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private Issue _issueItem;
        public Issue IssueItem { get => _issueItem; private set => SetProperty(ref _issueItem, value); }

        private readonly ObservableCollection<object> _timelineItems;
        public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

        public IAsyncRelayCommand RefreshIssuePageCommand { get; }
        #endregion

        private async Task LoadRepositoryOneIssueAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                IssueQueries issueQueries = new();
                IssueEventQueries queries = new();
                _timelineItems.Clear();

                // Get issue item
                IssueItem = await issueQueries.GetAsync(Repository.Owner.Login, Repository.Name, Number);

                // Get issue body comment
                var bodyComment = await issueQueries.GetBodyAsync(Repository.Owner.Login, Repository.Name, Number);
                _timelineItems.Add(bodyComment);

                // Get all issue event timeline items
                var issueEvents = await queries.GetAllAsync(Repository.Owner.Login, Repository.Name, Number);
                foreach (var item in issueEvents)
                    _timelineItems.Add(item);
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryOneIssueAsync), ex);
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

        public async Task LoadRepositoryAsync(string owner, string name)
        {
            try
            {
                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(owner, name);

                RepositoryOverviewViewModel = new()
                {
                    Repository = Repository,
                    RepositoryName = Repository.Name,
                    RepositoryOwnerLogin = Repository.Owner.Login,
                    RepositoryVisibilityLabel = new()
                    {
                        Name = Repository.IsPrivate ? "Private" : "Public",
                        Color = "#64000000",
                    },
                    ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                    SelectedTag = "issues",
                };
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryAsync), ex);
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
