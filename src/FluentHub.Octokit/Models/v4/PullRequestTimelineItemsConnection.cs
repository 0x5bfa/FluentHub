// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The connection type for PullRequestTimelineItems.
	/// </summary>
	public class PullRequestTimelineItemsConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<PullRequestTimelineItemsEdge> Edges { get; set; }

		/// <summary>
		/// Identifies the count of items after applying `before` and `after` filters.
		/// </summary>
		public int FilteredCount { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<PullRequestTimelineItems> Nodes { get; set; }

		/// <summary>
		/// Identifies the count of items after applying `before`/`after` filters and `first`/`last`/`skip` slicing.
		/// </summary>
		public int PageCount { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of items in the connection.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// Identifies the date and time when the timeline was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the timeline was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
