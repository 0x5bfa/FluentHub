// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The connection type for CreatedCommitContribution.
	/// </summary>
	public class CreatedCommitContributionConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<CreatedCommitContributionEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<CreatedCommitContribution> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of commits across days and repositories in the connection.
		/// </summary>
		public int TotalCount { get; set; }
	}
}
