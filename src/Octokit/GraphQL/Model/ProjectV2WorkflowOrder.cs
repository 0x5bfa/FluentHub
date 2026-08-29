namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for project v2 workflows connections
    /// </summary>
    public class ProjectV2WorkflowOrder
    {
        /// <summary>
        /// The field to order the project v2 workflows by.
        /// </summary>
        public ProjectV2WorkflowsOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}