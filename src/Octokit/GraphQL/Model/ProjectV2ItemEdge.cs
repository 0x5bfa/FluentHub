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
    public class ProjectV2ItemEdge : QueryableValue<ProjectV2ItemEdge>
    {
        internal ProjectV2ItemEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectV2Item Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectV2Item.Create);

        internal static ProjectV2ItemEdge Create(Expression expression)
        {
            return new ProjectV2ItemEdge(expression);
        }
    }
}