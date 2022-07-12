namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a user within an organization.
    /// </summary>
    public class OrganizationMemberEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// Whether the organization member has two factor enabled or not. Returns null if information is not available to viewer.
        /// </summary>
        public bool? HasTwoFactorEnabled { get; set; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public User Node { get; set; }

        /// <summary>
        /// The role this user has in the organization.
        /// </summary>
        public OrganizationMemberRole? Role { get; set; }
    }
}