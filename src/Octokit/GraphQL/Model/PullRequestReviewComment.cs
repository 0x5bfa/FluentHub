namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A review comment associated with a given repository pull request.
    /// </summary>
    public class PullRequestReviewComment : QueryableValue<PullRequestReviewComment>
    {
        internal PullRequestReviewComment(Expression expression) : base(expression)
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
        /// The comment body of this review comment.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// The comment body of this review comment rendered as plain text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// Identifies the commit associated with the comment.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies when the comment was created.
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
        /// The diff hunk to which the comment applies.
        /// </summary>
        public string DiffHunk { get; }

        /// <summary>
        /// Identifies when the comment was created in a draft state.
        /// </summary>
        public DateTimeOffset DraftedAt { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; }

        /// <summary>
        /// The Node ID of the PullRequestReviewComment object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; }

        /// <summary>
        /// Returns whether or not a comment has been minimized.
        /// </summary>
        public bool IsMinimized { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// The end line number on the file to which the comment applies
        /// </summary>
        public int? Line { get; }

        /// <summary>
        /// Returns why the comment was minimized. One of `abuse`, `off-topic`, `outdated`, `resolved`, `duplicate` and `spam`. Note that the case and formatting of these values differs from the inputs to the `MinimizeComment` mutation.
        /// </summary>
        public string MinimizedReason { get; }

        /// <summary>
        /// Identifies the original commit associated with the comment.
        /// </summary>
        public Commit OriginalCommit => this.CreateProperty(x => x.OriginalCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The end line number on the file to which the comment applied when it was first created
        /// </summary>
        public int? OriginalLine { get; }

        /// <summary>
        /// The original line index in the diff to which the comment applies.
        /// </summary>
        [Obsolete(@"We are phasing out diff-relative positioning for PR comments Removal on 2023-10-01 UTC.")]
        public int OriginalPosition { get; }

        /// <summary>
        /// The start line number on the file to which the comment applied when it was first created
        /// </summary>
        public int? OriginalStartLine { get; }

        /// <summary>
        /// Identifies when the comment body is outdated
        /// </summary>
        public bool Outdated { get; }

        /// <summary>
        /// The path to which the comment applies.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The line index in the diff to which the comment applies.
        /// </summary>
        [Obsolete(@"We are phasing out diff-relative positioning for PR comments Use the `line` and `startLine` fields instead, which are file line numbers instead of diff line numbers Removal on 2023-10-01 UTC.")]
        public int? Position { get; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public DateTimeOffset? PublishedAt { get; }

        /// <summary>
        /// The pull request associated with this review comment.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The pull request review associated with this review comment.
        /// </summary>
        public PullRequestReview PullRequestReview => this.CreateProperty(x => x.PullRequestReview, Octokit.GraphQL.Model.PullRequestReview.Create);

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
        /// The comment this is a reply to.
        /// </summary>
        public PullRequestReviewComment ReplyTo => this.CreateProperty(x => x.ReplyTo, Octokit.GraphQL.Model.PullRequestReviewComment.Create);

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path permalink for this review comment.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The start line number on the file to which the comment applies
        /// </summary>
        public int? StartLine { get; }

        /// <summary>
        /// Identifies the state of the comment.
        /// </summary>
        public PullRequestReviewCommentState State { get; }

        /// <summary>
        /// The level at which the comments in the corresponding thread are targeted, can be a diff line or a file
        /// </summary>
        public PullRequestReviewThreadSubjectType SubjectType { get; }

        /// <summary>
        /// Identifies when the comment was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL permalink for this review comment.
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
        /// Check if the current viewer can minimize this object.
        /// </summary>
        public bool ViewerCanMinimize { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

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

        internal static PullRequestReviewComment Create(Expression expression)
        {
            return new PullRequestReviewComment(expression);
        }
    }
}