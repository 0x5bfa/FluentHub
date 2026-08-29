namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of labels can be ordered upon return.
    /// </summary>
    public class LabelOrder
    {
        /// <summary>
        /// The field in which to order labels by.
        /// </summary>
        public LabelOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order labels by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}