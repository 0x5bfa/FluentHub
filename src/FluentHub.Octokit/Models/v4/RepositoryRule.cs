namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository rule.
    /// </summary>
    public class RepositoryRule
    {
        public ID Id { get; set; }

        /// <summary>
        /// The parameters for this rule.
        /// </summary>
        public RuleParameters Parameters { get; set; }

        /// <summary>
        /// The type of rule.
        /// </summary>
        public RepositoryRuleType Type { get; set; }
    }
}