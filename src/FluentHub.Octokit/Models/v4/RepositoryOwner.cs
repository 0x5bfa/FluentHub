namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an owner of a Repository.
    /// </summary>
    public interface IRepositoryOwner
    {
        /// <summary>
        /// A URL pointing to the owner's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        string AvatarUrl { get; set; }

        ID Id { get; set; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// A list of repositories that the user owns.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        RepositoryConnection Repositories { get; set; }

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        /// <param name="followRenames">Follow repository renames. If disabled, a repository referenced by its old name will return an error.</param>
        Repository Repository { get; set; }

        /// <summary>
        /// The HTTP URL for the owner.
        /// </summary>
        string ResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the owner.
        /// </summary>
        string Url { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class RepositoryOwner : IRepositoryOwner
    {
        public string AvatarUrl { get; set; }

        public ID Id { get; set; }

        public string Login { get; set; }

        public RepositoryConnection Repositories { get; set; }

        public Repository Repository { get; set; }

        public string ResourcePath { get; set; }

        public string Url { get; set; }
    }
}