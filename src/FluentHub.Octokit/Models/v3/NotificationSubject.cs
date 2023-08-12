namespace FluentHub.Octokit.Models.v3
{
	public class NotificationSubject
	{
		public NotificationSubjectType Type { get; set; }

		public string TypeHumanized { get; set; }

		public int Number { get; set; }

		public string Title { get; set; }
	}
}
