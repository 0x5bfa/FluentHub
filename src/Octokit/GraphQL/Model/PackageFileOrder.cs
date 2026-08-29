namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of package files can be ordered upon return.
    /// </summary>
    public class PackageFileOrder
    {
        /// <summary>
        /// The field in which to order package files by.
        /// </summary>
        public PackageFileOrderField? Field { get; set; }

        /// <summary>
        /// The direction in which to order package files by the specified field.
        /// </summary>
        public OrderDirection? Direction { get; set; }
    }
}