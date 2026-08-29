namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for Enterprise Server user accounts upload connections.
    /// </summary>
    public class EnterpriseServerUserAccountsUploadOrder
    {
        /// <summary>
        /// The field to order user accounts uploads by.
        /// </summary>
        public EnterpriseServerUserAccountsUploadOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}