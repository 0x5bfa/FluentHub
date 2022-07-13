using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
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

            LoadLatestCommitBlockCommand = new AsyncRelayCommand(LoadLatestCommitBlockAsync);
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

        public async Task LoadLatestCommitBlockAsync()
        {
            try
            {
                CommitQueries queries = new();
                CommitOverviewItem = await queries.GetAsync(ContextViewModel.Repository.Name, ContextViewModel.Repository.Owner.Login, ContextViewModel.BranchName, ContextViewModel.Path);

                CommitUpdatedAtHumanized = CommitOverviewItem.CommittedDate.Humanize();

                var commitMessageLines = CommitOverviewItem.Message.Split("\n");
                CommitMessageHeadline = commitMessageLines[0];

                // Get sub commit message
                if (commitMessageLines.Count() > 1)
                {
                    HasMoreCommitMessage = true;

                    if (commitMessageLines[1] == "")
                    {
                        var messageTextLinesList = commitMessageLines.ToList();
                        messageTextLinesList.RemoveAt(1);
                        commitMessageLines = messageTextLinesList.ToArray();
                    }

                    SubCommitMessages = string.Join('\n', commitMessageLines, 1, commitMessageLines.Count() - 1);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadLatestCommitBlockAsync), ex);
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
