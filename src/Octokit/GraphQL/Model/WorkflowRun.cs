namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A workflow run.
    /// </summary>
    public class WorkflowRun : QueryableValue<WorkflowRun>
    {
        internal WorkflowRun(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The check suite this workflow run belongs to.
        /// </summary>
        public CheckSuite CheckSuite => this.CreateProperty(x => x.CheckSuite, Octokit.GraphQL.Model.CheckSuite.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The log of deployment reviews
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentReviewConnection DeploymentReviews(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.DeploymentReviews(first, after, last, before), Octokit.GraphQL.Model.DeploymentReviewConnection.Create);

        /// <summary>
        /// The event that triggered the workflow run
        /// </summary>
        public string Event { get; }

        /// <summary>
        /// The workflow file
        /// </summary>
        public WorkflowRunFile File => this.CreateProperty(x => x.File, Octokit.GraphQL.Model.WorkflowRunFile.Create);

        /// <summary>
        /// The Node ID of the WorkflowRun object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The pending deployment requests of all check runs in this workflow run
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeploymentRequestConnection PendingDeploymentRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PendingDeploymentRequests(first, after, last, before), Octokit.GraphQL.Model.DeploymentRequestConnection.Create);

        /// <summary>
        /// The HTTP path for this workflow run
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// A number that uniquely identifies this workflow run in its parent workflow.
        /// </summary>
        public int RunNumber { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this workflow run
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The workflow executed in this workflow run.
        /// </summary>
        public Workflow Workflow => this.CreateProperty(x => x.Workflow, Octokit.GraphQL.Model.Workflow.Create);

        internal static WorkflowRun Create(Expression expression)
        {
            return new WorkflowRun(expression);
        }
    }
}