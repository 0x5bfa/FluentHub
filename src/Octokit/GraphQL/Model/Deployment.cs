namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents triggered deployment instance.
    /// </summary>
    public class Deployment : QueryableValue<Deployment>
    {
        internal Deployment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the commit sha of the deployment.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies the oid of the deployment commit, even if the commit has been deleted.
        /// </summary>
        public string CommitOid { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the actor who triggered the deployment.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The deployment description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The latest environment to which this deployment was made.
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// The Node ID of the Deployment object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The latest environment to which this deployment was made.
        /// </summary>
        public string LatestEnvironment { get; }

        /// <summary>
        /// The latest status of this deployment.
        /// </summary>
        public DeploymentStatus LatestStatus => this.CreateProperty(x => x.LatestStatus, Octokit.GraphQL.Model.DeploymentStatus.Create);

        /// <summary>
        /// The original environment to which this deployment was made.
        /// </summary>
        public string OriginalEnvironment { get; }

        /// <summary>
        /// Extra information that a deployment system might need.
        /// </summary>
        public string Payload { get; }

        /// <summary>
        /// Identifies the Ref of the deployment, if the deployment was created by ref.
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Identifies the repository associated with the deployment.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The current state of the deployment.
        /// </summary>
        public DeploymentState? State { get; }

        /// <summary>
        /// A list of statuses associated with the deployment.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentStatusConnection Statuses(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Statuses(first, after, last, before), Octokit.GraphQL.Model.DeploymentStatusConnection.Create);

        /// <summary>
        /// The deployment task.
        /// </summary>
        public string Task { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static Deployment Create(Expression expression)
        {
            return new Deployment(expression);
        }
    }
}