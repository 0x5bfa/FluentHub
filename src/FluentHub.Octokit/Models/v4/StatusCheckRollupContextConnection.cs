namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The connection type for StatusCheckRollupContext.
    /// </summary>
    public class StatusCheckRollupContextConnection
    {
        /// <summary>
        /// The number of check runs in this rollup.
        /// </summary>
        public int CheckRunCount { get; set; }

        /// <summary>
        /// Counts of check runs by state.
        /// </summary>
        public List<CheckRunStateCount> CheckRunCountsByState { get; set; }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public List<StatusCheckRollupContextEdge> Edges { get; set; }

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public List<StatusCheckRollupContext> Nodes { get; set; }

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// The number of status contexts in this rollup.
        /// </summary>
        public int StatusContextCount { get; set; }

        /// <summary>
        /// Counts of status contexts by state.
        /// </summary>
        public List<StatusContextStateCount> StatusContextCountsByState { get; set; }

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; set; }
    }
}