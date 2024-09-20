namespace FluentHub.App.ViewModels.UserControls.FeedBlocks
{
	public class SingleCommentBlockViewModel : ObservableObject
	{
		private IssueCommentPayload _issueCommentPayload;
		public IssueCommentPayload IssueCommentPayload { get => _issueCommentPayload; set => SetProperty(ref _issueCommentPayload, value); }
	}
}
