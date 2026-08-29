namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'added_to_merge_queue' event on a given pull request.
    /// </summary>
    public class AddedToMergeQueueEvent : QueryableValue<AddedToMergeQueueEvent>
    {
        internal AddedToMergeQueueEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The user who added this Pull Request to the merge queue
        /// </summary>
        public User Enqueuer => this.CreateProperty(x => x.Enqueuer, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The Node ID of the AddedToMergeQueueEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The merge queue where this pull request was added to.
        /// </summary>
        public MergeQueue MergeQueue => this.CreateProperty(x => x.MergeQueue, Octokit.GraphQL.Model.MergeQueue.Create);

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static AddedToMergeQueueEvent Create(Expression expression)
        {
            return new AddedToMergeQueueEvent(expression);
        }
    }
}