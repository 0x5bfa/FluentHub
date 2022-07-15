namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The connection type for IssueTimelineItems.
    /// </summary>
    public class IssueTimelineItemsConnection
    {
        /// <summary>
        /// A list of edges.
        /// </summary>
        public List<IssueTimelineItemsEdge> Edges { get; set; }

        /// <summary>
        /// Identifies the count of items after applying `before` and `after` filters.
        /// </summary>
        public int FilteredCount { get; set; }

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public List<IssueTimelineItems> Nodes { get; set; }

        /// <summary>
        /// Identifies the count of items after applying `before`/`after` filters and `first`/`last`/`skip` slicing.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Identifies the date and time when the timeline was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}