namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of projects can be ordered upon return.
    /// </summary>
    public class ProjectOrder
    {
        /// <summary>
        /// The field in which to order projects by.
        /// </summary>
        public ProjectOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order projects by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}