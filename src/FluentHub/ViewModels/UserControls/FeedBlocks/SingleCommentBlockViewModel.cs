using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.UserControls.FeedBlocks
{
	public class SingleCommentBlockViewModel : ObservableObject
	{
		private IssueCommentPayload _issueCommentPayload = default!;
		public IssueCommentPayload IssueCommentPayload { get => _issueCommentPayload; set => SetProperty(ref _issueCommentPayload, value); }
	}
}
