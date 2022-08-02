namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// An item in an issue timeline
    /// </summary>
    public class IssueTimelineItem
    {
        
        /// <summary>
        /// Represents an 'assigned' event on any assignable object.
        /// </summary>
            public AssignedEvent AssignedEvent { get; set; }

        /// <summary>
        /// Represents a 'closed' event on any `Closable`.
        /// </summary>
            public ClosedEvent ClosedEvent { get; set; }

        /// <summary>
        /// Represents a Git commit.
        /// </summary>
            public Commit Commit { get; set; }

        /// <summary>
        /// Represents a mention made by one issue or pull request to another.
        /// </summary>
            public CrossReferencedEvent CrossReferencedEvent { get; set; }

        /// <summary>
        /// Represents a 'demilestoned' event on a given issue or pull request.
        /// </summary>
            public DemilestonedEvent DemilestonedEvent { get; set; }

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
        /// Represents a 'milestoned' event on a given issue or pull request.
        /// </summary>
            public MilestonedEvent MilestonedEvent { get; set; }

        /// <summary>
        /// Represents a 'referenced' event on a given `ReferencedSubject`.
        /// </summary>
            public ReferencedEvent ReferencedEvent { get; set; }

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
        /// Represents an 'unsubscribed' event on a given `Subscribable`.
        /// </summary>
            public UnsubscribedEvent UnsubscribedEvent { get; set; }

        /// <summary>
        /// Represents a 'user_blocked' event on a given user.
        /// </summary>
            public UserBlockedEvent UserBlockedEvent { get; set; }
    }
}