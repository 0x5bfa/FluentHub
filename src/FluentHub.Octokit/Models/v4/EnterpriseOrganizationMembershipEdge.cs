namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// An enterprise organization that a user is a member of.
    /// </summary>
    public class EnterpriseOrganizationMembershipEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Organization Node { get; set; }

        /// <summary>
        /// The role of the user in the enterprise membership.
        /// </summary>
        public EnterpriseUserAccountMembershipRole Role { get; set; }
    }
}