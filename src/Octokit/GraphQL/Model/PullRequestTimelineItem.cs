namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An item in a pull request timeline
    /// </summary>
    public class PullRequestTimelineItem : QueryableValue<PullRequestTimelineItem>, IUnion
    {
        internal PullRequestTimelineItem(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Represents an 'assigned' event on any assignable object.
            /// </summary>
            public Selector<T> AssignedEvent(Func<AssignedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'base_ref_deleted' event on a given pull request.
            /// </summary>
            public Selector<T> BaseRefDeletedEvent(Func<BaseRefDeletedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'base_ref_force_pushed' event on a given pull request.
            /// </summary>
            public Selector<T> BaseRefForcePushedEvent(Func<BaseRefForcePushedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'closed' event on any `Closable`.
            /// </summary>
            public Selector<T> ClosedEvent(Func<ClosedEvent, T> selector) => default;

            /// <summary>
            /// Represents a Git commit.
            /// </summary>
            public Selector<T> Commit(Func<Commit, T> selector) => default;

            /// <summary>
            /// A thread of comments on a commit.
            /// </summary>
            public Selector<T> CommitCommentThread(Func<CommitCommentThread, T> selector) => default;

            /// <summary>
            /// Represents a mention made by one issue or pull request to another.
            /// </summary>
            public Selector<T> CrossReferencedEvent(Func<CrossReferencedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'demilestoned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> DemilestonedEvent(Func<DemilestonedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'deployed' event on a given pull request.
            /// </summary>
            public Selector<T> DeployedEvent(Func<DeployedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'deployment_environment_changed' event on a given pull request.
            /// </summary>
            public Selector<T> DeploymentEnvironmentChangedEvent(Func<DeploymentEnvironmentChangedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'head_ref_deleted' event on a given pull request.
            /// </summary>
            public Selector<T> HeadRefDeletedEvent(Func<HeadRefDeletedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'head_ref_force_pushed' event on a given pull request.
            /// </summary>
            public Selector<T> HeadRefForcePushedEvent(Func<HeadRefForcePushedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'head_ref_restored' event on a given pull request.
            /// </summary>
            public Selector<T> HeadRefRestoredEvent(Func<HeadRefRestoredEvent, T> selector) => default;

            /// <summary>
            /// Represents a comment on an Issue.
            /// </summary>
            public Selector<T> IssueComment(Func<IssueComment, T> selector) => default;

            /// <summary>
            /// Represents a 'labeled' event on a given issue or pull request.
            /// </summary>
            public Selector<T> LabeledEvent(Func<LabeledEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'locked' event on a given issue or pull request.
            /// </summary>
            public Selector<T> LockedEvent(Func<LockedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'merged' event on a given pull request.
            /// </summary>
            public Selector<T> MergedEvent(Func<MergedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'milestoned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> MilestonedEvent(Func<MilestonedEvent, T> selector) => default;

            /// <summary>
            /// A review object for a given pull request.
            /// </summary>
            public Selector<T> PullRequestReview(Func<PullRequestReview, T> selector) => default;

            /// <summary>
            /// A review comment associated with a given repository pull request.
            /// </summary>
            public Selector<T> PullRequestReviewComment(Func<PullRequestReviewComment, T> selector) => default;

            /// <summary>
            /// A threaded list of comments for a given pull request.
            /// </summary>
            public Selector<T> PullRequestReviewThread(Func<PullRequestReviewThread, T> selector) => default;

            /// <summary>
            /// Represents a 'referenced' event on a given `ReferencedSubject`.
            /// </summary>
            public Selector<T> ReferencedEvent(Func<ReferencedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'renamed' event on a given issue or pull request
            /// </summary>
            public Selector<T> RenamedTitleEvent(Func<RenamedTitleEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'reopened' event on any `Closable`.
            /// </summary>
            public Selector<T> ReopenedEvent(Func<ReopenedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'review_dismissed' event on a given issue or pull request.
            /// </summary>
            public Selector<T> ReviewDismissedEvent(Func<ReviewDismissedEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'review_request_removed' event on a given pull request.
            /// </summary>
            public Selector<T> ReviewRequestRemovedEvent(Func<ReviewRequestRemovedEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'review_requested' event on a given pull request.
            /// </summary>
            public Selector<T> ReviewRequestedEvent(Func<ReviewRequestedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'subscribed' event on a given `Subscribable`.
            /// </summary>
            public Selector<T> SubscribedEvent(Func<SubscribedEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'unassigned' event on any assignable object.
            /// </summary>
            public Selector<T> UnassignedEvent(Func<UnassignedEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'unlabeled' event on a given issue or pull request.
            /// </summary>
            public Selector<T> UnlabeledEvent(Func<UnlabeledEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'unlocked' event on a given issue or pull request.
            /// </summary>
            public Selector<T> UnlockedEvent(Func<UnlockedEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'unsubscribed' event on a given `Subscribable`.
            /// </summary>
            public Selector<T> UnsubscribedEvent(Func<UnsubscribedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'user_blocked' event on a given user.
            /// </summary>
            public Selector<T> UserBlockedEvent(Func<UserBlockedEvent, T> selector) => default;
        }

        internal static PullRequestTimelineItem Create(Expression expression)
        {
            return new PullRequestTimelineItem(expression);
        }
    }
}