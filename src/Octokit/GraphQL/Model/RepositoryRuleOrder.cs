namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for repository rules.
    /// </summary>
    public class RepositoryRuleOrder
    {
        /// <summary>
        /// The field to order repository rules by.
        /// </summary>
        public RepositoryRuleOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}