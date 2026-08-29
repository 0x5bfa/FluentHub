namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ProjectV2WorkflowEdge : QueryableValue<ProjectV2WorkflowEdge>
    {
        internal ProjectV2WorkflowEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectV2Workflow Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectV2Workflow.Create);

        internal static ProjectV2WorkflowEdge Create(Expression expression)
        {
            return new ProjectV2WorkflowEdge(expression);
        }
    }
}