namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which team connections can be ordered.
    /// </summary>
    public class TeamOrder
    {
        /// <summary>
        /// The field in which to order nodes by.
        /// </summary>
        public TeamOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order nodes.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}