namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which team discussion connections can be ordered.
    /// </summary>
    public class TeamDiscussionOrder
    {
        /// <summary>
        /// The field by which to order nodes.
        /// </summary>
        public TeamDiscussionOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order nodes.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}