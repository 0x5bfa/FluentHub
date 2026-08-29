namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of packages can be ordered upon return.
    /// </summary>
    public class PackageOrder
    {
        /// <summary>
        /// The field in which to order packages by.
        /// </summary>
        public PackageOrderField? Field { get; set; }

        /// <summary>
        /// The direction in which to order packages by the specified field.
        /// </summary>
        public OrderDirection? Direction { get; set; }
    }
}