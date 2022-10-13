using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Utils;

namespace FluentHub.App.ViewModels.UserControls.FeedBlocks
{
    public class SingleCommentBlockViewModel : ObservableObject
    {
        private IssueCommentPayload _issueCommentPayload;
        public IssueCommentPayload IssueCommentPayload { get => _issueCommentPayload; set => SetProperty(ref _issueCommentPayload, value); }
    }
}
