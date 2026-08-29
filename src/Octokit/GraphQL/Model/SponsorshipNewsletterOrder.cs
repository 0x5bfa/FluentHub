namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for sponsorship newsletter connections.
    /// </summary>
    public class SponsorshipNewsletterOrder
    {
        /// <summary>
        /// The field to order sponsorship newsletters by.
        /// </summary>
        public SponsorshipNewsletterOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}