namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'comment_deleted' event on a given issue or pull request.
    /// </summary>
    public class CommentDeletedEvent : QueryableValue<CommentDeletedEvent>
    {
        internal CommentDeletedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The user who authored the deleted comment.
        /// </summary>
        public IActor DeletedCommentAuthor => this.CreateProperty(x => x.DeletedCommentAuthor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The Node ID of the CommentDeletedEvent object
        /// </summary>
        public ID Id { get; }

        internal static CommentDeletedEvent Create(Expression expression)
        {
            return new CommentDeletedEvent(expression);
        }
    }
}