namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for Enterprise Server user account email connections.
    /// </summary>
    public class EnterpriseServerUserAccountEmailOrder
    {
        /// <summary>
        /// The field to order emails by.
        /// </summary>
        public EnterpriseServerUserAccountEmailOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}