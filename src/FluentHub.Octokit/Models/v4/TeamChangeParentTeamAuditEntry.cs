// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Audit log entry for a team.change_parent_team event.
	/// </summary>
	public class TeamChangeParentTeamAuditEntry
	{
		/// <summary>
		/// The action name
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// The user who initiated the action
		/// </summary>
		public AuditEntryActor Actor { get; set; }

		/// <summary>
		/// The IP address of the actor
		/// </summary>
		public string ActorIp { get; set; }

		/// <summary>
		/// A readable representation of the actor's location
		/// </summary>
		public ActorLocation ActorLocation { get; set; }

		/// <summary>
		/// The username of the user who initiated the action
		/// </summary>
		public string ActorLogin { get; set; }

		/// <summary>
		/// The HTTP path for the actor.
		/// </summary>
		public string ActorResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the actor.
		/// </summary>
		public string ActorUrl { get; set; }

		/// <summary>
		/// The time the action was initiated
		/// </summary>
		public string CreatedAt { get; set; }

		/// <summary>
		/// The Node ID of the TeamChangeParentTeamAuditEntry object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether the team was mapped to an LDAP Group.
		/// </summary>
		public bool? IsLdapMapped { get; set; }

		/// <summary>
		/// The corresponding operation type for the action
		/// </summary>
		public OperationType? OperationType { get; set; }

		/// <summary>
		/// The Organization associated with the Audit Entry.
		/// </summary>
		public Organization Organization { get; set; }

		/// <summary>
		/// The name of the Organization.
		/// </summary>
		public string OrganizationName { get; set; }

		/// <summary>
		/// The HTTP path for the organization
		/// </summary>
		public string OrganizationResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the organization
		/// </summary>
		public string OrganizationUrl { get; set; }

		/// <summary>
		/// The new parent team.
		/// </summary>
		public Team ParentTeam { get; set; }

		/// <summary>
		/// The name of the new parent team
		/// </summary>
		public string ParentTeamName { get; set; }

		/// <summary>
		/// The name of the former parent team
		/// </summary>
		public string ParentTeamNameWas { get; set; }

		/// <summary>
		/// The HTTP path for the parent team
		/// </summary>
		public string ParentTeamResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the parent team
		/// </summary>
		public string ParentTeamUrl { get; set; }

		/// <summary>
		/// The former parent team.
		/// </summary>
		public Team ParentTeamWas { get; set; }

		/// <summary>
		/// The HTTP path for the previous parent team
		/// </summary>
		public string ParentTeamWasResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the previous parent team
		/// </summary>
		public string ParentTeamWasUrl { get; set; }

		/// <summary>
		/// The team associated with the action
		/// </summary>
		public Team Team { get; set; }

		/// <summary>
		/// The name of the team
		/// </summary>
		public string TeamName { get; set; }

		/// <summary>
		/// The HTTP path for this team
		/// </summary>
		public string TeamResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this team
		/// </summary>
		public string TeamUrl { get; set; }

		/// <summary>
		/// The user affected by the action
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// For actions involving two users, the actor is the initiator and the user is the affected user.
		/// </summary>
		public string UserLogin { get; set; }

		/// <summary>
		/// The HTTP path for the user.
		/// </summary>
		public string UserResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the user.
		/// </summary>
		public string UserUrl { get; set; }
	}
}
