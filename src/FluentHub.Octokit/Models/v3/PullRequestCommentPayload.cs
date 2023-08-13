using System.Diagnostics;

namespace FluentHub.Octokit.Models.v3
{
	public class PullRequestCommentPayload : ActivityPayload
	{
		public string Action { get; protected set; }

		public PullRequest PullRequest { get; protected set; }

		public PullRequestReviewComment Comment { get; protected set; }
	}
}
