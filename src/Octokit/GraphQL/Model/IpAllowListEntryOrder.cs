namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for IP allow list entry connections.
    /// </summary>
    public class IpAllowListEntryOrder
    {
        /// <summary>
        /// The field to order IP allow list entries by.
        /// </summary>
        public IpAllowListEntryOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}