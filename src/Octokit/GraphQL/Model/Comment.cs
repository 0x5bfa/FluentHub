namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a comment.
    /// </summary>
    [GraphQLIdentifier("Comment")]
    public interface IComment : IQueryableValue<IComment>, IQueryableInterface
    {
        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        IActor Author { get; }

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        CommentAuthorAssociation AuthorAssociation { get; }

        /// <summary>
        /// The body as Markdown.
        /// </summary>
        string Body { get; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        string BodyHTML { get; }

        /// <summary>
        /// The body rendered to text.
        /// </summary>
        string BodyText { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        bool CreatedViaEmail { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        IActor Editor { get; }

        /// <summary>
        /// The Node ID of the Comment object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        bool IncludesCreatedEdit { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        DateTimeOffset? PublishedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// A list of edits to this content.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        UserContentEditConnection UserContentEdits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null);

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        bool ViewerDidAuthor { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Comment")]
    internal class StubIComment : QueryableValue<StubIComment>, IComment
    {
        internal StubIComment(Expression expression) : base(expression)
        {
        }

        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        public CommentAuthorAssociation AuthorAssociation { get; }

        public string Body { get; }

        public string BodyHTML { get; }

        public string BodyText { get; }

        public DateTimeOffset CreatedAt { get; }

        public bool CreatedViaEmail { get; }

        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        public ID Id { get; }

        public bool IncludesCreatedEdit { get; }

        public DateTimeOffset? LastEditedAt { get; }

        public DateTimeOffset? PublishedAt { get; }

        public DateTimeOffset UpdatedAt { get; }

        public UserContentEditConnection UserContentEdits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.UserContentEdits(first, after, last, before), Octokit.GraphQL.Model.UserContentEditConnection.Create);

        public bool ViewerDidAuthor { get; }

        internal static StubIComment Create(Expression expression)
        {
            return new StubIComment(expression);
        }
    }
}