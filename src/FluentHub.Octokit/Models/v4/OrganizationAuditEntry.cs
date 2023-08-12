// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An audit entry in an organization audit log.
	/// </summary>
	public class OrganizationAuditEntry
	{
		/// <summary>
		/// Audit log entry for a members_can_delete_repos.clear event.
		/// </summary>
		public MembersCanDeleteReposClearAuditEntry MembersCanDeleteReposClearAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a members_can_delete_repos.disable event.
		/// </summary>
		public MembersCanDeleteReposDisableAuditEntry MembersCanDeleteReposDisableAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a members_can_delete_repos.enable event.
		/// </summary>
		public MembersCanDeleteReposEnableAuditEntry MembersCanDeleteReposEnableAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a oauth_application.create event.
		/// </summary>
		public OauthApplicationCreateAuditEntry OauthApplicationCreateAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.add_billing_manager
		/// </summary>
		public OrgAddBillingManagerAuditEntry OrgAddBillingManagerAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.add_member
		/// </summary>
		public OrgAddMemberAuditEntry OrgAddMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.block_user
		/// </summary>
		public OrgBlockUserAuditEntry OrgBlockUserAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.config.disable_collaborators_only event.
		/// </summary>
		public OrgConfigDisableCollaboratorsOnlyAuditEntry OrgConfigDisableCollaboratorsOnlyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.config.enable_collaborators_only event.
		/// </summary>
		public OrgConfigEnableCollaboratorsOnlyAuditEntry OrgConfigEnableCollaboratorsOnlyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.create event.
		/// </summary>
		public OrgCreateAuditEntry OrgCreateAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.disable_oauth_app_restrictions event.
		/// </summary>
		public OrgDisableOauthAppRestrictionsAuditEntry OrgDisableOauthAppRestrictionsAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.disable_saml event.
		/// </summary>
		public OrgDisableSamlAuditEntry OrgDisableSamlAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.disable_two_factor_requirement event.
		/// </summary>
		public OrgDisableTwoFactorRequirementAuditEntry OrgDisableTwoFactorRequirementAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.enable_oauth_app_restrictions event.
		/// </summary>
		public OrgEnableOauthAppRestrictionsAuditEntry OrgEnableOauthAppRestrictionsAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.enable_saml event.
		/// </summary>
		public OrgEnableSamlAuditEntry OrgEnableSamlAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.enable_two_factor_requirement event.
		/// </summary>
		public OrgEnableTwoFactorRequirementAuditEntry OrgEnableTwoFactorRequirementAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.invite_member event.
		/// </summary>
		public OrgInviteMemberAuditEntry OrgInviteMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.invite_to_business event.
		/// </summary>
		public OrgInviteToBusinessAuditEntry OrgInviteToBusinessAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.oauth_app_access_approved event.
		/// </summary>
		public OrgOauthAppAccessApprovedAuditEntry OrgOauthAppAccessApprovedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.oauth_app_access_blocked event.
		/// </summary>
		public OrgOauthAppAccessBlockedAuditEntry OrgOauthAppAccessBlockedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.oauth_app_access_denied event.
		/// </summary>
		public OrgOauthAppAccessDeniedAuditEntry OrgOauthAppAccessDeniedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.oauth_app_access_requested event.
		/// </summary>
		public OrgOauthAppAccessRequestedAuditEntry OrgOauthAppAccessRequestedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.oauth_app_access_unblocked event.
		/// </summary>
		public OrgOauthAppAccessUnblockedAuditEntry OrgOauthAppAccessUnblockedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.remove_billing_manager event.
		/// </summary>
		public OrgRemoveBillingManagerAuditEntry OrgRemoveBillingManagerAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.remove_member event.
		/// </summary>
		public OrgRemoveMemberAuditEntry OrgRemoveMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.remove_outside_collaborator event.
		/// </summary>
		public OrgRemoveOutsideCollaboratorAuditEntry OrgRemoveOutsideCollaboratorAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.restore_member event.
		/// </summary>
		public OrgRestoreMemberAuditEntry OrgRestoreMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.unblock_user
		/// </summary>
		public OrgUnblockUserAuditEntry OrgUnblockUserAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.update_default_repository_permission
		/// </summary>
		public OrgUpdateDefaultRepositoryPermissionAuditEntry OrgUpdateDefaultRepositoryPermissionAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.update_member event.
		/// </summary>
		public OrgUpdateMemberAuditEntry OrgUpdateMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.update_member_repository_creation_permission event.
		/// </summary>
		public OrgUpdateMemberRepositoryCreationPermissionAuditEntry OrgUpdateMemberRepositoryCreationPermissionAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a org.update_member_repository_invitation_permission event.
		/// </summary>
		public OrgUpdateMemberRepositoryInvitationPermissionAuditEntry OrgUpdateMemberRepositoryInvitationPermissionAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a private_repository_forking.disable event.
		/// </summary>
		public PrivateRepositoryForkingDisableAuditEntry PrivateRepositoryForkingDisableAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a private_repository_forking.enable event.
		/// </summary>
		public PrivateRepositoryForkingEnableAuditEntry PrivateRepositoryForkingEnableAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.access event.
		/// </summary>
		public RepoAccessAuditEntry RepoAccessAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.add_member event.
		/// </summary>
		public RepoAddMemberAuditEntry RepoAddMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.add_topic event.
		/// </summary>
		public RepoAddTopicAuditEntry RepoAddTopicAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.archived event.
		/// </summary>
		public RepoArchivedAuditEntry RepoArchivedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.change_merge_setting event.
		/// </summary>
		public RepoChangeMergeSettingAuditEntry RepoChangeMergeSettingAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.disable_anonymous_git_access event.
		/// </summary>
		public RepoConfigDisableAnonymousGitAccessAuditEntry RepoConfigDisableAnonymousGitAccessAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.disable_collaborators_only event.
		/// </summary>
		public RepoConfigDisableCollaboratorsOnlyAuditEntry RepoConfigDisableCollaboratorsOnlyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.disable_contributors_only event.
		/// </summary>
		public RepoConfigDisableContributorsOnlyAuditEntry RepoConfigDisableContributorsOnlyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.disable_sockpuppet_disallowed event.
		/// </summary>
		public RepoConfigDisableSockpuppetDisallowedAuditEntry RepoConfigDisableSockpuppetDisallowedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.enable_anonymous_git_access event.
		/// </summary>
		public RepoConfigEnableAnonymousGitAccessAuditEntry RepoConfigEnableAnonymousGitAccessAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.enable_collaborators_only event.
		/// </summary>
		public RepoConfigEnableCollaboratorsOnlyAuditEntry RepoConfigEnableCollaboratorsOnlyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.enable_contributors_only event.
		/// </summary>
		public RepoConfigEnableContributorsOnlyAuditEntry RepoConfigEnableContributorsOnlyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.enable_sockpuppet_disallowed event.
		/// </summary>
		public RepoConfigEnableSockpuppetDisallowedAuditEntry RepoConfigEnableSockpuppetDisallowedAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.lock_anonymous_git_access event.
		/// </summary>
		public RepoConfigLockAnonymousGitAccessAuditEntry RepoConfigLockAnonymousGitAccessAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.config.unlock_anonymous_git_access event.
		/// </summary>
		public RepoConfigUnlockAnonymousGitAccessAuditEntry RepoConfigUnlockAnonymousGitAccessAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.create event.
		/// </summary>
		public RepoCreateAuditEntry RepoCreateAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.destroy event.
		/// </summary>
		public RepoDestroyAuditEntry RepoDestroyAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.remove_member event.
		/// </summary>
		public RepoRemoveMemberAuditEntry RepoRemoveMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repo.remove_topic event.
		/// </summary>
		public RepoRemoveTopicAuditEntry RepoRemoveTopicAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repository_visibility_change.disable event.
		/// </summary>
		public RepositoryVisibilityChangeDisableAuditEntry RepositoryVisibilityChangeDisableAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a repository_visibility_change.enable event.
		/// </summary>
		public RepositoryVisibilityChangeEnableAuditEntry RepositoryVisibilityChangeEnableAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a team.add_member event.
		/// </summary>
		public TeamAddMemberAuditEntry TeamAddMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a team.add_repository event.
		/// </summary>
		public TeamAddRepositoryAuditEntry TeamAddRepositoryAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a team.change_parent_team event.
		/// </summary>
		public TeamChangeParentTeamAuditEntry TeamChangeParentTeamAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a team.remove_member event.
		/// </summary>
		public TeamRemoveMemberAuditEntry TeamRemoveMemberAuditEntry { get; set; }

		/// <summary>
		/// Audit log entry for a team.remove_repository event.
		/// </summary>
		public TeamRemoveRepositoryAuditEntry TeamRemoveRepositoryAuditEntry { get; set; }
	}
}
