namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for project v2 view connections
    /// </summary>
    public class ProjectV2ViewOrder
    {
        /// <summary>
        /// The field to order the project v2 views by.
        /// </summary>
        public ProjectV2ViewOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}