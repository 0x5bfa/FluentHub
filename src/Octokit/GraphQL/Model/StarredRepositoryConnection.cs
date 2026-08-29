namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for Repository.
    /// </summary>
    public class StarredRepositoryConnection : QueryableValue<StarredRepositoryConnection>, IPagingConnection<Repository>
    {
        internal StarredRepositoryConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<StarredRepositoryEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// Is the list of stars for this user truncated? This is true for users that have many stars.
        /// </summary>
        public bool IsOverLimit { get; }

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<Repository> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static StarredRepositoryConnection Create(Expression expression)
        {
            return new StarredRepositoryConnection(expression);
        }
    }
}