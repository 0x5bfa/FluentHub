namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An executed workflow file for a workflow run.
    /// </summary>
    public class WorkflowRunFile : QueryableValue<WorkflowRunFile>
    {
        internal WorkflowRunFile(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the WorkflowRunFile object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The path of the workflow file relative to its repository.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The direct link to the file in the repository which stores the workflow file.
        /// </summary>
        public string RepositoryFileUrl { get; }

        /// <summary>
        /// The repository name and owner which stores the workflow file.
        /// </summary>
        public string RepositoryName { get; }

        /// <summary>
        /// The HTTP path for this workflow run file
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The parent workflow run execution for this file.
        /// </summary>
        public WorkflowRun Run => this.CreateProperty(x => x.Run, Octokit.GraphQL.Model.WorkflowRun.Create);

        /// <summary>
        /// The HTTP URL for this workflow run file
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// If the viewer has permissions to push to the repository which stores the workflow.
        /// </summary>
        public bool ViewerCanPushRepository { get; }

        /// <summary>
        /// If the viewer has permissions to read the repository which stores the workflow.
        /// </summary>
        public bool ViewerCanReadRepository { get; }

        internal static WorkflowRunFile Create(Expression expression)
        {
            return new WorkflowRunFile(expression);
        }
    }
}