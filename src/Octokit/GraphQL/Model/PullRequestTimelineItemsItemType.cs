using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible item types found in a timeline.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestTimelineItemsItemType
    {
        /// <summary>
        /// Represents a Git commit part of a pull request.
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST_COMMIT")]
        PullRequestCommit,

        /// <summary>
        /// Represents a commit comment thread part of a pull request.
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST_COMMIT_COMMENT_THREAD")]
        PullRequestCommitCommentThread,

        /// <summary>
        /// A review object for a given pull request.
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST_REVIEW")]
        PullRequestReview,

        /// <summary>
        /// A threaded list of comments for a given pull request.
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST_REVIEW_THREAD")]
        PullRequestReviewThread,

        /// <summary>
        /// Represents the latest point in the pull request timeline for which the viewer has seen the pull request's commits.
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST_REVISION_MARKER")]
        PullRequestRevisionMarker,

        /// <summary>
        /// Represents a 'automatic_base_change_failed' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "AUTOMATIC_BASE_CHANGE_FAILED_EVENT")]
        AutomaticBaseChangeFailedEvent,

        /// <summary>
        /// Represents a 'automatic_base_change_succeeded' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "AUTOMATIC_BASE_CHANGE_SUCCEEDED_EVENT")]
        AutomaticBaseChangeSucceededEvent,

        /// <summary>
        /// Represents a 'auto_merge_disabled' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "AUTO_MERGE_DISABLED_EVENT")]
        AutoMergeDisabledEvent,

        /// <summary>
        /// Represents a 'auto_merge_enabled' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "AUTO_MERGE_ENABLED_EVENT")]
        AutoMergeEnabledEvent,

        /// <summary>
        /// Represents a 'auto_rebase_enabled' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "AUTO_REBASE_ENABLED_EVENT")]
        AutoRebaseEnabledEvent,

        /// <summary>
        /// Represents a 'auto_squash_enabled' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "AUTO_SQUASH_ENABLED_EVENT")]
        AutoSquashEnabledEvent,

        /// <summary>
        /// Represents a 'base_ref_changed' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "BASE_REF_CHANGED_EVENT")]
        BaseRefChangedEvent,

        /// <summary>
        /// Represents a 'base_ref_force_pushed' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "BASE_REF_FORCE_PUSHED_EVENT")]
        BaseRefForcePushedEvent,

        /// <summary>
        /// Represents a 'base_ref_deleted' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "BASE_REF_DELETED_EVENT")]
        BaseRefDeletedEvent,

        /// <summary>
        /// Represents a 'deployed' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "DEPLOYED_EVENT")]
        DeployedEvent,

        /// <summary>
        /// Represents a 'deployment_environment_changed' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "DEPLOYMENT_ENVIRONMENT_CHANGED_EVENT")]
        DeploymentEnvironmentChangedEvent,

        /// <summary>
        /// Represents a 'head_ref_deleted' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "HEAD_REF_DELETED_EVENT")]
        HeadRefDeletedEvent,

        /// <summary>
        /// Represents a 'head_ref_force_pushed' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "HEAD_REF_FORCE_PUSHED_EVENT")]
        HeadRefForcePushedEvent,

        /// <summary>
        /// Represents a 'head_ref_restored' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "HEAD_REF_RESTORED_EVENT")]
        HeadRefRestoredEvent,

        /// <summary>
        /// Represents a 'merged' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "MERGED_EVENT")]
        MergedEvent,

        /// <summary>
        /// Represents a 'review_dismissed' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "REVIEW_DISMISSED_EVENT")]
        ReviewDismissedEvent,

        /// <summary>
        /// Represents an 'review_requested' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "REVIEW_REQUESTED_EVENT")]
        ReviewRequestedEvent,

        /// <summary>
        /// Represents an 'review_request_removed' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "REVIEW_REQUEST_REMOVED_EVENT")]
        ReviewRequestRemovedEvent,

        /// <summary>
        /// Represents a 'ready_for_review' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "READY_FOR_REVIEW_EVENT")]
        ReadyForReviewEvent,

        /// <summary>
        /// Represents a 'convert_to_draft' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "CONVERT_TO_DRAFT_EVENT")]
        ConvertToDraftEvent,

        /// <summary>
        /// Represents an 'added_to_merge_queue' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "ADDED_TO_MERGE_QUEUE_EVENT")]
        AddedToMergeQueueEvent,

        /// <summary>
        /// Represents a 'removed_from_merge_queue' event on a given pull request.
        /// </summary>
        [EnumMember(Value = "REMOVED_FROM_MERGE_QUEUE_EVENT")]
        RemovedFromMergeQueueEvent,

        /// <summary>
        /// Represents a comment on an Issue.
        /// </summary>
        [EnumMember(Value = "ISSUE_COMMENT")]
        IssueComment,

        /// <summary>
        /// Represents a mention made by one issue or pull request to another.
        /// </summary>
        [EnumMember(Value = "CROSS_REFERENCED_EVENT")]
        CrossReferencedEvent,

        /// <summary>
        /// Represents a 'added_to_project' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "ADDED_TO_PROJECT_EVENT")]
        AddedToProjectEvent,

        /// <summary>
        /// Represents an 'assigned' event on any assignable object.
        /// </summary>
        [EnumMember(Value = "ASSIGNED_EVENT")]
        AssignedEvent,

        /// <summary>
        /// Represents a 'closed' event on any `Closable`.
        /// </summary>
        [EnumMember(Value = "CLOSED_EVENT")]
        ClosedEvent,

        /// <summary>
        /// Represents a 'comment_deleted' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "COMMENT_DELETED_EVENT")]
        CommentDeletedEvent,

        /// <summary>
        /// Represents a 'connected' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "CONNECTED_EVENT")]
        ConnectedEvent,

        /// <summary>
        /// Represents a 'converted_note_to_issue' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "CONVERTED_NOTE_TO_ISSUE_EVENT")]
        ConvertedNoteToIssueEvent,

        /// <summary>
        /// Represents a 'converted_to_discussion' event on a given issue.
        /// </summary>
        [EnumMember(Value = "CONVERTED_TO_DISCUSSION_EVENT")]
        ConvertedToDiscussionEvent,

        /// <summary>
        /// Represents a 'demilestoned' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "DEMILESTONED_EVENT")]
        DemilestonedEvent,

        /// <summary>
        /// Represents a 'disconnected' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "DISCONNECTED_EVENT")]
        DisconnectedEvent,

        /// <summary>
        /// Represents a 'labeled' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "LABELED_EVENT")]
        LabeledEvent,

        /// <summary>
        /// Represents a 'locked' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "LOCKED_EVENT")]
        LockedEvent,

        /// <summary>
        /// Represents a 'marked_as_duplicate' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "MARKED_AS_DUPLICATE_EVENT")]
        MarkedAsDuplicateEvent,

        /// <summary>
        /// Represents a 'mentioned' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "MENTIONED_EVENT")]
        MentionedEvent,

        /// <summary>
        /// Represents a 'milestoned' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "MILESTONED_EVENT")]
        MilestonedEvent,

        /// <summary>
        /// Represents a 'moved_columns_in_project' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "MOVED_COLUMNS_IN_PROJECT_EVENT")]
        MovedColumnsInProjectEvent,

        /// <summary>
        /// Represents a 'pinned' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "PINNED_EVENT")]
        PinnedEvent,

        /// <summary>
        /// Represents a 'referenced' event on a given `ReferencedSubject`.
        /// </summary>
        [EnumMember(Value = "REFERENCED_EVENT")]
        ReferencedEvent,

        /// <summary>
        /// Represents a 'removed_from_project' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "REMOVED_FROM_PROJECT_EVENT")]
        RemovedFromProjectEvent,

        /// <summary>
        /// Represents a 'renamed' event on a given issue or pull request
        /// </summary>
        [EnumMember(Value = "RENAMED_TITLE_EVENT")]
        RenamedTitleEvent,

        /// <summary>
        /// Represents a 'reopened' event on any `Closable`.
        /// </summary>
        [EnumMember(Value = "REOPENED_EVENT")]
        ReopenedEvent,

        /// <summary>
        /// Represents a 'subscribed' event on a given `Subscribable`.
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED_EVENT")]
        SubscribedEvent,

        /// <summary>
        /// Represents a 'transferred' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "TRANSFERRED_EVENT")]
        TransferredEvent,

        /// <summary>
        /// Represents an 'unassigned' event on any assignable object.
        /// </summary>
        [EnumMember(Value = "UNASSIGNED_EVENT")]
        UnassignedEvent,

        /// <summary>
        /// Represents an 'unlabeled' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "UNLABELED_EVENT")]
        UnlabeledEvent,

        /// <summary>
        /// Represents an 'unlocked' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "UNLOCKED_EVENT")]
        UnlockedEvent,

        /// <summary>
        /// Represents a 'user_blocked' event on a given user.
        /// </summary>
        [EnumMember(Value = "USER_BLOCKED_EVENT")]
        UserBlockedEvent,

        /// <summary>
        /// Represents an 'unmarked_as_duplicate' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "UNMARKED_AS_DUPLICATE_EVENT")]
        UnmarkedAsDuplicateEvent,

        /// <summary>
        /// Represents an 'unpinned' event on a given issue or pull request.
        /// </summary>
        [EnumMember(Value = "UNPINNED_EVENT")]
        UnpinnedEvent,

        /// <summary>
        /// Represents an 'unsubscribed' event on a given `Subscribable`.
        /// </summary>
        [EnumMember(Value = "UNSUBSCRIBED_EVENT")]
        UnsubscribedEvent,
    }
}