namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// A subset of repository info.
    /// </summary>
    public interface IRepositoryInfo
    {        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The description of the repository.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        string DescriptionHTML { get; set; }

        /// <summary>
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        int ForkCount { get; set; }

        /// <summary>
        /// Indicates if the repository has issues feature enabled.
        /// </summary>
        bool HasIssuesEnabled { get; set; }

        /// <summary>
        /// Indicates if the repository has the Projects feature enabled.
        /// </summary>
        bool HasProjectsEnabled { get; set; }

        /// <summary>
        /// Indicates if the repository has wiki feature enabled.
        /// </summary>
        bool HasWikiEnabled { get; set; }

        /// <summary>
        /// The repository's URL.
        /// </summary>
        string HomepageUrl { get; set; }

        /// <summary>
        /// Indicates if the repository is unmaintained.
        /// </summary>
        bool IsArchived { get; set; }

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        bool IsFork { get; set; }

        /// <summary>
        /// Indicates if a repository is either owned by an organization, or is a private fork of an organization repository.
        /// </summary>
        bool IsInOrganization { get; set; }

        /// <summary>
        /// Indicates if the repository has been locked or not.
        /// </summary>
        bool IsLocked { get; set; }

        /// <summary>
        /// Identifies if the repository is a mirror.
        /// </summary>
        bool IsMirror { get; set; }

        /// <summary>
        /// Identifies if the repository is private or internal.
        /// </summary>
        bool IsPrivate { get; set; }

        /// <summary>
        /// Identifies if the repository is a template that can be used to generate new repositories.
        /// </summary>
        bool IsTemplate { get; set; }

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        License LicenseInfo { get; set; }

        /// <summary>
        /// The reason the repository has been locked.
        /// </summary>
        RepositoryLockReason? LockReason { get; set; }

        /// <summary>
        /// The repository's original mirror URL.
        /// </summary>
        string MirrorUrl { get; set; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        string NameWithOwner { get; set; }

        /// <summary>
        /// The image used to represent this repository in Open Graph data.
        /// </summary>
        string OpenGraphImageUrl { get; set; }

        /// <summary>
        /// The User owner of the repository.
        /// </summary>
        IRepositoryOwner Owner { get; set; }

        /// <summary>
        /// Identifies when the repository was last pushed to.
        /// </summary>
        DateTimeOffset? PushedAt { get; set; }

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        string ResourcePath { get; set; }

        /// <summary>
        /// A description of the repository, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        string ShortDescriptionHTML { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Whether this repository has a custom image to use with Open Graph as opposed to being represented by the owner's avatar.
        /// </summary>
        bool UsesCustomOpenGraphImage { get; set; }

        /// <summary>
        /// Indicates the repository's visibility level.
        /// </summary>
        RepositoryVisibility Visibility { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class RepositoryInfo : IRepositoryInfo
    {
        public DateTimeOffset CreatedAt { get; set; }

        public string Description { get; set; }

        public string DescriptionHTML { get; set; }

        public int ForkCount { get; set; }

        public bool HasIssuesEnabled { get; set; }

        public bool HasProjectsEnabled { get; set; }

        public bool HasWikiEnabled { get; set; }

        public string HomepageUrl { get; set; }

        public bool IsArchived { get; set; }

        public bool IsFork { get; set; }

        public bool IsInOrganization { get; set; }

        public bool IsLocked { get; set; }

        public bool IsMirror { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsTemplate { get; set; }

        public License LicenseInfo { get; set; }

        public RepositoryLockReason? LockReason { get; set; }

        public string MirrorUrl { get; set; }

        public string Name { get; set; }

        public string NameWithOwner { get; set; }

        public string OpenGraphImageUrl { get; set; }

        public IRepositoryOwner Owner { get; set; }

        public DateTimeOffset? PushedAt { get; set; }

        public string ResourcePath { get; set; }

        public string ShortDescriptionHTML { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string Url { get; set; }

        public bool UsesCustomOpenGraphImage { get; set; }

        public RepositoryVisibility Visibility { get; set; }
    }
}