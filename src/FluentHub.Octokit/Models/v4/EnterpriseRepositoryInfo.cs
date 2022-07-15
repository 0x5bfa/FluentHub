namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A subset of repository information queryable from an enterprise.
    /// </summary>
    public class EnterpriseRepositoryInfo
    {
        public ID Id { get; set; }

        /// <summary>
        /// Identifies if the repository is private or internal.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// The repository's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; set; }
    }
}