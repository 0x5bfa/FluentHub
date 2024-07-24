// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The connection type for EnterpriseRepositoryInfo.
	/// </summary>
	public class EnterpriseRepositoryInfoConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<EnterpriseRepositoryInfoEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<EnterpriseRepositoryInfo> Nodes { get; set; }

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
