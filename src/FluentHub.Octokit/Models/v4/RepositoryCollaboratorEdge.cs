namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a user who is a collaborator of a repository.
    /// </summary>
    public class RepositoryCollaboratorEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        public User Node { get; set; }

        /// <summary>
        /// The permission the user has on the repository.
        /// </summary>
        public RepositoryPermission Permission { get; set; }

        /// <summary>
        /// A list of sources for the user's access to the repository.
        /// </summary>
        public List<PermissionSource> PermissionSources { get; set; }
    }
}