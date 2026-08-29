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
    public class GistCommentEdge : QueryableValue<GistCommentEdge>
    {
        internal GistCommentEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public GistComment Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.GistComment.Create);

        internal static GistCommentEdge Create(Expression expression)
        {
            return new GistCommentEdge(expression);
        }
    }
}