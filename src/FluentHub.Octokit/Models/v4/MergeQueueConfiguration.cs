namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Configuration for a MergeQueue
    /// </summary>
    public class MergeQueueConfiguration
    {
        /// <summary>
        /// The amount of time in minutes to wait for a check response before considering it a failure.
        /// </summary>
        public int? CheckResponseTimeout { get; set; }

        /// <summary>
        /// The maximum number of entries to build at once.
        /// </summary>
        public int? MaximumEntriesToBuild { get; set; }

        /// <summary>
        /// The maximum number of entries to merge at once.
        /// </summary>
        public int? MaximumEntriesToMerge { get; set; }

        /// <summary>
        /// The merge method to use for this queue.
        /// </summary>
        public PullRequestMergeMethod? MergeMethod { get; set; }

        /// <summary>
        /// The strategy to use when merging entries.
        /// </summary>
        public MergeQueueMergingStrategy? MergingStrategy { get; set; }

        /// <summary>
        /// The minimum number of entries required to merge at once.
        /// </summary>
        public int? MinimumEntriesToMerge { get; set; }

        /// <summary>
        /// The amount of time in minutes to wait before ignoring the minumum number of entries in the queue requirement and merging a collection of entries
        /// </summary>
        public int? MinimumEntriesToMergeWaitTime { get; set; }
    }
}