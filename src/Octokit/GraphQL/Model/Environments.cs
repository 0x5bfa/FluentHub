namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for environments
    /// </summary>
    public class Environments
    {
        /// <summary>
        /// The field to order environments by.
        /// </summary>
        public EnvironmentOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order environments by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}