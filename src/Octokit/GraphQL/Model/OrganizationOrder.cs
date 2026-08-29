namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for organization connections.
    /// </summary>
    public class OrganizationOrder
    {
        /// <summary>
        /// The field to order organizations by.
        /// </summary>
        public OrganizationOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}