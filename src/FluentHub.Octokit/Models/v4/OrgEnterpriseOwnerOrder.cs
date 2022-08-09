namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for an organization's enterprise owner connections.
    /// </summary>
    public class OrgEnterpriseOwnerOrder
    {
        /// <summary>
        /// The field to order enterprise owners by.
        /// </summary>
        public OrgEnterpriseOwnerOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}