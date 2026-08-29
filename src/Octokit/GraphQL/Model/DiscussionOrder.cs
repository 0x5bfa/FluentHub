namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of discussions can be ordered upon return.
    /// </summary>
    public class DiscussionOrder
    {
        /// <summary>
        /// The field by which to order discussions.
        /// </summary>
        public DiscussionOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order discussions by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}