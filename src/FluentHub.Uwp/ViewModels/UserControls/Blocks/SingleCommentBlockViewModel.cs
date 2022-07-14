using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class SingleCommentBlockViewModel : ObservableObject
    {
        private IssueCommentPayload _issueCommentPayload;
        public IssueCommentPayload IssueCommentPayload { get => _issueCommentPayload; set => SetProperty(ref _issueCommentPayload, value); }
    }
}
