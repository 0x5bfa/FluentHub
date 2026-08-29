namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Require all changes made to a targeted branch to pass the specified workflows before they can be merged.
    /// </summary>
    public class WorkflowsParameters : QueryableValue<WorkflowsParameters>
    {
        internal WorkflowsParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Workflows that must pass for this rule to pass.
        /// </summary>
        public IQueryableList<WorkflowFileReference> Workflows => this.CreateProperty(x => x.Workflows);

        internal static WorkflowsParameters Create(Expression expression)
        {
            return new WorkflowsParameters(expression);
        }
    }
}