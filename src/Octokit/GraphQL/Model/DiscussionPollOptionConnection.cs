namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for DiscussionPollOption.
    /// </summary>
    public class DiscussionPollOptionConnection : QueryableValue<DiscussionPollOptionConnection>, IPagingConnection<DiscussionPollOption>
    {
        internal DiscussionPollOptionConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<DiscussionPollOptionEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<DiscussionPollOption> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static DiscussionPollOptionConnection Create(Expression expression)
        {
            return new DiscussionPollOptionConnection(expression);
        }
    }
}