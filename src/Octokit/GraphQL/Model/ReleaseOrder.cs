namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of releases can be ordered upon return.
    /// </summary>
    public class ReleaseOrder
    {
        /// <summary>
        /// The field in which to order releases by.
        /// </summary>
        public ReleaseOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order releases by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}