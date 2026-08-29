namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'deployed' event on a given pull request.
    /// </summary>
    public class DeployedEvent : QueryableValue<DeployedEvent>
    {
        internal DeployedEvent(Expression expression) : base(expression)
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
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The deployment associated with the 'deployed' event.
        /// </summary>
        public Deployment Deployment => this.CreateProperty(x => x.Deployment, Octokit.GraphQL.Model.Deployment.Create);

        /// <summary>
        /// The Node ID of the DeployedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The ref associated with the 'deployed' event.
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octokit.GraphQL.Model.Ref.Create);

        internal static DeployedEvent Create(Expression expression)
        {
            return new DeployedEvent(expression);
        }
    }
}