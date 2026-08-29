namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for PullRequestTimelineItems.
    /// </summary>
    public class PullRequestTimelineItemsConnection : QueryableValue<PullRequestTimelineItemsConnection>, IPagingConnection<PullRequestTimelineItems>
    {
        internal PullRequestTimelineItemsConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<PullRequestTimelineItemsEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// Identifies the count of items after applying `before` and `after` filters.
        /// </summary>
        public int FilteredCount { get; }

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<PullRequestTimelineItems> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Identifies the count of items after applying `before`/`after` filters and `first`/`last`/`skip` slicing.
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Identifies the date and time when the timeline was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static PullRequestTimelineItemsConnection Create(Expression expression)
        {
            return new PullRequestTimelineItemsConnection(expression);
        }
    }
}