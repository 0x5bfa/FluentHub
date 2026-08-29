namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository pull request.
    /// </summary>
    public class PullRequest : QueryableValue<PullRequest>
    {
        internal PullRequest(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; }

        /// <summary>
        /// The number of additions in this pull request.
        /// </summary>
        public int Additions { get; }

        /// <summary>
        /// A list of Users assigned to this object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Assignees(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        public CommentAuthorAssociation AuthorAssociation { get; }

        /// <summary>
        /// Returns the auto-merge request object if one exists for this pull request.
        /// </summary>
        public AutoMergeRequest AutoMergeRequest => this.CreateProperty(x => x.AutoMergeRequest, Octokit.GraphQL.Model.AutoMergeRequest.Create);

        /// <summary>
        /// Identifies the base Ref associated with the pull request.
        /// </summary>
        public Ref BaseRef => this.CreateProperty(x => x.BaseRef, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Identifies the name of the base Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string BaseRefName { get; }

        /// <summary>
        /// Identifies the oid of the base ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string BaseRefOid { get; }

        /// <summary>
        /// The repository associated with this pull request's base Ref.
        /// </summary>
        public Repository BaseRepository => this.CreateProperty(x => x.BaseRepository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The body as Markdown.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// The body rendered to text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// The number of changed files in this pull request.
        /// </summary>
        public int ChangedFiles { get; }

        /// <summary>
        /// The HTTP path for the checks of this pull request.
        /// </summary>
        public string ChecksResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the checks of this pull request.
        /// </summary>
        public string ChecksUrl { get; }

        /// <summary>
        /// `true` if the pull request is closed
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; }

        /// <summary>
        /// List of issues that were may be closed by this pull request
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection</param>
        /// <param name="userLinkedOnly">Return only manually linked Issues</param>
        public IssueConnection ClosingIssuesReferences(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IssueOrder>? orderBy = null, Arg<bool>? userLinkedOnly = null) => this.CreateMethodCall(x => x.ClosingIssuesReferences(first, after, last, before, orderBy, userLinkedOnly), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// A list of comments associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issue comments returned from the connection.</param>
        public IssueCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IssueCommentOrder>? orderBy = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before, orderBy), Octokit.GraphQL.Model.IssueCommentConnection.Create);

        /// <summary>
        /// A list of commits present in this pull request's head branch not present in the base branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestCommitConnection Commits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Commits(first, after, last, before), Octokit.GraphQL.Model.PullRequestCommitConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        public bool CreatedViaEmail { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"`databaseId` will be removed because it does not support 64-bit signed integer identifiers. Use `fullDatabaseId` instead. Removal on 2024-07-01 UTC.")]
        public long? DatabaseId { get; }

        /// <summary>
        /// The number of deletions in this pull request.
        /// </summary>
        public int Deletions { get; }

        /// <summary>
        /// The actor who edited this pull request's body.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Lists the files changed within this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestChangedFileConnection Files(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Files(first, after, last, before), Octokit.GraphQL.Model.PullRequestChangedFileConnection.Create);

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; }

        /// <summary>
        /// Identifies the head Ref associated with the pull request.
        /// </summary>
        public Ref HeadRef => this.CreateProperty(x => x.HeadRef, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Identifies the name of the head Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string HeadRefName { get; }

        /// <summary>
        /// Identifies the oid of the head ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string HeadRefOid { get; }

        /// <summary>
        /// The repository associated with this pull request's head Ref.
        /// </summary>
        public Repository HeadRepository => this.CreateProperty(x => x.HeadRepository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The owner of the repository associated with this pull request's head Ref.
        /// </summary>
        public IRepositoryOwner HeadRepositoryOwner => this.CreateProperty(x => x.HeadRepositoryOwner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// The hovercard information for this issue
        /// </summary>
        /// <param name="includeNotificationContexts">Whether or not to include notification contexts</param>
        public Hovercard Hovercard(Arg<bool>? includeNotificationContexts = null) => this.CreateMethodCall(x => x.Hovercard(includeNotificationContexts), Octokit.GraphQL.Model.Hovercard.Create);

        /// <summary>
        /// The Node ID of the PullRequest object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; }

        /// <summary>
        /// The head and base repositories are different.
        /// </summary>
        public bool IsCrossRepository { get; }

        /// <summary>
        /// Identifies if the pull request is a draft.
        /// </summary>
        public bool IsDraft { get; }

        /// <summary>
        /// Indicates whether the pull request is in a merge queue
        /// </summary>
        public bool IsInMergeQueue { get; }

        /// <summary>
        /// Indicates whether the pull request's base ref has a merge queue enabled.
        /// </summary>
        public bool IsMergeQueueEnabled { get; }

        /// <summary>
        /// Is this pull request read by the viewer
        /// </summary>
        public bool? IsReadByViewer { get; }

        /// <summary>
        /// A list of labels associated with the object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LabelOrder>? orderBy = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before, orderBy), Octokit.GraphQL.Model.LabelConnection.Create);

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// A list of latest reviews per user associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="writersOnly">Only return reviews from user who have write access to the repository</param>
        public PullRequestReviewConnection LatestOpinionatedReviews(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? writersOnly = null) => this.CreateMethodCall(x => x.LatestOpinionatedReviews(first, after, last, before, writersOnly), Octokit.GraphQL.Model.PullRequestReviewConnection.Create);

        /// <summary>
        /// A list of latest reviews per user associated with the pull request that are not also pending review.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestReviewConnection LatestReviews(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.LatestReviews(first, after, last, before), Octokit.GraphQL.Model.PullRequestReviewConnection.Create);

        /// <summary>
        /// `true` if the pull request is locked
        /// </summary>
        public bool Locked { get; }

        /// <summary>
        /// Indicates whether maintainers can modify the pull request.
        /// </summary>
        public bool MaintainerCanModify { get; }

        /// <summary>
        /// The commit that was created when this pull request was merged.
        /// </summary>
        public Commit MergeCommit => this.CreateProperty(x => x.MergeCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The merge queue for the pull request's base branch
        /// </summary>
        public MergeQueue MergeQueue => this.CreateProperty(x => x.MergeQueue, Octokit.GraphQL.Model.MergeQueue.Create);

        /// <summary>
        /// The merge queue entry of the pull request in the base branch's merge queue
        /// </summary>
        public MergeQueueEntry MergeQueueEntry => this.CreateProperty(x => x.MergeQueueEntry, Octokit.GraphQL.Model.MergeQueueEntry.Create);

        /// <summary>
        /// Whether or not the pull request can be merged based on the existence of merge conflicts.
        /// </summary>
        public MergeableState Mergeable { get; }

        /// <summary>
        /// Whether or not the pull request was merged.
        /// </summary>
        public bool Merged { get; }

        /// <summary>
        /// The date and time that the pull request was merged.
        /// </summary>
        public DateTimeOffset? MergedAt { get; }

        /// <summary>
        /// The actor who merged the pull request.
        /// </summary>
        public IActor MergedBy => this.CreateProperty(x => x.MergedBy, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the milestone associated with the pull request.
        /// </summary>
        public Milestone Milestone => this.CreateProperty(x => x.Milestone, Octokit.GraphQL.Model.Milestone.Create);

        /// <summary>
        /// Identifies the pull request number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// A list of Users that are participating in the Pull Request conversation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Participants(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Participants(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// The permalink to the pull request.
        /// </summary>
        public string Permalink { get; }

        /// <summary>
        /// The commit that GitHub automatically generated to test if this pull request could be merged. This field will not return a value if the pull request is merged, or if the test merge commit is still being generated. See the `mergeable` field for more details on the mergeability of the pull request.
        /// </summary>
        public Commit PotentialMergeCommit => this.CreateProperty(x => x.PotentialMergeCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// List of project cards associated with this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection ProjectCards(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<ProjectCardArchivedState?>>? archivedStates = null) => this.CreateMethodCall(x => x.ProjectCards(first, after, last, before, archivedStates), Octokit.GraphQL.Model.ProjectCardConnection.Create);

        /// <summary>
        /// List of project items associated with this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeArchived">Include archived items.</param>
        public ProjectV2ItemConnection ProjectItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includeArchived = null) => this.CreateMethodCall(x => x.ProjectItems(first, after, last, before, includeArchived), Octokit.GraphQL.Model.ProjectV2ItemConnection.Create);

        /// <summary>
        /// Find a project by number.
        /// </summary>
        /// <param name="number">The project number.</param>
        public ProjectV2 ProjectV2(Arg<int> number) => this.CreateMethodCall(x => x.ProjectV2(number), Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">How to order the returned projects.</param>
        /// <param name="query">A project to search for under the the owner.</param>
        public ProjectV2Connection ProjectsV2(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2Order>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.ProjectsV2(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public DateTimeOffset? PublishedAt { get; }

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        public IQueryableList<ReactionGroup> ReactionGroups => this.CreateProperty(x => x.ReactionGroups);

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        public ReactionConnection Reactions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ReactionContent>? content = null, Arg<ReactionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octokit.GraphQL.Model.ReactionConnection.Create);

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this pull request.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP path for reverting this pull request.
        /// </summary>
        public string RevertResourcePath { get; }

        /// <summary>
        /// The HTTP URL for reverting this pull request.
        /// </summary>
        public string RevertUrl { get; }

        /// <summary>
        /// The current status of this pull request with respect to code review.
        /// </summary>
        public PullRequestReviewDecision? ReviewDecision { get; }

        /// <summary>
        /// A list of review requests associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReviewRequestConnection ReviewRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ReviewRequests(first, after, last, before), Octokit.GraphQL.Model.ReviewRequestConnection.Create);

        /// <summary>
        /// The list of all review threads for this pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PullRequestReviewThreadConnection ReviewThreads(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ReviewThreads(first, after, last, before), Octokit.GraphQL.Model.PullRequestReviewThreadConnection.Create);

        /// <summary>
        /// A list of reviews associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="author">Filter by author of the review.</param>
        /// <param name="states">A list of states to filter the reviews.</param>
        public PullRequestReviewConnection Reviews(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? author = null, Arg<IEnumerable<PullRequestReviewState>>? states = null) => this.CreateMethodCall(x => x.Reviews(first, after, last, before, author, states), Octokit.GraphQL.Model.PullRequestReviewConnection.Create);

        /// <summary>
        /// Identifies the state of the pull request.
        /// </summary>
        public PullRequestState State { get; }

        /// <summary>
        /// A list of reviewer suggestions based on commit history and past review comments.
        /// </summary>
        public IQueryableList<SuggestedReviewer> SuggestedReviewers => this.CreateProperty(x => x.SuggestedReviewers);

        /// <summary>
        /// A list of events, comments, commits, etc. associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        public PullRequestTimelineConnection Timeline(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<DateTimeOffset>? since = null) => this.CreateMethodCall(x => x.Timeline(first, after, last, before, since), Octokit.GraphQL.Model.PullRequestTimelineConnection.Create);

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
        public PullRequestTimelineItemsConnection TimelineItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PullRequestTimelineItemsItemType>>? itemTypes = null, Arg<DateTimeOffset>? since = null, Arg<int>? skip = null) => this.CreateMethodCall(x => x.TimelineItems(first, after, last, before, itemTypes, since, skip), Octokit.GraphQL.Model.PullRequestTimelineItemsConnection.Create);

        /// <summary>
        /// Identifies the pull request title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the pull request title rendered to HTML.
        /// </summary>
        public string TitleHTML { get; }

        /// <summary>
        /// Returns a count of how many comments this pull request has received.
        /// </summary>
        public int? TotalCommentsCount { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this pull request.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// A list of edits to this content.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserContentEditConnection UserContentEdits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.UserContentEdits(first, after, last, before), Octokit.GraphQL.Model.UserContentEditConnection.Create);

        /// <summary>
        /// Whether or not the viewer can apply suggestion.
        /// </summary>
        public bool ViewerCanApplySuggestion { get; }

        /// <summary>
        /// Indicates if the object can be closed by the viewer.
        /// </summary>
        public bool ViewerCanClose { get; }

        /// <summary>
        /// Check if the viewer can restore the deleted head ref.
        /// </summary>
        public bool ViewerCanDeleteHeadRef { get; }

        /// <summary>
        /// Whether or not the viewer can disable auto-merge
        /// </summary>
        public bool ViewerCanDisableAutoMerge { get; }

        /// <summary>
        /// Can the viewer edit files within this pull request.
        /// </summary>
        public bool ViewerCanEditFiles { get; }

        /// <summary>
        /// Whether or not the viewer can enable auto-merge
        /// </summary>
        public bool ViewerCanEnableAutoMerge { get; }

        /// <summary>
        /// Indicates whether the viewer can bypass branch protections and merge the pull request immediately
        /// </summary>
        public bool ViewerCanMergeAsAdmin { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

        /// <summary>
        /// Indicates if the object can be reopened by the viewer.
        /// </summary>
        public bool ViewerCanReopen { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; }

        /// <summary>
        /// Whether or not the viewer can update the head ref of this PR, by merging or rebasing the base ref.
        /// If the head ref is up to date or unable to be updated by this user, this will return false.
        /// </summary>
        public bool ViewerCanUpdateBranch { get; }

        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        /// <summary>
        /// The latest review given from the viewer.
        /// </summary>
        public PullRequestReview ViewerLatestReview => this.CreateProperty(x => x.ViewerLatestReview, Octokit.GraphQL.Model.PullRequestReview.Create);

        /// <summary>
        /// The person who has requested the viewer for review on this pull request.
        /// </summary>
        public ReviewRequest ViewerLatestReviewRequest => this.CreateProperty(x => x.ViewerLatestReviewRequest, Octokit.GraphQL.Model.ReviewRequest.Create);

        /// <summary>
        /// The merge body text for the viewer and method.
        /// </summary>
        /// <param name="mergeType">The merge method for the message.</param>
        public string ViewerMergeBodyText(Arg<PullRequestMergeMethod>? mergeType = null) => default;

        /// <summary>
        /// The merge headline text for the viewer and method.
        /// </summary>
        /// <param name="mergeType">The merge method for the message.</param>
        public string ViewerMergeHeadlineText(Arg<PullRequestMergeMethod>? mergeType = null) => default;

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; }

        internal static PullRequest Create(Expression expression)
        {
            return new PullRequest(expression);
        }
    }
}