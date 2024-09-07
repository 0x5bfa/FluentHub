namespace FluentHub.Octokit.Models.v3
{
	public class StatusEventPayload : ActivityPayload
	{
		public string Name { get; set; }

		public string Sha { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public DateTimeOffset UpdatedAt { get; set; }

		//public StringEnum<CommitState> State { get; set; }

		public string TargetUrl { get; set; }

		public string Description { get; set; }

		public string Context { get; set; }

		public long Id { get; set; }

		//public GitHubCommit Commit { get; set; }

		public Organization Organization { get; set; }

		//public List<Branch> Branches { get; set; }
	}
}
