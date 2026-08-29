namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entries in a MergeQueue
    /// </summary>
    public class MergeQueueEntry : QueryableValue<MergeQueueEntry>
    {
        internal MergeQueueEntry(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The base commit for this entry
        /// </summary>
        public Commit BaseCommit => this.CreateProperty(x => x.BaseCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The date and time this entry was added to the merge queue
        /// </summary>
        public DateTimeOffset EnqueuedAt { get; }

        /// <summary>
        /// The actor that enqueued this entry
        /// </summary>
        public IActor Enqueuer => this.CreateProperty(x => x.Enqueuer, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The estimated time in seconds until this entry will be merged
        /// </summary>
        public int? EstimatedTimeToMerge { get; }

        /// <summary>
        /// The head commit for this entry
        /// </summary>
        public Commit HeadCommit => this.CreateProperty(x => x.HeadCommit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The Node ID of the MergeQueueEntry object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether this pull request should jump the queue
        /// </summary>
        public bool Jump { get; }

        /// <summary>
        /// The merge queue that this entry belongs to
        /// </summary>
        public MergeQueue MergeQueue => this.CreateProperty(x => x.MergeQueue, Octokit.GraphQL.Model.MergeQueue.Create);

        /// <summary>
        /// The position of this entry in the queue
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// The pull request that will be added to a merge group
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Does this pull request need to be deployed on its own
        /// </summary>
        public bool Solo { get; }

        /// <summary>
        /// The state of this entry in the queue
        /// </summary>
        public MergeQueueEntryState State { get; }

        internal static MergeQueueEntry Create(Expression expression)
        {
            return new MergeQueueEntry(expression);
        }
    }
}