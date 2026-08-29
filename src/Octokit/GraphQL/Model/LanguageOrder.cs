namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for language connections.
    /// </summary>
    public class LanguageOrder
    {
        /// <summary>
        /// The field to order languages by.
        /// </summary>
        public LanguageOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}