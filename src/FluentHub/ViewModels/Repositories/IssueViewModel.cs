using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.UserControls.Blocks;
using FluentHub.ViewModels.UserControls.Blocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.Repositories
{
    public class IssueViewModel : ObservableObject
    {
        #region constructor
        public IssueViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            CommentBlocks = new();
            RefreshIssuePageCommand = new AsyncRelayCommand<Issue>(RefreshIssuePageAsync);
        }
        #endregion

        #region fields
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private IssueCommentBlockViewModel _commentViewModel;
        private Issue _issueItem;
        #endregion

        #region properties
        public Issue IssueItem { get => _issueItem; private set => SetProperty(ref _issueItem, value); }
        public IssueCommentBlockViewModel CommentViewModel { get => _commentViewModel; private set => SetProperty(ref _commentViewModel, value); }
        public ObservableCollection<IssueCommentBlock> CommentBlocks;
        public IAsyncRelayCommand RefreshIssuePageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshIssuePageAsync(Issue issue)
        {
            try
            {
                IssueItem = issue;

                IssueEventQueries queries = new();
                var issueBody = await queries.GetBodyAsync(issue.OwnerLogin, issue.Name, issue.Number);

                var bodyCommentViewModel = new IssueCommentBlockViewModel() {
                    IssueComment = issueBody,
                };

                var bodyCommentBlock = new IssueCommentBlock() { PropertyViewModel = bodyCommentViewModel };

                CommentBlocks.Add(bodyCommentBlock);

                // Now only available issue comments
                var issueComments = await queries.GetAllAsync(issue.OwnerLogin, issue.Name, issue.Number);

                foreach(var issueCommentItem in issueComments)
                {
                    var viewmodel = new IssueCommentBlockViewModel()
                    {
                        IssueComment = issueCommentItem,
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
                _logger?.Error("RefreshIssuePageAsync", ex);
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
