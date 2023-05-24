namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; set; }

        /// <summary>
        /// A list of Users assigned to this object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Assignees { get; set; }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author { get; set; }

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        public CommentAuthorAssociation AuthorAssociation { get; set; }

        /// <summary>
        /// Identifies the body of the issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; set; }

        /// <summary>
        /// The http path for this issue body
        /// </summary>
        public string BodyResourcePath { get; set; }

        /// <summary>
        /// Identifies the body of the issue rendered to text.
        /// </summary>
        public string BodyText { get; set; }

        /// <summary>
        /// The http URL for this issue body
        /// </summary>
        public string BodyUrl { get; set; }

        /// <summary>
        /// Indicates if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was closed."
        /// <summary>
        public string ClosedAtHumanized { get; set; }

        /// <summary>
        /// A list of comments associated with the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issue comments returned from the connection.</param>
        public IssueCommentConnection Comments { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        public bool CreatedViaEmail { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor { get; set; }

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; set; }

        /// <summary>
        /// The hovercard information for this issue
        /// </summary>
        /// <param name="includeNotificationContexts">Whether or not to include notification contexts</param>
        public Hovercard Hovercard { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; set; }

        /// <summary>
        /// Indicates whether or not this issue is currently pinned to the repository issues list
        /// </summary>
        public bool? IsPinned { get; set; }

        /// <summary>
        /// Is this issue read by the viewer
        /// </summary>
        public bool? IsReadByViewer { get; set; }

        /// <summary>
        /// A list of labels associated with the object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        public LabelConnection Labels { get; set; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; set; }

        /// <summary>
        /// Humanized string of "The moment the editor made the last edit"
        /// <summary>
        public string LastEditedAtHumanized { get; set; }

        /// <summary>
        /// Branches linked to this issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public LinkedBranchConnection LinkedBranches { get; set; }

        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Identifies the milestone associated with the issue.
        /// </summary>
        public Milestone Milestone { get; set; }

        /// <summary>
        /// Identifies the issue number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// A list of Users that are participating in the Issue conversation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Participants { get; set; }

        /// <summary>
        /// List of project cards associated with this issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection ProjectCards { get; set; }

        /// <summary>
        /// List of project items associated with this issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeArchived">Include archived items.</param>
        public ProjectV2ItemConnection ProjectItems { get; set; }

        /// <summary>
        /// Find a project by number.
        /// </summary>
        /// <param name="number">The project number.</param>
        public ProjectV2 ProjectV2 { get; set; }

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">How to order the returned projects.</param>
        /// <param name="query">A project to search for under the the owner.</param>
        public ProjectV2Connection ProjectsV2 { get; set; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public DateTimeOffset? PublishedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies when the comment was published at."
        /// <summary>
        public string PublishedAtHumanized { get; set; }

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        public List<ReactionGroup> ReactionGroups { get; set; }

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        public ReactionConnection Reactions { get; set; }

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Identifies the state of the issue.
        /// </summary>
        public IssueState State { get; set; }

        /// <summary>
        /// Identifies the reason for the issue state.
        /// </summary>
        public IssueStateReason? StateReason { get; set; }

        /// <summary>
        /// A list of events, comments, commits, etc. associated with the issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        public IssueTimelineConnection Timeline { get; set; }

        /// <summary>
        /// A list of events, comments, commits, etc. associated with the issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="itemTypes">Filter timeline items by type.</param>
        /// <param name="since">Filter timeline items by a `since` timestamp.</param>
        /// <param name="skip">Skips the first _n_ elements in the list.</param>
        public IssueTimelineItemsConnection TimelineItems { get; set; }

        /// <summary>
        /// Identifies the issue title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifies the issue title rendered to HTML.
        /// </summary>
        public string TitleHTML { get; set; }

        /// <summary>
        /// A list of issues that track this issue
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public IssueConnection TrackedInIssues { get; set; }

        /// <summary>
        /// A list of issues tracked inside the current issue
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public IssueConnection TrackedIssues { get; set; }

        /// <summary>
        /// The number of tracked issues for this issue
        /// </summary>
        /// <param name="states">Limit the count to tracked issues with the specified states.</param>
        public int TrackedIssuesCount { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }

        /// <summary>
        /// The HTTP URL for this issue
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A list of edits to this content.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserContentEditConnection UserContentEdits { get; set; }

        /// <summary>
        /// Indicates if the object can be closed by the viewer.
        /// </summary>
        public bool ViewerCanClose { get; set; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; set; }

        /// <summary>
        /// Indicates if the object can be reopened by the viewer.
        /// </summary>
        public bool ViewerCanReopen { get; set; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; set; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; set; }

        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        public List<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; set; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; set; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; set; }
    }
}