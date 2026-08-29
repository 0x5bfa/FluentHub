namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'review_dismissed' event on a given issue or pull request.
    /// </summary>
    public class ReviewDismissedEvent : QueryableValue<ReviewDismissedEvent>
    {
        internal ReviewDismissedEvent(Expression expression) : base(expression)
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
        /// Identifies the optional message associated with the 'review_dismissed' event.
        /// </summary>
        public string DismissalMessage { get; }

        /// <summary>
        /// Identifies the optional message associated with the event, rendered to HTML.
        /// </summary>
        public string DismissalMessageHTML { get; }

        /// <summary>
        /// The Node ID of the ReviewDismissedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the previous state of the review with the 'review_dismissed' event.
        /// </summary>
        public PullRequestReviewState PreviousReviewState { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Identifies the commit which caused the review to become stale.
        /// </summary>
        public PullRequestCommit PullRequestCommit => this.CreateProperty(x => x.PullRequestCommit, Octokit.GraphQL.Model.PullRequestCommit.Create);

        /// <summary>
        /// The HTTP path for this review dismissed event.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the review associated with the 'review_dismissed' event.
        /// </summary>
        public PullRequestReview Review => this.CreateProperty(x => x.Review, Octokit.GraphQL.Model.PullRequestReview.Create);

        /// <summary>
        /// The HTTP URL for this review dismissed event.
        /// </summary>
        public string Url { get; }

        internal static ReviewDismissedEvent Create(Expression expression)
        {
            return new ReviewDismissedEvent(expression);
        }
    }
}