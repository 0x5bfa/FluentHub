namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for team repository connections
    /// </summary>
    public class TeamRepositoryOrder
    {
        /// <summary>
        /// The field to order repositories by.
        /// </summary>
        public TeamRepositoryOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}