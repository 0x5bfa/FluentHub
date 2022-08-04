namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Gist.
    /// </summary>
    public class Gist
    {
        /// <summary>
        /// A list of comments associated with the gist
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public GistCommentConnection Comments { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        /// <summary>
        /// The gist description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The files in this gist.
        /// </summary>
        /// <param name="limit">The maximum number of files to return.</param>
        /// <param name="oid">The oid of the files to return</param>
        public List<GistFile> Files { get; set; }

        /// <summary>
        /// A list of forks associated with the gist
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for gists returned from the connection</param>
        public GistConnection Forks { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies if the gist is a fork.
        /// </summary>
        public bool IsFork { get; set; }

        /// <summary>
        /// Whether the gist is public or not.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// The gist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The gist owner.
        /// </summary>
        public IRepositoryOwner Owner { get; set; }

        /// <summary>
        /// Identifies when the gist was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies when the gist was last pushed to."
        /// <summary>
        public string PushedAtHumanized { get; set; }

        /// <summary>
        /// The HTML path to this resource.
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        public int StargazerCount { get; set; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }

        /// <summary>
        /// The HTTP URL for this Gist.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; set; }
    }
}