namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a sort by field and direction.
    /// </summary>
    public class SortBy
    {
        /// <summary>
        /// The direction of the sorting. Possible values are ASC and DESC.
        /// </summary>
        public OrderDirection Direction { get; set; }

        /// <summary>
        /// The id of the field by which the column is sorted.
        /// </summary>
        public int Field { get; set; }
    }
}