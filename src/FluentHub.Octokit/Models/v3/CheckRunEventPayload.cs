namespace FluentHub.Octokit.Models.v3
{
	public class CheckRunEventPayload : ActivityPayload
	{
		public string Action { get; set; }

		public CheckRun CheckRun { get; set; }

		//public CheckRunRequestedAction RequestedAction { get; set; }
	}
}
