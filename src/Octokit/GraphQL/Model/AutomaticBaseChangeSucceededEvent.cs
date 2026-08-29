namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'automatic_base_change_succeeded' event on a given pull request.
    /// </summary>
    public class AutomaticBaseChangeSucceededEvent : QueryableValue<AutomaticBaseChangeSucceededEvent>
    {
        internal AutomaticBaseChangeSucceededEvent(Expression expression) : base(expression)
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
        /// The Node ID of the AutomaticBaseChangeSucceededEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The new base for this PR
        /// </summary>
        public string NewBase { get; }

        /// <summary>
        /// The old base for this PR
        /// </summary>
        public string OldBase { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static AutomaticBaseChangeSucceededEvent Create(Expression expression)
        {
            return new AutomaticBaseChangeSucceededEvent(expression);
        }
    }
}