namespace FluentHub.Octokit.Models.v3
{
	public class CheckSuiteEventPayload : ActivityPayload
	{
		public string Action { get; set; }

		public CheckSuite CheckSuite { get; set; }
	}
}
