namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for team member connections
    /// </summary>
    public class TeamMemberOrder
    {
        /// <summary>
        /// The field to order team members by.
        /// </summary>
        public TeamMemberOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}