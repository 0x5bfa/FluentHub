namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for connections to get sponsor entities for GitHub Sponsors.
    /// </summary>
    public class SponsorOrder
    {
        /// <summary>
        /// The field to order sponsor entities by.
        /// </summary>
        public SponsorOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}