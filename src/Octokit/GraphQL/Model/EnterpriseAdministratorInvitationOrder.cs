namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for enterprise administrator invitation connections
    /// </summary>
    public class EnterpriseAdministratorInvitationOrder
    {
        /// <summary>
        /// The field to order enterprise administrator invitations by.
        /// </summary>
        public EnterpriseAdministratorInvitationOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}