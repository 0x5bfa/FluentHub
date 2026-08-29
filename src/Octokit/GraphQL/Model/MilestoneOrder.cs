namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for milestone connections.
    /// </summary>
    public class MilestoneOrder
    {
        /// <summary>
        /// The field to order milestones by.
        /// </summary>
        public MilestoneOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}