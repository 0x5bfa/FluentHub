namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A Dependabot Update for a dependency in a repository
    /// </summary>
    public class DependabotUpdate
    {
        /// <summary>
        /// The error from a dependency update
        /// </summary>
        public DependabotUpdateError Error { get; set; }

        /// <summary>
        /// The associated pull request
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository { get; set; }
    }
}