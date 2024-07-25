// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A list of repositories owned by the subject.
	/// </summary>
	public class RepositoryConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<RepositoryEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<Repository> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of items in the connection.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// The total size in kilobytes of all repositories in the connection. Value will never be larger than max 32-bit signed integer.
		/// </summary>
		public int TotalDiskUsage { get; set; }
	}
}
