namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A request to deploy a workflow run to an environment.
    /// </summary>
    public class DeploymentRequest : QueryableValue<DeploymentRequest>
    {
        internal DeploymentRequest(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Whether or not the current user can approve the deployment
        /// </summary>
        public bool CurrentUserCanApprove { get; }

        /// <summary>
        /// The target environment of the deployment
        /// </summary>
        public Environment Environment => this.CreateProperty(x => x.Environment, Octokit.GraphQL.Model.Environment.Create);

        /// <summary>
        /// The teams or users that can review the deployment
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentReviewerConnection Reviewers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Reviewers(first, after, last, before), Octokit.GraphQL.Model.DeploymentReviewerConnection.Create);

        /// <summary>
        /// The wait timer in minutes configured in the environment
        /// </summary>
        public int WaitTimer { get; }

        /// <summary>
        /// The wait timer in minutes configured in the environment
        /// </summary>
        public DateTimeOffset? WaitTimerStartedAt { get; }

        internal static DeploymentRequest Create(Expression expression)
        {
            return new DeploymentRequest(expression);
        }
    }
}