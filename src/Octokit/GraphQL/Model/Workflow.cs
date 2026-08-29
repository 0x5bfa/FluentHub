namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A workflow contains meta information about an Actions workflow file.
    /// </summary>
    public class Workflow : QueryableValue<Workflow>
    {
        internal Workflow(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the Workflow object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The name of the workflow.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The HTTP path for this workflow
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The runs of the workflow.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the connection</param>
        public WorkflowRunConnection Runs(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<WorkflowRunOrder>? orderBy = null) => this.CreateMethodCall(x => x.Runs(first, after, last, before, orderBy), Octokit.GraphQL.Model.WorkflowRunConnection.Create);

        /// <summary>
        /// The state of the workflow.
        /// </summary>
        public WorkflowState State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this workflow
        /// </summary>
        public string Url { get; }

        internal static Workflow Create(Expression expression)
        {
            return new Workflow(expression);
        }
    }
}