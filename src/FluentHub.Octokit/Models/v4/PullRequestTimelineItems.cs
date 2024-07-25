// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An item in a pull request timeline
	/// </summary>
	public class PullRequestTimelineItems
	{
		/// <summary>
		/// Represents an 'added_to_merge_queue' event on a given pull request.
		/// </summary>
		public AddedToMergeQueueEvent AddedToMergeQueueEvent { get; set; }

		/// <summary>
		/// Represents a 'added_to_project' event on a given issue or pull request.
		/// </summary>
		public AddedToProjectEvent AddedToProjectEvent { get; set; }

		/// <summary>
		/// Represents an 'assigned' event on any assignable object.
		/// </summary>
		public AssignedEvent AssignedEvent { get; set; }

		/// <summary>
		/// Represents a 'auto_merge_disabled' event on a given pull request.
		/// </summary>
		public AutoMergeDisabledEvent AutoMergeDisabledEvent { get; set; }

		/// <summary>
		/// Represents a 'auto_merge_enabled' event on a given pull request.
		/// </summary>
		public AutoMergeEnabledEvent AutoMergeEnabledEvent { get; set; }

		/// <summary>
		/// Represents a 'auto_rebase_enabled' event on a given pull request.
		/// </summary>
		public AutoRebaseEnabledEvent AutoRebaseEnabledEvent { get; set; }

		/// <summary>
		/// Represents a 'auto_squash_enabled' event on a given pull request.
		/// </summary>
		public AutoSquashEnabledEvent AutoSquashEnabledEvent { get; set; }

		/// <summary>
		/// Represents a 'automatic_base_change_failed' event on a given pull request.
		/// </summary>
		public AutomaticBaseChangeFailedEvent AutomaticBaseChangeFailedEvent { get; set; }

		/// <summary>
		/// Represents a 'automatic_base_change_succeeded' event on a given pull request.
		/// </summary>
		public AutomaticBaseChangeSucceededEvent AutomaticBaseChangeSucceededEvent { get; set; }

		/// <summary>
		/// Represents a 'base_ref_changed' event on a given issue or pull request.
		/// </summary>
		public BaseRefChangedEvent BaseRefChangedEvent { get; set; }

		/// <summary>
		/// Represents a 'base_ref_deleted' event on a given pull request.
		/// </summary>
		public BaseRefDeletedEvent BaseRefDeletedEvent { get; set; }

		/// <summary>
		/// Represents a 'base_ref_force_pushed' event on a given pull request.
		/// </summary>
		public BaseRefForcePushedEvent BaseRefForcePushedEvent { get; set; }

		/// <summary>
		/// Represents a 'closed' event on any `Closable`.
		/// </summary>
		public ClosedEvent ClosedEvent { get; set; }

		/// <summary>
		/// Represents a 'comment_deleted' event on a given issue or pull request.
		/// </summary>
		public CommentDeletedEvent CommentDeletedEvent { get; set; }

		/// <summary>
		/// Represents a 'connected' event on a given issue or pull request.
		/// </summary>
		public ConnectedEvent ConnectedEvent { get; set; }

		/// <summary>
		/// Represents a 'convert_to_draft' event on a given pull request.
		/// </summary>
		public ConvertToDraftEvent ConvertToDraftEvent { get; set; }

		/// <summary>
		/// Represents a 'converted_note_to_issue' event on a given issue or pull request.
		/// </summary>
		public ConvertedNoteToIssueEvent ConvertedNoteToIssueEvent { get; set; }

		/// <summary>
		/// Represents a 'converted_to_discussion' event on a given issue.
		/// </summary>
		public ConvertedToDiscussionEvent ConvertedToDiscussionEvent { get; set; }

		/// <summary>
		/// Represents a mention made by one issue or pull request to another.
		/// </summary>
		public CrossReferencedEvent CrossReferencedEvent { get; set; }

		/// <summary>
		/// Represents a 'demilestoned' event on a given issue or pull request.
		/// </summary>
		public DemilestonedEvent DemilestonedEvent { get; set; }

		/// <summary>
		/// Represents a 'deployed' event on a given pull request.
		/// </summary>
		public DeployedEvent DeployedEvent { get; set; }

		/// <summary>
		/// Represents a 'deployment_environment_changed' event on a given pull request.
		/// </summary>
		public DeploymentEnvironmentChangedEvent DeploymentEnvironmentChangedEvent { get; set; }

		/// <summary>
		/// Represents a 'disconnected' event on a given issue or pull request.
		/// </summary>
		public DisconnectedEvent DisconnectedEvent { get; set; }

		/// <summary>
		/// Represents a 'head_ref_deleted' event on a given pull request.
		/// </summary>
		public HeadRefDeletedEvent HeadRefDeletedEvent { get; set; }

		/// <summary>
		/// Represents a 'head_ref_force_pushed' event on a given pull request.
		/// </summary>
		public HeadRefForcePushedEvent HeadRefForcePushedEvent { get; set; }

		/// <summary>
		/// Represents a 'head_ref_restored' event on a given pull request.
		/// </summary>
		public HeadRefRestoredEvent HeadRefRestoredEvent { get; set; }

		/// <summary>
		/// Represents a comment on an Issue.
		/// </summary>
		public IssueComment IssueComment { get; set; }

		/// <summary>
		/// Represents a 'labeled' event on a given issue or pull request.
		/// </summary>
		public LabeledEvent LabeledEvent { get; set; }

		/// <summary>
		/// Represents a 'locked' event on a given issue or pull request.
		/// </summary>
		public LockedEvent LockedEvent { get; set; }

		/// <summary>
		/// Represents a 'marked_as_duplicate' event on a given issue or pull request.
		/// </summary>
		public MarkedAsDuplicateEvent MarkedAsDuplicateEvent { get; set; }

		/// <summary>
		/// Represents a 'mentioned' event on a given issue or pull request.
		/// </summary>
		public MentionedEvent MentionedEvent { get; set; }

		/// <summary>
		/// Represents a 'merged' event on a given pull request.
		/// </summary>
		public MergedEvent MergedEvent { get; set; }

		/// <summary>
		/// Represents a 'milestoned' event on a given issue or pull request.
		/// </summary>
		public MilestonedEvent MilestonedEvent { get; set; }

		/// <summary>
		/// Represents a 'moved_columns_in_project' event on a given issue or pull request.
		/// </summary>
		public MovedColumnsInProjectEvent MovedColumnsInProjectEvent { get; set; }

		/// <summary>
		/// Represents a 'pinned' event on a given issue or pull request.
		/// </summary>
		public PinnedEvent PinnedEvent { get; set; }

		/// <summary>
		/// Represents a Git commit part of a pull request.
		/// </summary>
		public PullRequestCommit PullRequestCommit { get; set; }

		/// <summary>
		/// Represents a commit comment thread part of a pull request.
		/// </summary>
		public PullRequestCommitCommentThread PullRequestCommitCommentThread { get; set; }

		/// <summary>
		/// A review object for a given pull request.
		/// </summary>
		public PullRequestReview PullRequestReview { get; set; }

		/// <summary>
		/// A threaded list of comments for a given pull request.
		/// </summary>
		public PullRequestReviewThread PullRequestReviewThread { get; set; }

		/// <summary>
		/// Represents the latest point in the pull request timeline for which the viewer has seen the pull request's commits.
		/// </summary>
		public PullRequestRevisionMarker PullRequestRevisionMarker { get; set; }

		/// <summary>
		/// Represents a 'ready_for_review' event on a given pull request.
		/// </summary>
		public ReadyForReviewEvent ReadyForReviewEvent { get; set; }

		/// <summary>
		/// Represents a 'referenced' event on a given `ReferencedSubject`.
		/// </summary>
		public ReferencedEvent ReferencedEvent { get; set; }

		/// <summary>
		/// Represents a 'removed_from_merge_queue' event on a given pull request.
		/// </summary>
		public RemovedFromMergeQueueEvent RemovedFromMergeQueueEvent { get; set; }

		/// <summary>
		/// Represents a 'removed_from_project' event on a given issue or pull request.
		/// </summary>
		public RemovedFromProjectEvent RemovedFromProjectEvent { get; set; }

		/// <summary>
		/// Represents a 'renamed' event on a given issue or pull request
		/// </summary>
		public RenamedTitleEvent RenamedTitleEvent { get; set; }

		/// <summary>
		/// Represents a 'reopened' event on any `Closable`.
		/// </summary>
		public ReopenedEvent ReopenedEvent { get; set; }

		/// <summary>
		/// Represents a 'review_dismissed' event on a given issue or pull request.
		/// </summary>
		public ReviewDismissedEvent ReviewDismissedEvent { get; set; }

		/// <summary>
		/// Represents an 'review_request_removed' event on a given pull request.
		/// </summary>
		public ReviewRequestRemovedEvent ReviewRequestRemovedEvent { get; set; }

		/// <summary>
		/// Represents an 'review_requested' event on a given pull request.
		/// </summary>
		public ReviewRequestedEvent ReviewRequestedEvent { get; set; }

		/// <summary>
		/// Represents a 'subscribed' event on a given `Subscribable`.
		/// </summary>
		public SubscribedEvent SubscribedEvent { get; set; }

		/// <summary>
		/// Represents a 'transferred' event on a given issue or pull request.
		/// </summary>
		public TransferredEvent TransferredEvent { get; set; }

		/// <summary>
		/// Represents an 'unassigned' event on any assignable object.
		/// </summary>
		public UnassignedEvent UnassignedEvent { get; set; }

		/// <summary>
		/// Represents an 'unlabeled' event on a given issue or pull request.
		/// </summary>
		public UnlabeledEvent UnlabeledEvent { get; set; }

		/// <summary>
		/// Represents an 'unlocked' event on a given issue or pull request.
		/// </summary>
		public UnlockedEvent UnlockedEvent { get; set; }

		/// <summary>
		/// Represents an 'unmarked_as_duplicate' event on a given issue or pull request.
		/// </summary>
		public UnmarkedAsDuplicateEvent UnmarkedAsDuplicateEvent { get; set; }

		/// <summary>
		/// Represents an 'unpinned' event on a given issue or pull request.
		/// </summary>
		public UnpinnedEvent UnpinnedEvent { get; set; }

		/// <summary>
		/// Represents an 'unsubscribed' event on a given `Subscribable`.
		/// </summary>
		public UnsubscribedEvent UnsubscribedEvent { get; set; }

		/// <summary>
		/// Represents a 'user_blocked' event on a given user.
		/// </summary>
		public UserBlockedEvent UserBlockedEvent { get; set; }
	}
}
