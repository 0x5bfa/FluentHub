namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for mannequins.
    /// </summary>
    public class MannequinOrder
    {
        /// <summary>
        /// The field to order mannequins by.
        /// </summary>
        public MannequinOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}