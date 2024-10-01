namespace FluentHub.Octokit.Models.v3
{
	public class Activity
	{
		public ActivityPayloadType Type { get; set; }

		public ActivityPayloads PayloadSets { get; set; }

		public bool Public { get; set; }

		public Repository Repository { get; set; }

		public User Actor { get; set; }

		public Organization Organization { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

		public string CreatedAtHumanized { get; set; }

		public string Id { get; set; }
	}
}
