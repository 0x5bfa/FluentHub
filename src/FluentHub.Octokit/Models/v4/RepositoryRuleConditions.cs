namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Set of conditions that determine if a ruleset will evaluate
    /// </summary>
    public class RepositoryRuleConditions
    {
        /// <summary>
        /// Configuration for the ref_name condition
        /// </summary>
        public RefNameConditionTarget RefName { get; set; }

        /// <summary>
        /// Configuration for the repository_name condition
        /// </summary>
        public RepositoryNameConditionTarget RepositoryName { get; set; }
    }
}