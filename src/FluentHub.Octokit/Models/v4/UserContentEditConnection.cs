// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A list of edits to content.
	/// </summary>
	public class UserContentEditConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<UserContentEditEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<UserContentEdit> Nodes { get; set; }

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
