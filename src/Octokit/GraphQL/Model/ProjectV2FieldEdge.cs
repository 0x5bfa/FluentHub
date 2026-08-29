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
    public class ProjectV2FieldEdge : QueryableValue<ProjectV2FieldEdge>
    {
        internal ProjectV2FieldEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectV2Field Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectV2Field.Create);

        internal static ProjectV2FieldEdge Create(Expression expression)
        {
            return new ProjectV2FieldEdge(expression);
        }
    }
}