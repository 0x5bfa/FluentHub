namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'base_ref_changed' event on a given issue or pull request.
    /// </summary>
    public class BaseRefChangedEvent : QueryableValue<BaseRefChangedEvent>
    {
        internal BaseRefChangedEvent(Expression expression) : base(expression)
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
        /// Identifies the name of the base ref for the pull request after it was changed.
        /// </summary>
        public string CurrentRefName { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the BaseRefChangedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the name of the base ref for the pull request before it was changed.
        /// </summary>
        public string PreviousRefName { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static BaseRefChangedEvent Create(Expression expression)
        {
            return new BaseRefChangedEvent(expression);
        }
    }
}