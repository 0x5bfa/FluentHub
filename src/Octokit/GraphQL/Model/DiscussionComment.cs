namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A comment on a discussion.
    /// </summary>
    public class DiscussionComment : QueryableValue<DiscussionComment>
    {
        internal DiscussionComment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        public CommentAuthorAssociation AuthorAssociation { get; }

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
        /// The time when this replied-to comment was deleted
        /// </summary>
        public DateTimeOffset? DeletedAt { get; }

        /// <summary>
        /// The discussion this comment was created in
        /// </summary>
        public Discussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.Discussion.Create);

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The Node ID of the DiscussionComment object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; }

        /// <summary>
        /// Has this comment been chosen as the answer of its discussion?
        /// </summary>
        public bool IsAnswer { get; }

        /// <summary>
        /// Returns whether or not a comment has been minimized.
        /// </summary>
        public bool IsMinimized { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// Returns why the comment was minimized. One of `abuse`, `off-topic`, `outdated`, `resolved`, `duplicate` and `spam`. Note that the case and formatting of these values differs from the inputs to the `MinimizeComment` mutation.
        /// </summary>
        public string MinimizedReason { get; }

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
        /// The threaded replies to this comment.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DiscussionCommentConnection Replies(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Replies(first, after, last, before), Octokit.GraphQL.Model.DiscussionCommentConnection.Create);

        /// <summary>
        /// The discussion comment this comment is a reply to
        /// </summary>
        public DiscussionComment ReplyTo => this.CreateProperty(x => x.ReplyTo, Octokit.GraphQL.Model.DiscussionComment.Create);

        /// <summary>
        /// The path for this discussion comment.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// Number of upvotes that this subject has received.
        /// </summary>
        public int UpvoteCount { get; }

        /// <summary>
        /// The URL for this discussion comment.
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
        /// Check if the current viewer can delete this object.
        /// </summary>
        public bool ViewerCanDelete { get; }

        /// <summary>
        /// Can the current user mark this comment as an answer?
        /// </summary>
        public bool ViewerCanMarkAsAnswer { get; }

        /// <summary>
        /// Check if the current viewer can minimize this object.
        /// </summary>
        public bool ViewerCanMinimize { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

        /// <summary>
        /// Can the current user unmark this comment as an answer?
        /// </summary>
        public bool ViewerCanUnmarkAsAnswer { get; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; }

        /// <summary>
        /// Whether or not the current user can add or remove an upvote on this subject.
        /// </summary>
        public bool ViewerCanUpvote { get; }

        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        /// <summary>
        /// Whether or not the current user has already upvoted this subject.
        /// </summary>
        public bool ViewerHasUpvoted { get; }

        internal static DiscussionComment Create(Expression expression)
        {
            return new DiscussionComment(expression);
        }
    }
}