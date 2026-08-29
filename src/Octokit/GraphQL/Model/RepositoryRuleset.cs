namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository ruleset.
    /// </summary>
    public class RepositoryRuleset : QueryableValue<RepositoryRuleset>
    {
        internal RepositoryRuleset(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actors that can bypass this ruleset
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RepositoryRulesetBypassActorConnection BypassActors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.BypassActors(first, after, last, before), Octokit.GraphQL.Model.RepositoryRulesetBypassActorConnection.Create);

        /// <summary>
        /// The set of conditions that must evaluate to true for this ruleset to apply
        /// </summary>
        public RepositoryRuleConditions Conditions => this.CreateProperty(x => x.Conditions, Octokit.GraphQL.Model.RepositoryRuleConditions.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The enforcement level of this ruleset
        /// </summary>
        public RuleEnforcement Enforcement { get; }

        /// <summary>
        /// The Node ID of the RepositoryRuleset object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Name of the ruleset.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// List of rules.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="type">The type of rule.</param>
        public RepositoryRuleConnection Rules(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryRuleType>? type = null) => this.CreateMethodCall(x => x.Rules(first, after, last, before, type), Octokit.GraphQL.Model.RepositoryRuleConnection.Create);

        /// <summary>
        /// Source of ruleset.
        /// </summary>
        public RuleSource Source => this.CreateProperty(x => x.Source, Octokit.GraphQL.Model.RuleSource.Create);

        /// <summary>
        /// Target of the ruleset.
        /// </summary>
        public RepositoryRulesetTarget? Target { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static RepositoryRuleset Create(Expression expression)
        {
            return new RepositoryRuleset(expression);
        }
    }
}