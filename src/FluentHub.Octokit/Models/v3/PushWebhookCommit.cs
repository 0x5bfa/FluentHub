namespace FluentHub.Octokit.Models.v3
{
	public class PushWebhookCommit
	{
		public string Id { get; set; }

		public string TreeId { get; set; }

		public bool Distinct { get; set; }

		public string Message { get; set; }

		public DateTimeOffset Timestamp { get; set; }

		public Uri Url { get; set; }

		//public Committer Author { get; set; }

		//public Committer Committer { get; set; }

		public List<string> Added { get; set; }

		public List<string> Removed { get; set; }

		public List<string> Modified { get; set; }
	}
}
