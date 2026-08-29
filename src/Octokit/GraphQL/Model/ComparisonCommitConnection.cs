namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for Commit.
    /// </summary>
    public class ComparisonCommitConnection : QueryableValue<ComparisonCommitConnection>, IPagingConnection<Commit>
    {
        internal ComparisonCommitConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The total count of authors and co-authors across all commits.
        /// </summary>
        public int AuthorCount { get; }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<CommitEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<Commit> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static ComparisonCommitConnection Create(Expression expression)
        {
            return new ComparisonCommitConnection(expression);
        }
    }
}