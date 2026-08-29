namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A release contains the content for a release.
    /// </summary>
    public class Release : QueryableValue<Release>
    {
        internal Release(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The author of the release
        /// </summary>
        public User Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The description of the release.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The description of this release rendered to HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// The Node ID of the Release object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether or not the release is a draft
        /// </summary>
        public bool IsDraft { get; }

        /// <summary>
        /// Whether or not the release is the latest releast
        /// </summary>
        public bool IsLatest { get; }

        /// <summary>
        /// Whether or not the release is a prerelease
        /// </summary>
        public bool IsPrerelease { get; }

        /// <summary>
        /// A list of users mentioned in the release description
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Mentions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Mentions(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// The title of the release.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Identifies the date and time when the release was created.
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
        /// List of releases assets which are dependent on this release.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="name">A name to filter the assets by.</param>
        public ReleaseAssetConnection ReleaseAssets(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? name = null) => this.CreateMethodCall(x => x.ReleaseAssets(first, after, last, before, name), Octokit.GraphQL.Model.ReleaseAssetConnection.Create);

        /// <summary>
        /// The repository that the release belongs to.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// A description of the release, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        public string ShortDescriptionHTML(Arg<int>? limit = null) => default;

        /// <summary>
        /// The Git tag the release points to
        /// </summary>
        public Ref Tag => this.CreateProperty(x => x.Tag, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// The tag commit for this release.
        /// </summary>
        public Commit TagCommit => this.CreateProperty(x => x.TagCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The name of the release's Git tag
        /// </summary>
        public string TagName { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this issue
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

        internal static Release Create(Expression expression)
        {
            return new Release(expression);
        }
    }
}