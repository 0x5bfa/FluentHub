namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for security advisory connections
    /// </summary>
    public class SecurityAdvisoryOrder
    {
        /// <summary>
        /// The field to order security advisories by.
        /// </summary>
        public SecurityAdvisoryOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}