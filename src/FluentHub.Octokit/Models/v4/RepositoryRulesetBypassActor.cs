namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A team or app that has the ability to bypass a rules defined on a ruleset
    /// </summary>
    public class RepositoryRulesetBypassActor
    {
        /// <summary>
        /// The actor that can bypass rules.
        /// </summary>
        public BypassActor Actor { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the ruleset associated with the allowed actor
        /// </summary>
        public RepositoryRuleset RepositoryRuleset { get; set; }
    }
}