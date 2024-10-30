namespace FluentHub.Octokit.Models.v3
{
	public class PushEventPayload : ActivityPayload
	{
		public string Head { get; set; }

		public string Ref { get; set; }

		public int Size { get; set; }

		public List<Commit> Commits { get; set; }
	}
}
