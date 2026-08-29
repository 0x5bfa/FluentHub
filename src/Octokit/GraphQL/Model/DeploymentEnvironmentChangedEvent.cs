namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'deployment_environment_changed' event on a given pull request.
    /// </summary>
    public class DeploymentEnvironmentChangedEvent : QueryableValue<DeploymentEnvironmentChangedEvent>
    {
        internal DeploymentEnvironmentChangedEvent(Expression expression) : base(expression)
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
        /// The deployment status that updated the deployment environment.
        /// </summary>
        public DeploymentStatus DeploymentStatus => this.CreateProperty(x => x.DeploymentStatus, Octokit.GraphQL.Model.DeploymentStatus.Create);

        /// <summary>
        /// The Node ID of the DeploymentEnvironmentChangedEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static DeploymentEnvironmentChangedEvent Create(Expression expression)
        {
            return new DeploymentEnvironmentChangedEvent(expression);
        }
    }
}