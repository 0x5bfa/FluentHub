namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for repository invitation connections.
    /// </summary>
    public class RepositoryInvitationOrder
    {
        /// <summary>
        /// The field to order repository invitations by.
        /// </summary>
        public RepositoryInvitationOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}