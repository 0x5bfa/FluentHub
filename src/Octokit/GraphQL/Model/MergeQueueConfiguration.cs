namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Configuration for a MergeQueue
    /// </summary>
    public class MergeQueueConfiguration : QueryableValue<MergeQueueConfiguration>
    {
        internal MergeQueueConfiguration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The amount of time in minutes to wait for a check response before considering it a failure.
        /// </summary>
        public int? CheckResponseTimeout { get; }

        /// <summary>
        /// The maximum number of entries to build at once.
        /// </summary>
        public int? MaximumEntriesToBuild { get; }

        /// <summary>
        /// The maximum number of entries to merge at once.
        /// </summary>
        public int? MaximumEntriesToMerge { get; }

        /// <summary>
        /// The merge method to use for this queue.
        /// </summary>
        public PullRequestMergeMethod? MergeMethod { get; }

        /// <summary>
        /// The strategy to use when merging entries.
        /// </summary>
        public MergeQueueMergingStrategy? MergingStrategy { get; }

        /// <summary>
        /// The minimum number of entries required to merge at once.
        /// </summary>
        public int? MinimumEntriesToMerge { get; }

        /// <summary>
        /// The amount of time in minutes to wait before ignoring the minumum number of entries in the queue requirement and merging a collection of entries
        /// </summary>
        public int? MinimumEntriesToMergeWaitTime { get; }

        internal static MergeQueueConfiguration Create(Expression expression)
        {
            return new MergeQueueConfiguration(expression);
        }
    }
}