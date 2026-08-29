namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
    /// </summary>
    public class Issue : QueryableValue<Issue>
    {
        internal Issue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; }

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
        /// Identifies the body of the issue.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// The http path for this issue body
        /// </summary>
        public string BodyResourcePath { get; }

        /// <summary>
        /// Identifies the body of the issue rendered to text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// The http URL for this issue body
        /// </summary>
        public string BodyUrl { get; }

        /// <summary>
        /// Indicates if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; }

        /// <summary>
        /// A list of comments associated with the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issue comments returned from the connection.</param>
        public IssueCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IssueCommentOrder>? orderBy = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before, orderBy), Octokit.GraphQL.Model.IssueCommentConnection.Create);

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
        public long? DatabaseId { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; }

        /// <summary>
        /// The hovercard information for this issue
        /// </summary>
        /// <param name="includeNotificationContexts">Whether or not to include notification contexts</param>
        public Hovercard Hovercard(Arg<bool>? includeNotificationContexts = null) => this.CreateMethodCall(x => x.Hovercard(includeNotificationContexts), Octokit.GraphQL.Model.Hovercard.Create);

        /// <summary>
        /// The Node ID of the Issue object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; }

        /// <summary>
        /// Indicates whether or not this issue is currently pinned to the repository issues list
        /// </summary>
        public bool? IsPinned { get; }

        /// <summary>
        /// Is this issue read by the viewer
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
        /// Branches linked to this issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public LinkedBranchConnection LinkedBranches(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.LinkedBranches(first, after, last, before), Octokit.GraphQL.Model.LinkedBranchConnection.Create);

        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        public bool Locked { get; }

        /// <summary>
        /// Identifies the milestone associated with the issue.
        /// </summary>
        public Milestone Milestone => this.CreateProperty(x => x.Milestone, Octokit.GraphQL.Model.Milestone.Create);

        /// <summary>
        /// Identifies the issue number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// A list of Users that are participating in the Issue conversation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Participants(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Participants(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// List of project cards associated with this issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="archivedStates">A list of archived states to filter the cards by</param>
        public ProjectCardConnection ProjectCards(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<ProjectCardArchivedState?>>? archivedStates = null) => this.CreateMethodCall(x => x.ProjectCards(first, after, last, before, archivedStates), Octokit.GraphQL.Model.ProjectCardConnection.Create);

        /// <summary>
        /// List of project items associated with this issue.
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
        /// The HTTP path for this issue
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the state of the issue.
        /// </summary>
        public IssueState State { get; }

        /// <summary>
        /// Identifies the reason for the issue state.
        /// </summary>
        public IssueStateReason? StateReason { get; }

        /// <summary>
        /// A list of events, comments, commits, etc. associated with the issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        public IssueTimelineConnection Timeline(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<DateTimeOffset>? since = null) => this.CreateMethodCall(x => x.Timeline(first, after, last, before, since), Octokit.GraphQL.Model.IssueTimelineConnection.Create);

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
        public IssueTimelineItemsConnection TimelineItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<IssueTimelineItemsItemType>>? itemTypes = null, Arg<DateTimeOffset>? since = null, Arg<int>? skip = null) => this.CreateMethodCall(x => x.TimelineItems(first, after, last, before, itemTypes, since, skip), Octokit.GraphQL.Model.IssueTimelineItemsConnection.Create);

        /// <summary>
        /// Identifies the issue title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the issue title rendered to HTML.
        /// </summary>
        public string TitleHTML { get; }

        /// <summary>
        /// A list of issues that track this issue
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public IssueConnection TrackedInIssues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.TrackedInIssues(first, after, last, before), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// A list of issues tracked inside the current issue
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public IssueConnection TrackedIssues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.TrackedIssues(first, after, last, before), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// The number of tracked issues for this issue
        /// </summary>
        /// <param name="states">Limit the count to tracked issues with the specified states.</param>
        public int TrackedIssuesCount(Arg<IEnumerable<TrackedIssueStates?>>? states = null) => default;

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this issue
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
        /// Indicates if the object can be closed by the viewer.
        /// </summary>
        public bool ViewerCanClose { get; }

        /// <summary>
        /// Check if the current viewer can delete this object.
        /// </summary>
        public bool ViewerCanDelete { get; }

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
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; }

        /// <summary>
        /// Identifies the viewer's thread subscription form action.
        /// </summary>
        public ThreadSubscriptionFormAction? ViewerThreadSubscriptionFormAction { get; }

        /// <summary>
        /// Identifies the viewer's thread subscription status.
        /// </summary>
        public ThreadSubscriptionState? ViewerThreadSubscriptionStatus { get; }

        internal static Issue Create(Expression expression)
        {
            return new Issue(expression);
        }
    }
}