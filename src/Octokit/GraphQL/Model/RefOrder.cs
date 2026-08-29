namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of git refs can be ordered upon return.
    /// </summary>
    public class RefOrder
    {
        /// <summary>
        /// The field in which to order refs by.
        /// </summary>
        public RefOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order refs by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}