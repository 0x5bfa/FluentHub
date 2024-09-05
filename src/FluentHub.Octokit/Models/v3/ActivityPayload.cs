namespace FluentHub.Octokit.Models.v3
{
	public class ActivityPayload
	{
		public Repository Repository { get; set; }

		public User Sender { get; set; }

		public long Installation { get; set; }
	}
}
