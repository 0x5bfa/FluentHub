namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for GitHub Sponsors activity connections.
    /// </summary>
    public class SponsorsActivityOrder
    {        /// <summary>
        /// The field to order activity by.
        /// </summary>
        public SponsorsActivityOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}