namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of workflow runs can be ordered upon return.
    /// </summary>
    public class WorkflowRunOrder
    {
        /// <summary>
        /// The field by which to order workflows.
        /// </summary>
        public WorkflowRunOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order workflow runs by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}