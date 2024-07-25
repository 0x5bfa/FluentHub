// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A list of reactions that have been left on the subject.
	/// </summary>
	public class ReactionConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<ReactionEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<Reaction> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of items in the connection.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// Whether or not the authenticated user has left a reaction on the subject.
		/// </summary>
		public bool ViewerHasReacted { get; set; }
	}
}
