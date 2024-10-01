namespace FluentHub.Octokit.Models.v3
{
	/// <summary>
	/// The connection type for Notification.
	/// </summary>
	public class NotificationConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<NotificationEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<Notification> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of items in the connection.
		/// </summary>
		public int TotalCount { get; set; }
	}
}
