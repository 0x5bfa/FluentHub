using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.UserControls.Blocks;
using FluentHub.ViewModels.UserControls.Blocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.PullRequests
{
    public class PullRequestViewModel : ObservableObject
    {
        #region constructor
        public PullRequestViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            CommentBlocks = new();
            RefreshPullRequestPageCommand = new AsyncRelayCommand<PullRequest>(RefreshPullRequestPageAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private PullRequest pullItem;
        #endregion

        #region properties
        public PullRequest PullItem { get => pullItem; private set => SetProperty(ref pullItem, value); }
        public ObservableCollection<IssueCommentBlock> CommentBlocks;
        public IAsyncRelayCommand RefreshPullRequestPageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshPullRequestPageAsync(PullRequest pull)
        {
            try
            {
                PullItem = pull;

                PullRequestEventQueries queries = new();
                var prBoddy = await queries.GetBodyAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                var bodyCommentViewModel = new IssueCommentBlockViewModel()
                {
                    IssueComment = prBoddy,
                };

                var bodyCommentBlock = new IssueCommentBlock() { PropertyViewModel = bodyCommentViewModel };

                CommentBlocks.Add(bodyCommentBlock);

                // Now only available PR comments
                var prComments = await queries.GetAllAsync(PullItem.OwnerLogin, PullItem.Name, PullItem.Number);

                foreach (var prCommentItem in prComments)
                {
                    var viewmodel = new IssueCommentBlockViewModel()
                    {
                        IssueComment = prCommentItem,
                    };

                    var commentBlock = new IssueCommentBlock()
                    {
                        PropertyViewModel = viewmodel
                    };

                    CommentBlocks.Add(commentBlock);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshPullRequestPageAsync", ex);
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
