namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for StatusCheckRollupContext.
    /// </summary>
    public class StatusCheckRollupContextConnection : QueryableValue<StatusCheckRollupContextConnection>, IPagingConnection<StatusCheckRollupContext>
    {
        internal StatusCheckRollupContextConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of check runs in this rollup.
        /// </summary>
        public int CheckRunCount { get; }

        /// <summary>
        /// Counts of check runs by state.
        /// </summary>
        public IQueryableList<CheckRunStateCount> CheckRunCountsByState => this.CreateProperty(x => x.CheckRunCountsByState);

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<StatusCheckRollupContextEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<StatusCheckRollupContext> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// The number of status contexts in this rollup.
        /// </summary>
        public int StatusContextCount { get; }

        /// <summary>
        /// Counts of status contexts by state.
        /// </summary>
        public IQueryableList<StatusContextStateCount> StatusContextCountsByState => this.CreateProperty(x => x.StatusContextCountsByState);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static StatusCheckRollupContextConnection Create(Expression expression)
        {
            return new StatusCheckRollupContextConnection(expression);
        }
    }
}