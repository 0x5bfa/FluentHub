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
    public class ProjectV2Edge : QueryableValue<ProjectV2Edge>
    {
        internal ProjectV2Edge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectV2 Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectV2.Create);

        internal static ProjectV2Edge Create(Expression expression)
        {
            return new ProjectV2Edge(expression);
        }
    }
}