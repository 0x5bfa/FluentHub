namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A failed invitation to be a member in an enterprise organization.
    /// </summary>
    public class EnterpriseFailedInvitationEdge
    {
        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; set; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public OrganizationInvitation Node { get; set; }
    }
}