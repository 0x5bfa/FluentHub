namespace FluentHub.Octokit.Models.v3
{
	public class ActivityPayloads
	{
		public CheckRunEventPayload CheckRunEventPayload { get; set; }
		public CheckSuiteEventPayload CheckSuiteEventPayload { get; set; }
		public CommitComment CommitComment { get; set; }
		public CreateEventPayload CreateEventPayload { get; set; }
		public DeleteEventPayload DeleteEventPayload { get; set; }
		public ForkEventPayload ForkEventPayload { get; set; }
		public IssueCommentPayload IssueCommentPayload { get; set; }
		public IssueEventPayload IssueEventPayload { get; set; }
		public PullRequestCommentPayload PullRequestCommentPayload { get; set; }
		public PullRequestEventPayload PullRequestEventPayload { get; set; }
		public PullRequestReviewEvent PullRequestReviewEvent { get; set; }
		public PushEventPayload PushEventPayload { get; set; }
		public PushWebhookCommit PushWebhookCommit { get; set; }
		public PushWebhookCommitter PushWebhookCommitter { get; set; }
		public PushWebhookPayload PushWebhookPayload { get; set; }
		public ReleaseEventPayload ReleaseEventPayload { get; set; }
		public StarredEventPayload StarredEventPayload { get; set; }
		public StatusEventPayload StatusEventPayload { get; set; }
	}
}
