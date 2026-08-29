namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for Enterprise Server installation connections.
    /// </summary>
    public class EnterpriseServerInstallationOrder
    {
        /// <summary>
        /// The field to order Enterprise Server installations by.
        /// </summary>
        public EnterpriseServerInstallationOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}