namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A Gist.
    /// </summary>
    public class Gist : QueryableValue<Gist>
    {
        internal Gist(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of comments associated with the gist
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public GistCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octokit.GraphQL.Model.GistCommentConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The gist description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The files in this gist.
        /// </summary>
        /// <param name="limit">The maximum number of files to return.</param>
        /// <param name="oid">The oid of the files to return</param>
        public IQueryableList<GistFile> Files(Arg<int>? limit = null, Arg<string>? oid = null) => this.CreateMethodCall(x => x.Files(limit, oid));

        /// <summary>
        /// A list of forks associated with the gist
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for gists returned from the connection</param>
        public GistConnection Forks(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<GistOrder>? orderBy = null) => this.CreateMethodCall(x => x.Forks(first, after, last, before, orderBy), Octokit.GraphQL.Model.GistConnection.Create);

        /// <summary>
        /// The Node ID of the Gist object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies if the gist is a fork.
        /// </summary>
        public bool IsFork { get; }

        /// <summary>
        /// Whether the gist is public or not.
        /// </summary>
        public bool IsPublic { get; }

        /// <summary>
        /// The gist name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The gist owner.
        /// </summary>
        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// Identifies when the gist was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; }

        /// <summary>
        /// The HTML path to this resource.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        public int StargazerCount { get; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null) => this.CreateMethodCall(x => x.Stargazers(first, after, last, before, orderBy), Octokit.GraphQL.Model.StargazerConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this Gist.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; }

        internal static Gist Create(Expression expression)
        {
            return new Gist(expression);
        }
    }
}