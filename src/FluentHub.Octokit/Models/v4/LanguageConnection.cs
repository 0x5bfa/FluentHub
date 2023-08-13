// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A list of languages associated with the parent.
	/// </summary>
	public class LanguageConnection
	{
		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<LanguageEdge> Edges { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<Language> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// Identifies the total count of items in the connection.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// The total size in bytes of files written in that language.
		/// </summary>
		public int TotalSize { get; set; }
	}
}
