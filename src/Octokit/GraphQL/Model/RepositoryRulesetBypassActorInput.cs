namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies the attributes for a new or updated ruleset bypass actor. Only one of `actor_id`, `repository_role_database_id`, or `organization_admin` should be specified.
    /// </summary>
    public class RepositoryRulesetBypassActorInput
    {
        /// <summary>
        /// For Team and Integration bypasses, the Team or Integration ID
        /// </summary>
        public ID? ActorId { get; set; }

        /// <summary>
        /// For role bypasses, the role database ID
        /// </summary>
        public int? RepositoryRoleDatabaseId { get; set; }

        /// <summary>
        /// For organization owner bypasses, true
        /// </summary>
        public bool? OrganizationAdmin { get; set; }

        /// <summary>
        /// The bypass mode for this actor.
        /// </summary>
        public RepositoryRulesetBypassActorBypassMode BypassMode { get; set; }
    }
}