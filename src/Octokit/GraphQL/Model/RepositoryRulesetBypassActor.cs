namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team or app that has the ability to bypass a rules defined on a ruleset
    /// </summary>
    public class RepositoryRulesetBypassActor : QueryableValue<RepositoryRulesetBypassActor>
    {
        internal RepositoryRulesetBypassActor(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor that can bypass rules.
        /// </summary>
        public BypassActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.BypassActor.Create);

        /// <summary>
        /// The mode for the bypass actor
        /// </summary>
        public RepositoryRulesetBypassActorBypassMode? BypassMode { get; }

        /// <summary>
        /// The Node ID of the RepositoryRulesetBypassActor object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// This actor represents the ability for an organization owner to bypass
        /// </summary>
        public bool OrganizationAdmin { get; }

        /// <summary>
        /// If the actor is a repository role, the repository role's ID that can bypass
        /// </summary>
        public int? RepositoryRoleDatabaseId { get; }

        /// <summary>
        /// If the actor is a repository role, the repository role's name that can bypass
        /// </summary>
        public string RepositoryRoleName { get; }

        /// <summary>
        /// Identifies the ruleset associated with the allowed actor
        /// </summary>
        public RepositoryRuleset RepositoryRuleset => this.CreateProperty(x => x.RepositoryRuleset, Octokit.GraphQL.Model.RepositoryRuleset.Create);

        internal static RepositoryRulesetBypassActor Create(Expression expression)
        {
            return new RepositoryRulesetBypassActor(expression);
        }
    }
}