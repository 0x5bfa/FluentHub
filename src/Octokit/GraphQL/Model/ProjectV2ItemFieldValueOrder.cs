namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for project v2 item field value connections
    /// </summary>
    public class ProjectV2ItemFieldValueOrder
    {
        /// <summary>
        /// The field to order the project v2 item field values by.
        /// </summary>
        public ProjectV2ItemFieldValueOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}