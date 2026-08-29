namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A comment on a team discussion.
    /// </summary>
    public class TeamDiscussionComment : QueryableValue<TeamDiscussionComment>
    {
        internal TeamDiscussionComment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Author's association with the comment's team.
        /// </summary>
        [Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
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
        /// The current version of the body content.
        /// </summary>
        [Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
        public string BodyVersion { get; }

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
        /// The discussion this comment is about.
        /// </summary>
        [Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
        public TeamDiscussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.TeamDiscussion.Create);

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The Node ID of the TeamDiscussionComment object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        public bool IncludesCreatedEdit { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// Identifies the comment number.
        /// </summary>
        [Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
        public int Number { get; }

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
        /// The HTTP path for this comment
        /// </summary>
        [Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this comment
        /// </summary>
        [Obsolete(@"The Team Discussions feature is deprecated in favor of Organization Discussions. Follow the guide at https://github.blog/changelog/2023-02-08-sunset-notice-team-discussions/ to find a suitable replacement. Removal on 2024-07-01 UTC.")]
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

        internal static TeamDiscussionComment Create(Expression expression)
        {
            return new TeamDiscussionComment(expression);
        }
    }
}