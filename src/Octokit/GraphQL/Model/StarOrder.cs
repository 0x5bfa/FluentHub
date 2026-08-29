namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which star connections can be ordered.
    /// </summary>
    public class StarOrder
    {
        /// <summary>
        /// The field in which to order nodes by.
        /// </summary>
        public StarOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order nodes.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}