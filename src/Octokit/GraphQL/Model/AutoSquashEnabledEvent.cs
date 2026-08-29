namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'auto_squash_enabled' event on a given pull request.
    /// </summary>
    public class AutoSquashEnabledEvent : QueryableValue<AutoSquashEnabledEvent>
    {
        internal AutoSquashEnabledEvent(Expression expression) : base(expression)
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
        /// The user who enabled auto-merge (squash) for this Pull Request
        /// </summary>
        public User Enabler => this.CreateProperty(x => x.Enabler, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The Node ID of the AutoSquashEnabledEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static AutoSquashEnabledEvent Create(Expression expression)
        {
            return new AutoSquashEnabledEvent(expression);
        }
    }
}