namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of reactions can be ordered upon return.
    /// </summary>
    public class ReactionOrder
    {
        /// <summary>
        /// The field in which to order reactions by.
        /// </summary>
        public ReactionOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order reactions by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}