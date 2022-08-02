namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A list of results that matched against a search query.
    /// </summary>
    public class SearchResultItemConnection
    {
        /// <summary>
        /// The number of pieces of code that matched the search query.
        /// </summary>
        public int CodeCount { get; set; }

        /// <summary>
        /// The number of discussions that matched the search query.
        /// </summary>
        public int DiscussionCount { get; set; }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public List<SearchResultItemEdge> Edges { get; set; }

        /// <summary>
        /// The number of issues that matched the search query.
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
        /// The number of repositories that matched the search query.
        /// </summary>
        public int RepositoryCount { get; set; }

        /// <summary>
        /// The number of users that matched the search query.
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// The number of wiki pages that matched the search query.
        /// </summary>
        public int WikiCount { get; set; }
    }
}