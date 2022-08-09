namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for Enterprise Server user account connections.
    /// </summary>
    public class EnterpriseServerUserAccountOrder
    {
        /// <summary>
        /// The field to order user accounts by.
        /// </summary>
        public EnterpriseServerUserAccountOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}