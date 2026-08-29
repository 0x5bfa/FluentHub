namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An audit entry in an organization audit log.
    /// </summary>
    public class OrganizationAuditEntry : QueryableValue<OrganizationAuditEntry>, IUnion
    {
        internal OrganizationAuditEntry(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// Audit log entry for a members_can_delete_repos.clear event.
            /// </summary>
            public Selector<T> MembersCanDeleteReposClearAuditEntry(Func<MembersCanDeleteReposClearAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a members_can_delete_repos.disable event.
            /// </summary>
            public Selector<T> MembersCanDeleteReposDisableAuditEntry(Func<MembersCanDeleteReposDisableAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a members_can_delete_repos.enable event.
            /// </summary>
            public Selector<T> MembersCanDeleteReposEnableAuditEntry(Func<MembersCanDeleteReposEnableAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a oauth_application.create event.
            /// </summary>
            public Selector<T> OauthApplicationCreateAuditEntry(Func<OauthApplicationCreateAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.add_billing_manager
            /// </summary>
            public Selector<T> OrgAddBillingManagerAuditEntry(Func<OrgAddBillingManagerAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.add_member
            /// </summary>
            public Selector<T> OrgAddMemberAuditEntry(Func<OrgAddMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.block_user
            /// </summary>
            public Selector<T> OrgBlockUserAuditEntry(Func<OrgBlockUserAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.config.disable_collaborators_only event.
            /// </summary>
            public Selector<T> OrgConfigDisableCollaboratorsOnlyAuditEntry(Func<OrgConfigDisableCollaboratorsOnlyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.config.enable_collaborators_only event.
            /// </summary>
            public Selector<T> OrgConfigEnableCollaboratorsOnlyAuditEntry(Func<OrgConfigEnableCollaboratorsOnlyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.create event.
            /// </summary>
            public Selector<T> OrgCreateAuditEntry(Func<OrgCreateAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.disable_oauth_app_restrictions event.
            /// </summary>
            public Selector<T> OrgDisableOauthAppRestrictionsAuditEntry(Func<OrgDisableOauthAppRestrictionsAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.disable_saml event.
            /// </summary>
            public Selector<T> OrgDisableSamlAuditEntry(Func<OrgDisableSamlAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.disable_two_factor_requirement event.
            /// </summary>
            public Selector<T> OrgDisableTwoFactorRequirementAuditEntry(Func<OrgDisableTwoFactorRequirementAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.enable_oauth_app_restrictions event.
            /// </summary>
            public Selector<T> OrgEnableOauthAppRestrictionsAuditEntry(Func<OrgEnableOauthAppRestrictionsAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.enable_saml event.
            /// </summary>
            public Selector<T> OrgEnableSamlAuditEntry(Func<OrgEnableSamlAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.enable_two_factor_requirement event.
            /// </summary>
            public Selector<T> OrgEnableTwoFactorRequirementAuditEntry(Func<OrgEnableTwoFactorRequirementAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.invite_member event.
            /// </summary>
            public Selector<T> OrgInviteMemberAuditEntry(Func<OrgInviteMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.invite_to_business event.
            /// </summary>
            public Selector<T> OrgInviteToBusinessAuditEntry(Func<OrgInviteToBusinessAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.oauth_app_access_approved event.
            /// </summary>
            public Selector<T> OrgOauthAppAccessApprovedAuditEntry(Func<OrgOauthAppAccessApprovedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.oauth_app_access_blocked event.
            /// </summary>
            public Selector<T> OrgOauthAppAccessBlockedAuditEntry(Func<OrgOauthAppAccessBlockedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.oauth_app_access_denied event.
            /// </summary>
            public Selector<T> OrgOauthAppAccessDeniedAuditEntry(Func<OrgOauthAppAccessDeniedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.oauth_app_access_requested event.
            /// </summary>
            public Selector<T> OrgOauthAppAccessRequestedAuditEntry(Func<OrgOauthAppAccessRequestedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.oauth_app_access_unblocked event.
            /// </summary>
            public Selector<T> OrgOauthAppAccessUnblockedAuditEntry(Func<OrgOauthAppAccessUnblockedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.remove_billing_manager event.
            /// </summary>
            public Selector<T> OrgRemoveBillingManagerAuditEntry(Func<OrgRemoveBillingManagerAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.remove_member event.
            /// </summary>
            public Selector<T> OrgRemoveMemberAuditEntry(Func<OrgRemoveMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.remove_outside_collaborator event.
            /// </summary>
            public Selector<T> OrgRemoveOutsideCollaboratorAuditEntry(Func<OrgRemoveOutsideCollaboratorAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.restore_member event.
            /// </summary>
            public Selector<T> OrgRestoreMemberAuditEntry(Func<OrgRestoreMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.unblock_user
            /// </summary>
            public Selector<T> OrgUnblockUserAuditEntry(Func<OrgUnblockUserAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.update_default_repository_permission
            /// </summary>
            public Selector<T> OrgUpdateDefaultRepositoryPermissionAuditEntry(Func<OrgUpdateDefaultRepositoryPermissionAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.update_member event.
            /// </summary>
            public Selector<T> OrgUpdateMemberAuditEntry(Func<OrgUpdateMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.update_member_repository_creation_permission event.
            /// </summary>
            public Selector<T> OrgUpdateMemberRepositoryCreationPermissionAuditEntry(Func<OrgUpdateMemberRepositoryCreationPermissionAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a org.update_member_repository_invitation_permission event.
            /// </summary>
            public Selector<T> OrgUpdateMemberRepositoryInvitationPermissionAuditEntry(Func<OrgUpdateMemberRepositoryInvitationPermissionAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a private_repository_forking.disable event.
            /// </summary>
            public Selector<T> PrivateRepositoryForkingDisableAuditEntry(Func<PrivateRepositoryForkingDisableAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a private_repository_forking.enable event.
            /// </summary>
            public Selector<T> PrivateRepositoryForkingEnableAuditEntry(Func<PrivateRepositoryForkingEnableAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.access event.
            /// </summary>
            public Selector<T> RepoAccessAuditEntry(Func<RepoAccessAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.add_member event.
            /// </summary>
            public Selector<T> RepoAddMemberAuditEntry(Func<RepoAddMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.add_topic event.
            /// </summary>
            public Selector<T> RepoAddTopicAuditEntry(Func<RepoAddTopicAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.archived event.
            /// </summary>
            public Selector<T> RepoArchivedAuditEntry(Func<RepoArchivedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.change_merge_setting event.
            /// </summary>
            public Selector<T> RepoChangeMergeSettingAuditEntry(Func<RepoChangeMergeSettingAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.disable_anonymous_git_access event.
            /// </summary>
            public Selector<T> RepoConfigDisableAnonymousGitAccessAuditEntry(Func<RepoConfigDisableAnonymousGitAccessAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.disable_collaborators_only event.
            /// </summary>
            public Selector<T> RepoConfigDisableCollaboratorsOnlyAuditEntry(Func<RepoConfigDisableCollaboratorsOnlyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.disable_contributors_only event.
            /// </summary>
            public Selector<T> RepoConfigDisableContributorsOnlyAuditEntry(Func<RepoConfigDisableContributorsOnlyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.disable_sockpuppet_disallowed event.
            /// </summary>
            public Selector<T> RepoConfigDisableSockpuppetDisallowedAuditEntry(Func<RepoConfigDisableSockpuppetDisallowedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.enable_anonymous_git_access event.
            /// </summary>
            public Selector<T> RepoConfigEnableAnonymousGitAccessAuditEntry(Func<RepoConfigEnableAnonymousGitAccessAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.enable_collaborators_only event.
            /// </summary>
            public Selector<T> RepoConfigEnableCollaboratorsOnlyAuditEntry(Func<RepoConfigEnableCollaboratorsOnlyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.enable_contributors_only event.
            /// </summary>
            public Selector<T> RepoConfigEnableContributorsOnlyAuditEntry(Func<RepoConfigEnableContributorsOnlyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.enable_sockpuppet_disallowed event.
            /// </summary>
            public Selector<T> RepoConfigEnableSockpuppetDisallowedAuditEntry(Func<RepoConfigEnableSockpuppetDisallowedAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.lock_anonymous_git_access event.
            /// </summary>
            public Selector<T> RepoConfigLockAnonymousGitAccessAuditEntry(Func<RepoConfigLockAnonymousGitAccessAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.config.unlock_anonymous_git_access event.
            /// </summary>
            public Selector<T> RepoConfigUnlockAnonymousGitAccessAuditEntry(Func<RepoConfigUnlockAnonymousGitAccessAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.create event.
            /// </summary>
            public Selector<T> RepoCreateAuditEntry(Func<RepoCreateAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.destroy event.
            /// </summary>
            public Selector<T> RepoDestroyAuditEntry(Func<RepoDestroyAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.remove_member event.
            /// </summary>
            public Selector<T> RepoRemoveMemberAuditEntry(Func<RepoRemoveMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repo.remove_topic event.
            /// </summary>
            public Selector<T> RepoRemoveTopicAuditEntry(Func<RepoRemoveTopicAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repository_visibility_change.disable event.
            /// </summary>
            public Selector<T> RepositoryVisibilityChangeDisableAuditEntry(Func<RepositoryVisibilityChangeDisableAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a repository_visibility_change.enable event.
            /// </summary>
            public Selector<T> RepositoryVisibilityChangeEnableAuditEntry(Func<RepositoryVisibilityChangeEnableAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a team.add_member event.
            /// </summary>
            public Selector<T> TeamAddMemberAuditEntry(Func<TeamAddMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a team.add_repository event.
            /// </summary>
            public Selector<T> TeamAddRepositoryAuditEntry(Func<TeamAddRepositoryAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a team.change_parent_team event.
            /// </summary>
            public Selector<T> TeamChangeParentTeamAuditEntry(Func<TeamChangeParentTeamAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a team.remove_member event.
            /// </summary>
            public Selector<T> TeamRemoveMemberAuditEntry(Func<TeamRemoveMemberAuditEntry, T> selector) => default;

            /// <summary>
            /// Audit log entry for a team.remove_repository event.
            /// </summary>
            public Selector<T> TeamRemoveRepositoryAuditEntry(Func<TeamRemoveRepositoryAuditEntry, T> selector) => default;
        }

        internal static OrganizationAuditEntry Create(Expression expression)
        {
            return new OrganizationAuditEntry(expression);
        }
    }
}