namespace FluentHub.Octokit.Models.v3
{
	public class NotificationEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The item at the end of the edge.
		/// </summary>
		public Issue Node { get; set; }
	}
}
