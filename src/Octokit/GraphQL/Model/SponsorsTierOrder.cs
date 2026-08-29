namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for Sponsors tiers connections.
    /// </summary>
    public class SponsorsTierOrder
    {
        /// <summary>
        /// The field to order tiers by.
        /// </summary>
        public SponsorsTierOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}