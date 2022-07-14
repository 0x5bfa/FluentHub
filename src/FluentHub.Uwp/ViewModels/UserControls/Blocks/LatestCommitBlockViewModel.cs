using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.Repositories;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class LatestCommitBlockViewModel : ObservableObject
    {
        public LatestCommitBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            LoadLatestCommitBlockCommand = new AsyncRelayCommand(LoadRepositoryLatestCommitAsync);
        }

        #region Fields and Properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private RepoContextViewModel _contextViewModel;
        public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

        private Commit _commitOverviewItem;
        public Commit CommitOverviewItem { get => _commitOverviewItem; set => SetProperty(ref _commitOverviewItem, value); }

        private string _commitUpdatedAtHumanized;
        public string CommitUpdatedAtHumanized { get => _commitUpdatedAtHumanized; set => SetProperty(ref _commitUpdatedAtHumanized, value); }

        private string _commitMessageHeadline;
        public string CommitMessageHeadline { get => _commitMessageHeadline; set => SetProperty(ref _commitMessageHeadline, value); }

        private bool _hasMoreCommitMessage;
        public bool HasMoreCommitMessage { get => _hasMoreCommitMessage; set => SetProperty(ref _hasMoreCommitMessage, value); }

        private string _subCommitMessages;
        public string SubCommitMessages { get => _subCommitMessages; set => SetProperty(ref _subCommitMessages, value); }

        public IAsyncRelayCommand LoadLatestCommitBlockCommand { get; }
        #endregion

        public async Task LoadRepositoryLatestCommitAsync()
        {
            try
            {
                CommitQueries queries = new();
                CommitOverviewItem = await queries.GetAsync(ContextViewModel.Repository.Name, ContextViewModel.Repository.Owner.Login, ContextViewModel.BranchName, ContextViewModel.Path);

                CommitUpdatedAtHumanized = CommitOverviewItem.CommittedDate.Humanize();

                CommitMessageHeadline = CommitOverviewItem.MessageHeadline;
                var splittedMessages = CommitOverviewItem.Message.Split("\n", 2);

                // Get sub commit message
                if (splittedMessages.Count() > 1)
                {
                    HasMoreCommitMessage = true;

                    SubCommitMessages = splittedMessages[1];
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryLatestCommitAsync), ex);
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
