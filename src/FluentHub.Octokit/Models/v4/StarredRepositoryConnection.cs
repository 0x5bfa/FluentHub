namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The connection type for Repository.
    /// </summary>
    public class StarredRepositoryConnection
    {
        /// <summary>
        /// A list of edges.
        /// </summary>
        public List<StarredRepositoryEdge> Edges { get; set; }

        /// <summary>
        /// Is the list of stars for this user truncated? This is true for users that have many stars.
        /// </summary>
        public bool IsOverLimit { get; set; }

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
    }
}