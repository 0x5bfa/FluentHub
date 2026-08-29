namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for user status connections.
    /// </summary>
    public class UserStatusOrder
    {
        /// <summary>
        /// The field to order user statuses by.
        /// </summary>
        public UserStatusOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}