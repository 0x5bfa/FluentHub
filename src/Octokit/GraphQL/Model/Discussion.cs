namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A discussion in a repository.
    /// </summary>
    public class Discussion : QueryableValue<Discussion>
    {
        internal Discussion(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        public LockReason? ActiveLockReason { get; }

        /// <summary>
        /// The comment chosen as this discussion's answer, if any.
        /// </summary>
        public DiscussionComment Answer => this.CreateProperty(x => x.Answer, Octokit.GraphQL.Model.DiscussionComment.Create);

        /// <summary>
        /// The time when a user chose this discussion's answer, if answered.
        /// </summary>
        public DateTimeOffset? AnswerChosenAt { get; }

        /// <summary>
        /// The user who chose this discussion's answer, if answered.
        /// </summary>
        public IActor AnswerChosenBy => this.CreateProperty(x => x.AnswerChosenBy, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        public CommentAuthorAssociation AuthorAssociation { get; }

        /// <summary>
        /// The main text of the discussion post.
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
        /// The category for this discussion.
        /// </summary>
        public DiscussionCategory Category => this.CreateProperty(x => x.Category, Octokit.GraphQL.Model.DiscussionCategory.Create);

        /// <summary>
        /// Indicates if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; }

        /// <summary>
        /// The replies to the discussion.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DiscussionCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octokit.GraphQL.Model.DiscussionCommentConnection.Create);

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
        /// The Node ID of the Discussion object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; }

        /// <summary>
        /// Only return answered/unanswered discussions
        /// </summary>
        public bool? IsAnswered { get; }

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
        /// `true` if the object is locked
        /// </summary>
        public bool Locked { get; }

        /// <summary>
        /// The number identifying this discussion within the repository.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The poll associated with this discussion, if one exists.
        /// </summary>
        public DiscussionPoll Poll => this.CreateProperty(x => x.Poll, Octokit.GraphQL.Model.DiscussionPoll.Create);

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
        /// The path for this discussion.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the reason for the discussion's state.
        /// </summary>
        public DiscussionStateReason? StateReason { get; }

        /// <summary>
        /// The title of this discussion.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// Number of upvotes that this subject has received.
        /// </summary>
        public int UpvoteCount { get; }

        /// <summary>
        /// The URL for this discussion.
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
        /// Whether or not the current user can add or remove an upvote on this subject.
        /// </summary>
        public bool ViewerCanUpvote { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        /// <summary>
        /// Whether or not the current user has already upvoted this subject.
        /// </summary>
        public bool ViewerHasUpvoted { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; }

        internal static Discussion Create(Expression expression)
        {
            return new Discussion(expression);
        }
    }
}