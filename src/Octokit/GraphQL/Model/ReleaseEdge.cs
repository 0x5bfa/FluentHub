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
    public class ReleaseEdge : QueryableValue<ReleaseEdge>
    {
        internal ReleaseEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Release Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Release.Create);

        internal static ReleaseEdge Create(Expression expression)
        {
            return new ReleaseEdge(expression);
        }
    }
}