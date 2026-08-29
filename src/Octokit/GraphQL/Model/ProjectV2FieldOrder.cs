namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for project v2 field connections
    /// </summary>
    public class ProjectV2FieldOrder
    {
        /// <summary>
        /// The field to order the project v2 fields by.
        /// </summary>
        public ProjectV2FieldOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}