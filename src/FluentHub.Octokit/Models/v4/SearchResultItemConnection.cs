// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A list of results that matched against a search query. Regardless of the number of matches, a maximum of 1,000 results will be available across all types, potentially split across many pages.
	/// </summary>
	public class SearchResultItemConnection
	{
		/// <summary>
		/// The total number of pieces of code that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
		/// </summary>
		public int CodeCount { get; set; }

		/// <summary>
		/// The total number of discussions that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
		/// </summary>
		public int DiscussionCount { get; set; }

		/// <summary>
		/// A list of edges.
		/// </summary>
		public List<SearchResultItemEdge> Edges { get; set; }

		/// <summary>
		/// The total number of issues that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
		/// </summary>
		public int IssueCount { get; set; }

		/// <summary>
		/// A list of nodes.
		/// </summary>
		public List<SearchResultItem> Nodes { get; set; }

		/// <summary>
		/// Information to aid in pagination.
		/// </summary>
		public PageInfo PageInfo { get; set; }

		/// <summary>
		/// The total number of repositories that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
		/// </summary>
		public int RepositoryCount { get; set; }

		/// <summary>
		/// The total number of users that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
		/// </summary>
		public int UserCount { get; set; }

		/// <summary>
		/// The total number of wiki pages that matched the search query. Regardless of the total number of matches, a maximum of 1,000 results will be available across all types.
		/// </summary>
		public int WikiCount { get; set; }
	}
}
