namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for connections to get sponsor entities and associated USD amounts for GitHub Sponsors.
    /// </summary>
    public class SponsorAndLifetimeValueOrder
    {
        /// <summary>
        /// The field to order results by.
        /// </summary>
        public SponsorAndLifetimeValueOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}