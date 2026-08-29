namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An item in an issue timeline
    /// </summary>
    public class IssueTimelineItem : QueryableValue<IssueTimelineItem>, IUnion
    {
        internal IssueTimelineItem(Expression expression) : base(expression)
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
            /// Represents a 'closed' event on any `Closable`.
            /// </summary>
            public Selector<T> ClosedEvent(Func<ClosedEvent, T> selector) => default;

            /// <summary>
            /// Represents a Git commit.
            /// </summary>
            public Selector<T> Commit(Func<Commit, T> selector) => default;

            /// <summary>
            /// Represents a mention made by one issue or pull request to another.
            /// </summary>
            public Selector<T> CrossReferencedEvent(Func<CrossReferencedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'demilestoned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> DemilestonedEvent(Func<DemilestonedEvent, T> selector) => default;

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
            /// Represents a 'milestoned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> MilestonedEvent(Func<MilestonedEvent, T> selector) => default;

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
            /// Represents a 'subscribed' event on a given `Subscribable`.
            /// </summary>
            public Selector<T> SubscribedEvent(Func<SubscribedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'transferred' event on a given issue or pull request.
            /// </summary>
            public Selector<T> TransferredEvent(Func<TransferredEvent, T> selector) => default;

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

        internal static IssueTimelineItem Create(Expression expression)
        {
            return new IssueTimelineItem(expression);
        }
    }
}