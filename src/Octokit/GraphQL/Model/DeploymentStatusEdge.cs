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
    public class DeploymentStatusEdge : QueryableValue<DeploymentStatusEdge>
    {
        internal DeploymentStatusEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public DeploymentStatus Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.DeploymentStatus.Create);

        internal static DeploymentStatusEdge Create(Expression expression)
        {
            return new DeploymentStatusEdge(expression);
        }
    }
}