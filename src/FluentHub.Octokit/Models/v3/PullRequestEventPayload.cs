namespace FluentHub.Octokit.Models.v3
{
	public class PullRequestEventPayload : ActivityPayload
	{
		public string Action { get; set; }

		public int Number { get; set; }

		public PullRequest PullRequest { get; set; }
	}
}
