namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for verifiable domain connections.
    /// </summary>
    public class VerifiableDomainOrder
    {
        /// <summary>
        /// The field to order verifiable domains by.
        /// </summary>
        public VerifiableDomainOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}