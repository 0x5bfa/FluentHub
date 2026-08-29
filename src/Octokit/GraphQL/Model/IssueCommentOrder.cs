namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of issue comments can be ordered upon return.
    /// </summary>
    public class IssueCommentOrder
    {
        /// <summary>
        /// The field in which to order issue comments by.
        /// </summary>
        public IssueCommentOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order issue comments by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}