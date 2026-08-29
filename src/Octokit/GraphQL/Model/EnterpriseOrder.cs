namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for enterprises.
    /// </summary>
    public class EnterpriseOrder
    {
        /// <summary>
        /// The field to order enterprises by.
        /// </summary>
        public EnterpriseOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}