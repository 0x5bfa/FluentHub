using System.Diagnostics;

namespace FluentHub.Octokit.Models.v3
{
	public class PullRequestEventPayload : ActivityPayload
	{
		public string Action { get; protected set; }

		public int Number { get; protected set; }

		public PullRequest PullRequest { get; protected set; }
	}
}
