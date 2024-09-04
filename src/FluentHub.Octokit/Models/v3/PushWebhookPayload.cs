namespace FluentHub.Octokit.Models.v3
{
	public class PushWebhookPayload : ActivityPayload
	{
		public string Head { get; set; }

		public string Before { get; set; }

		public string After { get; set; }

		public string Ref { get; set; }

		public string BaseRef { get; set; }

		public bool Created { get; set; }

		public bool Deleted { get; set; }

		public bool Forced { get; set; }

		public string Compare { get; set; }

		public int Size { get; set; }

		public List<PushWebhookCommit> Commits { get; set; }

		public PushWebhookCommit HeadCommit { get; set; }
	}
}
