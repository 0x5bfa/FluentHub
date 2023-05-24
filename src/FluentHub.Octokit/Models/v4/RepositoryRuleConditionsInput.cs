namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies the conditions required for a ruleset to evaluate
    /// </summary>
    public class RepositoryRuleConditionsInput
    {
        /// <summary>
        /// Configuration for the ref_name condition
        /// </summary>
        public RefNameConditionTargetInput RefName { get; set; }

        /// <summary>
        /// Configuration for the repository_name condition
        /// </summary>
        public RepositoryNameConditionTargetInput RepositoryName { get; set; }
    }
}