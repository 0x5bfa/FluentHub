namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class BranchProtectionRuleConflictEdge : QueryableValue<BranchProtectionRuleConflictEdge>
    {
        internal BranchProtectionRuleConflictEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public BranchProtectionRuleConflict Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.BranchProtectionRuleConflict.Create);

        internal static BranchProtectionRuleConflictEdge Create(Expression expression)
        {
            return new BranchProtectionRuleConflictEdge(expression);
        }
    }
}