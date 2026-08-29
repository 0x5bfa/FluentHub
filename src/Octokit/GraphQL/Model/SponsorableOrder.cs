namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for connections to get sponsorable entities for GitHub Sponsors.
    /// </summary>
    public class SponsorableOrder
    {
        /// <summary>
        /// The field to order sponsorable entities by.
        /// </summary>
        public SponsorableOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}