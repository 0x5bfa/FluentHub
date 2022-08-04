namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// An item in a pull request timeline
    /// </summary>
    public class PullRequestTimelineItem
    {
        /// <summary>
        /// Represents an 'assigned' event on any assignable object.
        /// </summary>
        public AssignedEvent AssignedEvent { get; set; }

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
        /// Represents a Git commit.
        /// </summary>
        public Commit Commit { get; set; }

        /// <summary>
        /// A thread of comments on a commit.
        /// </summary>
        public CommitCommentThread CommitCommentThread { get; set; }

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
        /// Represents a 'merged' event on a given pull request.
        /// </summary>
        public MergedEvent MergedEvent { get; set; }

        /// <summary>
        /// Represents a 'milestoned' event on a given issue or pull request.
        /// </summary>
        public MilestonedEvent MilestonedEvent { get; set; }

        /// <summary>
        /// A review object for a given pull request.
        /// </summary>
        public PullRequestReview PullRequestReview { get; set; }

        /// <summary>
        /// A review comment associated with a given repository pull request.
        /// </summary>
        public PullRequestReviewComment PullRequestReviewComment { get; set; }

        /// <summary>
        /// A threaded list of comments for a given pull request.
        /// </summary>
        public PullRequestReviewThread PullRequestReviewThread { get; set; }

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