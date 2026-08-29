namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'review_requested' event on a given pull request.
    /// </summary>
    public class ReviewRequestedEvent : QueryableValue<ReviewRequestedEvent>
    {
        internal ReviewRequestedEvent(Expression expression) : base(expression)
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
        /// The Node ID of the ReviewRequestedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Identifies the reviewer whose review was requested.
        /// </summary>
        public RequestedReviewer RequestedReviewer => this.CreateProperty(x => x.RequestedReviewer, Octokit.GraphQL.Model.RequestedReviewer.Create);

        internal static ReviewRequestedEvent Create(Expression expression)
        {
            return new ReviewRequestedEvent(expression);
        }
    }
}