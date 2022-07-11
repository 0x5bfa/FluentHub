namespace FluentHub.Octokit.Models.v4
{
    using System;

    /// <summary>
    /// An item in an issue timeline
    /// </summary>
public class IssueTimelineItems
    {
        
            /// <summary>
            /// Represents a 'added_to_project' event on a given issue or pull request.
            /// </summary>
        public AddedToProjectEvent AddedToProjectEvent { get; set; }

            /// <summary>
            /// Represents an 'assigned' event on any assignable object.
            /// </summary>
        public AssignedEvent AssignedEvent { get; set; }

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
            /// Represents a 'disconnected' event on a given issue or pull request.
            /// </summary>
        public DisconnectedEvent DisconnectedEvent { get; set; }

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
            /// Represents a 'referenced' event on a given `ReferencedSubject`.
            /// </summary>
        public ReferencedEvent ReferencedEvent { get; set; }

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