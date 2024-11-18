namespace FluentHub.Octokit.Models.v3
{
	public class PullRequestReviewEventPayload : ActivityPayload
	{
		public string Action { get; set; }

		public PullRequest PullRequest { get; set; }

		public PullRequestReview Review { get; set; }
	}
}
