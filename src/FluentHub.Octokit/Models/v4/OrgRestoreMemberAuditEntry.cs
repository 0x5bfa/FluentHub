// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Audit log entry for a org.restore_member event.
	/// </summary>
	public class OrgRestoreMemberAuditEntry
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
		/// The Node ID of the OrgRestoreMemberAuditEntry object
		/// </summary>
		public ID Id { get; set; }

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
		/// The number of custom email routings for the restored member.
		/// </summary>
		public int? RestoredCustomEmailRoutingsCount { get; set; }

		/// <summary>
		/// The number of issue assignments for the restored member.
		/// </summary>
		public int? RestoredIssueAssignmentsCount { get; set; }

		/// <summary>
		/// Restored organization membership objects.
		/// </summary>
		public List<OrgRestoreMemberAuditEntryMembership> RestoredMemberships { get; set; }

		/// <summary>
		/// The number of restored memberships.
		/// </summary>
		public int? RestoredMembershipsCount { get; set; }

		/// <summary>
		/// The number of repositories of the restored member.
		/// </summary>
		public int? RestoredRepositoriesCount { get; set; }

		/// <summary>
		/// The number of starred repositories for the restored member.
		/// </summary>
		public int? RestoredRepositoryStarsCount { get; set; }

		/// <summary>
		/// The number of watched repositories for the restored member.
		/// </summary>
		public int? RestoredRepositoryWatchesCount { get; set; }

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
