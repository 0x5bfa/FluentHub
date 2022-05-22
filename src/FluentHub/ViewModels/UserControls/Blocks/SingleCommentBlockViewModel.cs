using FluentHub.Octokit.Models.ActivityPayloads;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class SingleCommentBlockViewModel : ObservableObject
    {
        private IssueCommentPayload _issueCommentPayload;
        public IssueCommentPayload IssueCommentPayload { get => _issueCommentPayload; set => SetProperty(ref _issueCommentPayload, value); }
    }
}
