namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for repository connections
    /// </summary>
    public class RepositoryOrder
    {
        /// <summary>
        /// The field to order repositories by.
        /// </summary>
        public RepositoryOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}