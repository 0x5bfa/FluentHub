using FluentHub.Backend;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class LatestCommitBlockViewModel : ObservableObject
    {
        #region constructor
        public LatestCommitBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;

            LoadLatestCommitBlockCommand = new AsyncRelayCommand(LoadLatestCommitBlockAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private RepoContextViewModel _contextViewModel;
        private Commit _commitOverviewItem;
        private string _commitUpdatedAtHumanized;
        private string _commitMessageHeadline;
        private bool _hasMoreCommitMessage;
        private string _subCommitMessages;
        #endregion

        #region properties
        public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }
        public Commit CommitOverviewItem { get => _commitOverviewItem; set => SetProperty(ref _commitOverviewItem, value); }
        public string CommitUpdatedAtHumanized { get => _commitUpdatedAtHumanized; set => SetProperty(ref _commitUpdatedAtHumanized, value); }
        public string CommitMessageHeadline { get => _commitMessageHeadline; set => SetProperty(ref _commitMessageHeadline, value); }
        public bool HasMoreCommitMessage { get => _hasMoreCommitMessage; set => SetProperty(ref _hasMoreCommitMessage, value); }
        public string SubCommitMessages { get => _subCommitMessages; set => SetProperty(ref _subCommitMessages, value); }

        public IAsyncRelayCommand LoadLatestCommitBlockCommand { get; }
        #endregion

        #region methods
        public async Task LoadLatestCommitBlockAsync()
        {
            try
            {
                CommitQueries queries = new();
                CommitOverviewItem = await queries.GetAsync(ContextViewModel.Name, ContextViewModel.Owner, ContextViewModel.BranchName, ContextViewModel.Path);

                CommitUpdatedAtHumanized = CommitOverviewItem.CommittedAtHumanized;

                var commitMessageLines = CommitOverviewItem.CommitMessage.Split("\n");
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
                _logger?.Error("LoadLatestCommitBlockAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
        #endregion
    }
}
