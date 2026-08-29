namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A list of repositories owned by the subject.
    /// </summary>
    public class RepositoryConnection : QueryableValue<RepositoryConnection>, IPagingConnection<Repository>
    {
        internal RepositoryConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<RepositoryEdge> Edges => this.CreateProperty(x => x.Edges);

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

        /// <summary>
        /// The total size in kilobytes of all repositories in the connection. Value will never be larger than max 32-bit signed integer.
        /// </summary>
        public int TotalDiskUsage { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static RepositoryConnection Create(Expression expression)
        {
            return new RepositoryConnection(expression);
        }
    }
}