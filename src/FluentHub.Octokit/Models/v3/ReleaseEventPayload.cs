namespace FluentHub.Octokit.Models.v3
{
	public class ReleaseEventPayload : ActivityPayload
	{
		public string Action { get; set; }

		public Release Release { get; set; }
	}
}
