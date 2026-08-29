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
    public class TeamDiscussionCommentEdge : QueryableValue<TeamDiscussionCommentEdge>
    {
        internal TeamDiscussionCommentEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public TeamDiscussionComment Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.TeamDiscussionComment.Create);

        internal static TeamDiscussionCommentEdge Create(Expression expression)
        {
            return new TeamDiscussionCommentEdge(expression);
        }
    }
}