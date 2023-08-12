// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The connection type for ProjectV2.
	/// </summary>
	public class ProjectV2Connection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<ProjectV2Edge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<ProjectV2> Nodes { get; set; }

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
