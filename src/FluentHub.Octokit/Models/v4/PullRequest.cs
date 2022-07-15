namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository pull request.
    /// </summary>
    public class PullRequest
    {
        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; set; }

        /// <summary>
        /// The number of additions in this pull request.
        /// </summary>
        public int Additions { get; set; }

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
        /// Returns the auto-merge request object if one exists for this pull request.
        /// </summary>
        public AutoMergeRequest AutoMergeRequest { get; set; }

        /// <summary>
        /// Identifies the base Ref associated with the pull request.
        /// </summary>
        public Ref BaseRef { get; set; }

        /// <summary>
        /// Identifies the name of the base Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string BaseRefName { get; set; }

        /// <summary>
        /// Identifies the oid of the base ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string BaseRefOid { get; set; }

        /// <summary>
        /// The repository associated with this pull request's base Ref.
        /// </summary>
        public Repository BaseRepository { get; set; }

        /// <summary>
        /// The body as Markdown.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; set; }

        /// <summary>
        /// The body rendered to text.
        /// </summary>
        public string BodyText { get; set; }

        /// <summary>
        /// The number of changed files in this pull request.
        /// </summary>
        public int ChangedFiles { get; set; }

        /// <summary>
        /// The HTTP path for the checks of this pull request.
        /// </summary>
        public string ChecksResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the checks of this pull request.
        /// </summary>
        public string ChecksUrl { get; set; }

        /// <summary>
        /// `true` if the pull request is closed
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// List of issues that were may be closed by this pull request
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection</param>
        /// <param name="userLinkedOnly">Return only manually linked Issues</param>
        public IssueConnection ClosingIssuesReferences { get; set; }

        /// <summary>
        /// A list of comments associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issue comments returned from the connection.</param>
        public IssueCommentConnection Comments { get; set; }

        /// <summary>
        /// A list of commits present in this pull request's head branch not present in the base branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestCommitConnection Commits { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        public bool CreatedViaEmail { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The number of deletions in this pull request.
        /// </summary>
        public int Deletions { get; set; }

        /// <summary>
        /// The actor who edited this pull request's body.
        /// </summary>
        public IActor Editor { get; set; }

        /// <summary>
        /// Lists the files changed within this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestChangedFileConnection Files { get; set; }

        /// <summary>
        /// Identifies the head Ref associated with the pull request.
        /// </summary>
        public Ref HeadRef { get; set; }

        /// <summary>
        /// Identifies the name of the head Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string HeadRefName { get; set; }

        /// <summary>
        /// Identifies the oid of the head ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string HeadRefOid { get; set; }

        /// <summary>
        /// The repository associated with this pull request's head Ref.
        /// </summary>
        public Repository HeadRepository { get; set; }

        /// <summary>
        /// The owner of the repository associated with this pull request's head Ref.
        /// </summary>
        public IRepositoryOwner HeadRepositoryOwner { get; set; }

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
        /// The head and base repositories are different.
        /// </summary>
        public bool IsCrossRepository { get; set; }

        /// <summary>
        /// Identifies if the pull request is a draft.
        /// </summary>
        public bool IsDraft { get; set; }

        /// <summary>
        /// Is this pull request read by the viewer
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
        /// A list of latest reviews per user associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="writersOnly">Only return reviews from user who have write access to the repository</param>
        public PullRequestReviewConnection LatestOpinionatedReviews { get; set; }

        /// <summary>
        /// A list of latest reviews per user associated with the pull request that are not also pending review.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestReviewConnection LatestReviews { get; set; }

        /// <summary>
        /// `true` if the pull request is locked
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// Indicates whether maintainers can modify the pull request.
        /// </summary>
        public bool MaintainerCanModify { get; set; }

        /// <summary>
        /// The commit that was created when this pull request was merged.
        /// </summary>
        public Commit MergeCommit { get; set; }

        /// <summary>
        /// Whether or not the pull request can be merged based on the existence of merge conflicts.
        /// </summary>
        public MergeableState Mergeable { get; set; }

        /// <summary>
        /// Whether or not the pull request was merged.
        /// </summary>
        public bool Merged { get; set; }

        /// <summary>
        /// The date and time that the pull request was merged.
        /// </summary>
        public DateTimeOffset? MergedAt { get; set; }

        /// <summary>
        /// The actor who merged the pull request.
        /// </summary>
        public IActor MergedBy { get; set; }

        /// <summary>
        /// Identifies the milestone associated with the pull request.
        /// </summary>
        public Milestone Milestone { get; set; }

        /// <summary>
        /// Identifies the pull request number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// A list of Users that are participating in the Pull Request conversation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Participants { get; set; }

        /// <summary>
        /// The permalink to the pull request.
        /// </summary>
        public string Permalink { get; set; }

        /// <summary>
        /// The commit that GitHub automatically generated to test if this pull request could be merged. This field will not return a value if the pull request is merged, or if the test merge commit is still being generated. See the `mergeable` field for more details on the mergeability of the pull request.
        /// </summary>
        public Commit PotentialMergeCommit { get; set; }

        /// <summary>
        /// List of project cards associated with this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection ProjectCards { get; set; }

        /// <summary>
        /// List of project items associated with this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeArchived">Include archived items.</param>
        public ProjectV2ItemConnection ProjectItems { get; set; }

        /// <summary>
        /// Find a project by project (beta) number.
        /// </summary>
        /// <param name="number">The project (beta) number.</param>
        public ProjectNext ProjectNext { get; set; }

        /// <summary>
        /// List of project (beta) items associated with this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeArchived">Include archived items.</param>
        public ProjectNextItemConnection ProjectNextItems { get; set; }

        /// <summary>
        /// Find a project by number.
        /// </summary>
        /// <param name="number">The project number.</param>
        public ProjectV2 ProjectV2 { get; set; }

        /// <summary>
        /// A list of projects (beta) under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">A project (beta) to search for under the the owner.</param>
        /// <param name="sortBy">How to order the returned projects (beta).</param>
        public ProjectNextConnection ProjectsNext { get; set; }

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
        /// The HTTP path for this pull request.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The HTTP path for reverting this pull request.
        /// </summary>
        public string RevertResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for reverting this pull request.
        /// </summary>
        public string RevertUrl { get; set; }

        /// <summary>
        /// The current status of this pull request with respect to code review.
        /// </summary>
        public PullRequestReviewDecision? ReviewDecision { get; set; }

        /// <summary>
        /// A list of review requests associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReviewRequestConnection ReviewRequests { get; set; }

        /// <summary>
        /// The list of all review threads for this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestReviewThreadConnection ReviewThreads { get; set; }

        /// <summary>
        /// A list of reviews associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="author">Filter by author of the review.</param>
        /// <param name="states">A list of states to filter the reviews.</param>
        public PullRequestReviewConnection Reviews { get; set; }

        /// <summary>
        /// Identifies the state of the pull request.
        /// </summary>
        public PullRequestState State { get; set; }

        /// <summary>
        /// A list of reviewer suggestions based on commit history and past review comments.
        /// </summary>
        public List<SuggestedReviewer> SuggestedReviewers { get; set; }

        /// <summary>
        /// A list of events, comments, commits, etc. associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        public PullRequestTimelineConnection Timeline { get; set; }

        /// <summary>
        /// A list of events, comments, commits, etc. associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="itemTypes">Filter timeline items by type.</param>
        /// <param name="since">Filter timeline items by a `since` timestamp.</param>
        /// <param name="skip">Skips the first _n_ elements in the list.</param>
        public PullRequestTimelineItemsConnection TimelineItems { get; set; }

        /// <summary>
        /// Identifies the pull request title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifies the pull request title rendered to HTML.
        /// </summary>
        public string TitleHTML { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The HTTP URL for this pull request.
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
        /// Whether or not the viewer can apply suggestion.
        /// </summary>
        public bool ViewerCanApplySuggestion { get; set; }

        /// <summary>
        /// Check if the viewer can restore the deleted head ref.
        /// </summary>
        public bool ViewerCanDeleteHeadRef { get; set; }

        /// <summary>
        /// Whether or not the viewer can disable auto-merge
        /// </summary>
        public bool ViewerCanDisableAutoMerge { get; set; }

        /// <summary>
        /// Can the viewer edit files within this pull request.
        /// </summary>
        public bool ViewerCanEditFiles { get; set; }

        /// <summary>
        /// Whether or not the viewer can enable auto-merge
        /// </summary>
        public bool ViewerCanEnableAutoMerge { get; set; }

        /// <summary>
        /// Indicates whether the viewer can bypass branch protections and merge the pull request immediately
        /// </summary>
        public bool ViewerCanMergeAsAdmin { get; set; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; set; }

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
        /// The latest review given from the viewer.
        /// </summary>
        public PullRequestReview ViewerLatestReview { get; set; }

        /// <summary>
        /// The person who has requested the viewer for review on this pull request.
        /// </summary>
        public ReviewRequest ViewerLatestReviewRequest { get; set; }

        /// <summary>
        /// The merge body text for the viewer and method.
        /// </summary>
        /// <param name="mergeType">The merge method for the message.</param>
        public string ViewerMergeBodyText { get; set; }

        /// <summary>
        /// The merge headline text for the viewer and method.
        /// </summary>
        /// <param name="mergeType">The merge method for the message.</param>
        public string ViewerMergeHeadlineText { get; set; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; set; }
    }
}