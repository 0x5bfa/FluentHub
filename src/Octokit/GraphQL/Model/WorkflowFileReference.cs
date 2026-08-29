namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A workflow that must run for this rule to pass
    /// </summary>
    public class WorkflowFileReference : QueryableValue<WorkflowFileReference>
    {
        internal WorkflowFileReference(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The path to the workflow file
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The ref (branch or tag) of the workflow file to use
        /// </summary>
        public string Ref { get; }

        /// <summary>
        /// The ID of the repository where the workflow is defined
        /// </summary>
        public int RepositoryId { get; }

        /// <summary>
        /// The commit SHA of the workflow file to use
        /// </summary>
        public string Sha { get; }

        internal static WorkflowFileReference Create(Expression expression)
        {
            return new WorkflowFileReference(expression);
        }
    }
}