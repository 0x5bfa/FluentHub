namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for discussion poll option connections.
    /// </summary>
    public class DiscussionPollOptionOrder
    {
        /// <summary>
        /// The field to order poll options by.
        /// </summary>
        public DiscussionPollOptionOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}