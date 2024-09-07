namespace FluentHub.Octokit.Models.v3
{
	public class PullRequestCommentPayload : ActivityPayload
	{
		public string Action { get; set; }

		public PullRequest PullRequest { get; set; }

		public PullRequestReviewComment Comment { get; set; }
	}
}
