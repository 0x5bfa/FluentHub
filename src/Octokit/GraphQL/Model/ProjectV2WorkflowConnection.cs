namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for ProjectV2Workflow.
    /// </summary>
    public class ProjectV2WorkflowConnection : QueryableValue<ProjectV2WorkflowConnection>, IPagingConnection<ProjectV2Workflow>
    {
        internal ProjectV2WorkflowConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<ProjectV2WorkflowEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<ProjectV2Workflow> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static ProjectV2WorkflowConnection Create(Expression expression)
        {
            return new ProjectV2WorkflowConnection(expression);
        }
    }
}