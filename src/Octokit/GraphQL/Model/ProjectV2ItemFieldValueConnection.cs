namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for ProjectV2ItemFieldValue.
    /// </summary>
    public class ProjectV2ItemFieldValueConnection : QueryableValue<ProjectV2ItemFieldValueConnection>, IPagingConnection<ProjectV2ItemFieldValue>
    {
        internal ProjectV2ItemFieldValueConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<ProjectV2ItemFieldValueEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<ProjectV2ItemFieldValue> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static ProjectV2ItemFieldValueConnection Create(Expression expression)
        {
            return new ProjectV2ItemFieldValueConnection(expression);
        }
    }
}