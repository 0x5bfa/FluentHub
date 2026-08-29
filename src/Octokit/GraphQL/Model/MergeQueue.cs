namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The queue of pull request entries to be merged into a protected branch in a repository.
    /// </summary>
    public class MergeQueue : QueryableValue<MergeQueue>
    {
        internal MergeQueue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The configuration for this merge queue
        /// </summary>
        public MergeQueueConfiguration Configuration => this.CreateProperty(x => x.Configuration, Octokit.GraphQL.Model.MergeQueueConfiguration.Create);

        /// <summary>
        /// The entries in the queue
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public MergeQueueEntryConnection Entries(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Entries(first, after, last, before), Octokit.GraphQL.Model.MergeQueueEntryConnection.Create);

        /// <summary>
        /// The Node ID of the MergeQueue object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The estimated time in seconds until a newly added entry would be merged
        /// </summary>
        public int? NextEntryEstimatedTimeToMerge { get; }

        /// <summary>
        /// The repository this merge queue belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this merge queue
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this merge queue
        /// </summary>
        public string Url { get; }

        internal static MergeQueue Create(Expression expression)
        {
            return new MergeQueue(expression);
        }
    }
}