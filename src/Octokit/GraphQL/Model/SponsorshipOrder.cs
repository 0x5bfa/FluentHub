namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for sponsorship connections.
    /// </summary>
    public class SponsorshipOrder
    {
        /// <summary>
        /// The field to order sponsorship by.
        /// </summary>
        public SponsorshipOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}