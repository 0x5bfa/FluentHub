namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'converted_to_discussion' event on a given issue.
    /// </summary>
    public class ConvertedToDiscussionEvent : QueryableValue<ConvertedToDiscussionEvent>
    {
        internal ConvertedToDiscussionEvent(Expression expression) : base(expression)
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
        /// The discussion that the issue was converted into.
        /// </summary>
        public Discussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.Discussion.Create);

        /// <summary>
        /// The Node ID of the ConvertedToDiscussionEvent object
        /// </summary>
        public ID Id { get; }

        internal static ConvertedToDiscussionEvent Create(Expression expression)
        {
            return new ConvertedToDiscussionEvent(expression);
        }
    }
}