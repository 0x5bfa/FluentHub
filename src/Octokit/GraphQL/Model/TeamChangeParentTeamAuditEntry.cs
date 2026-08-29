namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Audit log entry for a team.change_parent_team event.
    /// </summary>
    public class TeamChangeParentTeamAuditEntry : QueryableValue<TeamChangeParentTeamAuditEntry>
    {
        internal TeamChangeParentTeamAuditEntry(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The action name
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// The user who initiated the action
        /// </summary>
        public AuditEntryActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.AuditEntryActor.Create);

        /// <summary>
        /// The IP address of the actor
        /// </summary>
        public string ActorIp { get; }

        /// <summary>
        /// A readable representation of the actor's location
        /// </summary>
        public ActorLocation ActorLocation => this.CreateProperty(x => x.ActorLocation, Octokit.GraphQL.Model.ActorLocation.Create);

        /// <summary>
        /// The username of the user who initiated the action
        /// </summary>
        public string ActorLogin { get; }

        /// <summary>
        /// The HTTP path for the actor.
        /// </summary>
        public string ActorResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the actor.
        /// </summary>
        public string ActorUrl { get; }

        /// <summary>
        /// The time the action was initiated
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The Node ID of the TeamChangeParentTeamAuditEntry object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether the team was mapped to an LDAP Group.
        /// </summary>
        public bool? IsLdapMapped { get; }

        /// <summary>
        /// The corresponding operation type for the action
        /// </summary>
        public OperationType? OperationType { get; }

        /// <summary>
        /// The Organization associated with the Audit Entry.
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The name of the Organization.
        /// </summary>
        public string OrganizationName { get; }

        /// <summary>
        /// The HTTP path for the organization
        /// </summary>
        public string OrganizationResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the organization
        /// </summary>
        public string OrganizationUrl { get; }

        /// <summary>
        /// The new parent team.
        /// </summary>
        public Team ParentTeam => this.CreateProperty(x => x.ParentTeam, Octokit.GraphQL.Model.Team.Create);

        /// <summary>
        /// The name of the new parent team
        /// </summary>
        public string ParentTeamName { get; }

        /// <summary>
        /// The name of the former parent team
        /// </summary>
        public string ParentTeamNameWas { get; }

        /// <summary>
        /// The HTTP path for the parent team
        /// </summary>
        public string ParentTeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the parent team
        /// </summary>
        public string ParentTeamUrl { get; }

        /// <summary>
        /// The former parent team.
        /// </summary>
        public Team ParentTeamWas => this.CreateProperty(x => x.ParentTeamWas, Octokit.GraphQL.Model.Team.Create);

        /// <summary>
        /// The HTTP path for the previous parent team
        /// </summary>
        public string ParentTeamWasResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the previous parent team
        /// </summary>
        public string ParentTeamWasUrl { get; }

        /// <summary>
        /// The team associated with the action
        /// </summary>
        public Team Team => this.CreateProperty(x => x.Team, Octokit.GraphQL.Model.Team.Create);

        /// <summary>
        /// The name of the team
        /// </summary>
        public string TeamName { get; }

        /// <summary>
        /// The HTTP path for this team
        /// </summary>
        public string TeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this team
        /// </summary>
        public string TeamUrl { get; }

        /// <summary>
        /// The user affected by the action
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// For actions involving two users, the actor is the initiator and the user is the affected user.
        /// </summary>
        public string UserLogin { get; }

        /// <summary>
        /// The HTTP path for the user.
        /// </summary>
        public string UserResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the user.
        /// </summary>
        public string UserUrl { get; }

        internal static TeamChangeParentTeamAuditEntry Create(Expression expression)
        {
            return new TeamChangeParentTeamAuditEntry(expression);
        }
    }
}