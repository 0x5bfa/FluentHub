// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An edge in a connection.
	/// </summary>
	public class IssueTimelineItemsEdge
	{
		/// <summary>
		/// A cursor for use in pagination.
		/// </summary>
		public string Cursor { get; set; }

		/// <summary>
		/// The item at the end of the edge.
		/// </summary>
		public IssueTimelineItems Node { get; set; }
	}
}
