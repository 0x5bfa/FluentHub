namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A deployment review.
    /// </summary>
    public class DeploymentReview : QueryableValue<DeploymentReview>
    {
        internal DeploymentReview(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The comment the user left.
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The environments approved or rejected
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public EnvironmentConnection Environments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Environments(first, after, last, before), Octokit.GraphQL.Model.EnvironmentConnection.Create);

        /// <summary>
        /// The Node ID of the DeploymentReview object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The decision of the user.
        /// </summary>
        public DeploymentReviewState State { get; }

        /// <summary>
        /// The user that reviewed the deployment.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static DeploymentReview Create(Expression expression)
        {
            return new DeploymentReview(expression);
        }
    }
}