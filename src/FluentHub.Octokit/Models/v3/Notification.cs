namespace FluentHub.Octokit.Models.v3
{
	public class Notification
	{
		public long Id { get; set; }

		public Repository Repository { get; set; }

		public NotificationSubject Subject { get; set; }

		public string Reason { get; set; }

		public bool Unread { get; set; }

		public DateTimeOffset UpdatedAt { get; set; }

		public string UpdatedAtHumanized { get; set; }

		public DateTimeOffset LastReadAt { get; set; }

		public string LastReadAtHumanized { get; set; }

		public string Url { get; set; }

	}
}
