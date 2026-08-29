namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for commit contribution connections.
    /// </summary>
    public class CommitContributionOrder
    {
        /// <summary>
        /// The field by which to order commit contributions.
        /// </summary>
        public CommitContributionOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}