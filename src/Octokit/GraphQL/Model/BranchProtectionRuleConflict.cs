namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A conflict between two branch protection rules.
    /// </summary>
    public class BranchProtectionRuleConflict : QueryableValue<BranchProtectionRuleConflict>
    {
        internal BranchProtectionRuleConflict(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the branch protection rule.
        /// </summary>
        public BranchProtectionRule BranchProtectionRule => this.CreateProperty(x => x.BranchProtectionRule, Octokit.GraphQL.Model.BranchProtectionRule.Create);

        /// <summary>
        /// Identifies the conflicting branch protection rule.
        /// </summary>
        public BranchProtectionRule ConflictingBranchProtectionRule => this.CreateProperty(x => x.ConflictingBranchProtectionRule, Octokit.GraphQL.Model.BranchProtectionRule.Create);

        /// <summary>
        /// Identifies the branch ref that has conflicting rules
        /// </summary>
        public Ref Ref => this.CreateProperty(x => x.Ref, Octokit.GraphQL.Model.Ref.Create);

        internal static BranchProtectionRuleConflict Create(Expression expression)
        {
            return new BranchProtectionRuleConflict(expression);
        }
    }
}