namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for enterprise member connections.
    /// </summary>
    public class EnterpriseMemberOrder
    {
        /// <summary>
        /// The field to order enterprise members by.
        /// </summary>
        public EnterpriseMemberOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}