namespace FluentHub.Octokit.Models.v3
{
	public class IssueEventPayload : ActivityPayload
	{
		public string Action { get; set; }

		public Issue Issue { get; set; }
	}
}
