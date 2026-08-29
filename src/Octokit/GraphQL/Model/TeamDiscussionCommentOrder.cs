namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which team discussion comment connections can be ordered.
    /// </summary>
    public class TeamDiscussionCommentOrder
    {
        /// <summary>
        /// The field by which to order nodes.
        /// </summary>
        public TeamDiscussionCommentOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order nodes.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}