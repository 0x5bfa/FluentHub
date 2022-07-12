namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A level of permission and source for a user's access to a repository.
    /// </summary>
    public class PermissionSource
    {
        /// <summary>
        /// The organization the repository belongs to.
        /// </summary>
        public Organization Organization { get; set; }

        /// <summary>
        /// The level of access this source has granted to the user.
        /// </summary>
        public DefaultRepositoryPermissionField Permission { get; set; }

        /// <summary>
        /// The source of this permission.
        /// </summary>
        public PermissionGranter Source { get; set; }
    }
}