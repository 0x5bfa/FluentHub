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
    public class PullRequestTimelineItems : QueryableValue<PullRequestTimelineItems>, IUnion
    {
        internal PullRequestTimelineItems(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Represents an 'added_to_merge_queue' event on a given pull request.
            /// </summary>
            public Selector<T> AddedToMergeQueueEvent(Func<AddedToMergeQueueEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'added_to_project' event on a given issue or pull request.
            /// </summary>
            public Selector<T> AddedToProjectEvent(Func<AddedToProjectEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'assigned' event on any assignable object.
            /// </summary>
            public Selector<T> AssignedEvent(Func<AssignedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'auto_merge_disabled' event on a given pull request.
            /// </summary>
            public Selector<T> AutoMergeDisabledEvent(Func<AutoMergeDisabledEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'auto_merge_enabled' event on a given pull request.
            /// </summary>
            public Selector<T> AutoMergeEnabledEvent(Func<AutoMergeEnabledEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'auto_rebase_enabled' event on a given pull request.
            /// </summary>
            public Selector<T> AutoRebaseEnabledEvent(Func<AutoRebaseEnabledEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'auto_squash_enabled' event on a given pull request.
            /// </summary>
            public Selector<T> AutoSquashEnabledEvent(Func<AutoSquashEnabledEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'automatic_base_change_failed' event on a given pull request.
            /// </summary>
            public Selector<T> AutomaticBaseChangeFailedEvent(Func<AutomaticBaseChangeFailedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'automatic_base_change_succeeded' event on a given pull request.
            /// </summary>
            public Selector<T> AutomaticBaseChangeSucceededEvent(Func<AutomaticBaseChangeSucceededEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'base_ref_changed' event on a given issue or pull request.
            /// </summary>
            public Selector<T> BaseRefChangedEvent(Func<BaseRefChangedEvent, T> selector) => default;

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
            /// Represents a 'comment_deleted' event on a given issue or pull request.
            /// </summary>
            public Selector<T> CommentDeletedEvent(Func<CommentDeletedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'connected' event on a given issue or pull request.
            /// </summary>
            public Selector<T> ConnectedEvent(Func<ConnectedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'convert_to_draft' event on a given pull request.
            /// </summary>
            public Selector<T> ConvertToDraftEvent(Func<ConvertToDraftEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'converted_note_to_issue' event on a given issue or pull request.
            /// </summary>
            public Selector<T> ConvertedNoteToIssueEvent(Func<ConvertedNoteToIssueEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'converted_to_discussion' event on a given issue.
            /// </summary>
            public Selector<T> ConvertedToDiscussionEvent(Func<ConvertedToDiscussionEvent, T> selector) => default;

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
            /// Represents a 'disconnected' event on a given issue or pull request.
            /// </summary>
            public Selector<T> DisconnectedEvent(Func<DisconnectedEvent, T> selector) => default;

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
            /// Represents a 'marked_as_duplicate' event on a given issue or pull request.
            /// </summary>
            public Selector<T> MarkedAsDuplicateEvent(Func<MarkedAsDuplicateEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'mentioned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> MentionedEvent(Func<MentionedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'merged' event on a given pull request.
            /// </summary>
            public Selector<T> MergedEvent(Func<MergedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'milestoned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> MilestonedEvent(Func<MilestonedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'moved_columns_in_project' event on a given issue or pull request.
            /// </summary>
            public Selector<T> MovedColumnsInProjectEvent(Func<MovedColumnsInProjectEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'pinned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> PinnedEvent(Func<PinnedEvent, T> selector) => default;

            /// <summary>
            /// Represents a Git commit part of a pull request.
            /// </summary>
            public Selector<T> PullRequestCommit(Func<PullRequestCommit, T> selector) => default;

            /// <summary>
            /// Represents a commit comment thread part of a pull request.
            /// </summary>
            public Selector<T> PullRequestCommitCommentThread(Func<PullRequestCommitCommentThread, T> selector) => default;

            /// <summary>
            /// A review object for a given pull request.
            /// </summary>
            public Selector<T> PullRequestReview(Func<PullRequestReview, T> selector) => default;

            /// <summary>
            /// A threaded list of comments for a given pull request.
            /// </summary>
            public Selector<T> PullRequestReviewThread(Func<PullRequestReviewThread, T> selector) => default;

            /// <summary>
            /// Represents the latest point in the pull request timeline for which the viewer has seen the pull request's commits.
            /// </summary>
            public Selector<T> PullRequestRevisionMarker(Func<PullRequestRevisionMarker, T> selector) => default;

            /// <summary>
            /// Represents a 'ready_for_review' event on a given pull request.
            /// </summary>
            public Selector<T> ReadyForReviewEvent(Func<ReadyForReviewEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'referenced' event on a given `ReferencedSubject`.
            /// </summary>
            public Selector<T> ReferencedEvent(Func<ReferencedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'removed_from_merge_queue' event on a given pull request.
            /// </summary>
            public Selector<T> RemovedFromMergeQueueEvent(Func<RemovedFromMergeQueueEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'removed_from_project' event on a given issue or pull request.
            /// </summary>
            public Selector<T> RemovedFromProjectEvent(Func<RemovedFromProjectEvent, T> selector) => default;

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
            /// Represents an 'unmarked_as_duplicate' event on a given issue or pull request.
            /// </summary>
            public Selector<T> UnmarkedAsDuplicateEvent(Func<UnmarkedAsDuplicateEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'unpinned' event on a given issue or pull request.
            /// </summary>
            public Selector<T> UnpinnedEvent(Func<UnpinnedEvent, T> selector) => default;

            /// <summary>
            /// Represents an 'unsubscribed' event on a given `Subscribable`.
            /// </summary>
            public Selector<T> UnsubscribedEvent(Func<UnsubscribedEvent, T> selector) => default;

            /// <summary>
            /// Represents a 'user_blocked' event on a given user.
            /// </summary>
            public Selector<T> UserBlockedEvent(Func<UserBlockedEvent, T> selector) => default;
        }

        internal static PullRequestTimelineItems Create(Expression expression)
        {
            return new PullRequestTimelineItems(expression);
        }
    }
}