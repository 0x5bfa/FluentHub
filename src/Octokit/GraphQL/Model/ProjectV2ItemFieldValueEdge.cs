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
    public class ProjectV2ItemFieldValueEdge : QueryableValue<ProjectV2ItemFieldValueEdge>
    {
        internal ProjectV2ItemFieldValueEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectV2ItemFieldValue Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectV2ItemFieldValue.Create);

        internal static ProjectV2ItemFieldValueEdge Create(Expression expression)
        {
            return new ProjectV2ItemFieldValueEdge(expression);
        }
    }
}