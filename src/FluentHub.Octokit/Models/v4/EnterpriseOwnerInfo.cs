// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Enterprise information visible to enterprise owners or enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
	/// </summary>
	public class EnterpriseOwnerInfo
	{
		/// <summary>
		/// A list of all of the administrators for this enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="hasTwoFactorEnabled">Only return administrators with this two-factor authentication status.</param>
		/// <param name="orderBy">Ordering options for administrators returned from the connection.</param>
		/// <param name="organizationLogins">Only return members within the organizations with these logins</param>
		/// <param name="query">The search string to look for.</param>
		/// <param name="role">The role to filter by.</param>
		public EnterpriseAdministratorConnection Admins { get; set; }

		/// <summary>
		/// A list of users in the enterprise who currently have two-factor authentication disabled.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public UserConnection AffiliatedUsersWithTwoFactorDisabled { get; set; }

		/// <summary>
		/// Whether or not affiliated users with two-factor authentication disabled exist in the enterprise.
		/// </summary>
		public bool AffiliatedUsersWithTwoFactorDisabledExist { get; set; }

		/// <summary>
		/// The setting value for whether private repository forking is enabled for repositories in organizations in this enterprise.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue AllowPrivateRepositoryForkingSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided private repository forking setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection AllowPrivateRepositoryForkingSettingOrganizations { get; set; }

		/// <summary>
		/// The value for the allow private repository forking policy on the enterprise.
		/// </summary>
		public EnterpriseAllowPrivateRepositoryForkingPolicyValue? AllowPrivateRepositoryForkingSettingPolicyValue { get; set; }

		/// <summary>
		/// The setting value for base repository permissions for organizations in this enterprise.
		/// </summary>
		public EnterpriseDefaultRepositoryPermissionSettingValue DefaultRepositoryPermissionSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided base repository permission.
		/// </summary>
		/// <param name="value">The permission to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection DefaultRepositoryPermissionSettingOrganizations { get; set; }

		/// <summary>
		/// A list of domains owned by the enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with admin:enterprise scope.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="isApproved">Filter whether or not the domain is approved.</param>
		/// <param name="isVerified">Filter whether or not the domain is verified.</param>
		/// <param name="orderBy">Ordering options for verifiable domains returned.</param>
		public VerifiableDomainConnection Domains { get; set; }

		/// <summary>
		/// Enterprise Server installations owned by the enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="connectedOnly">Whether or not to only return installations discovered via GitHub Connect.</param>
		/// <param name="orderBy">Ordering options for Enterprise Server installations returned.</param>
		public EnterpriseServerInstallationConnection EnterpriseServerInstallations { get; set; }

		/// <summary>
		/// A list of failed invitations in the enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="query">The search string to look for.</param>
		public EnterpriseFailedInvitationConnection FailedInvitations { get; set; }

		/// <summary>
		/// The setting value for whether the enterprise has an IP allow list enabled.
		/// </summary>
		public IpAllowListEnabledSettingValue IpAllowListEnabledSetting { get; set; }

		/// <summary>
		/// The IP addresses that are allowed to access resources owned by the enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with admin:enterprise scope.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for IP allow list entries returned.</param>
		public IpAllowListEntryConnection IpAllowListEntries { get; set; }

		/// <summary>
		/// The setting value for whether the enterprise has IP allow list configuration for installed GitHub Apps enabled.
		/// </summary>
		public IpAllowListForInstalledAppsEnabledSettingValue IpAllowListForInstalledAppsEnabledSetting { get; set; }

		/// <summary>
		/// Whether or not the base repository permission is currently being updated.
		/// </summary>
		public bool IsUpdatingDefaultRepositoryPermission { get; set; }

		/// <summary>
		/// Whether the two-factor authentication requirement is currently being enforced.
		/// </summary>
		public bool IsUpdatingTwoFactorRequirement { get; set; }

		/// <summary>
		/// The setting value for whether organization members with admin permissions on a repository can change repository visibility.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue MembersCanChangeRepositoryVisibilitySetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided can change repository visibility setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanChangeRepositoryVisibilitySettingOrganizations { get; set; }

		/// <summary>
		/// The setting value for whether members of organizations in the enterprise can create internal repositories.
		/// </summary>
		public bool? MembersCanCreateInternalRepositoriesSetting { get; set; }

		/// <summary>
		/// The setting value for whether members of organizations in the enterprise can create private repositories.
		/// </summary>
		public bool? MembersCanCreatePrivateRepositoriesSetting { get; set; }

		/// <summary>
		/// The setting value for whether members of organizations in the enterprise can create public repositories.
		/// </summary>
		public bool? MembersCanCreatePublicRepositoriesSetting { get; set; }

		/// <summary>
		/// The setting value for whether members of organizations in the enterprise can create repositories.
		/// </summary>
		public EnterpriseMembersCanCreateRepositoriesSettingValue? MembersCanCreateRepositoriesSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided repository creation setting value.
		/// </summary>
		/// <param name="value">The setting to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanCreateRepositoriesSettingOrganizations { get; set; }

		/// <summary>
		/// The setting value for whether members with admin permissions for repositories can delete issues.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue MembersCanDeleteIssuesSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided members can delete issues setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanDeleteIssuesSettingOrganizations { get; set; }

		/// <summary>
		/// The setting value for whether members with admin permissions for repositories can delete or transfer repositories.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue MembersCanDeleteRepositoriesSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided members can delete repositories setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanDeleteRepositoriesSettingOrganizations { get; set; }

		/// <summary>
		/// The setting value for whether members of organizations in the enterprise can invite outside collaborators.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue MembersCanInviteCollaboratorsSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided members can invite collaborators setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanInviteCollaboratorsSettingOrganizations { get; set; }

		/// <summary>
		/// Indicates whether members of this enterprise's organizations can purchase additional services for those organizations.
		/// </summary>
		public EnterpriseMembersCanMakePurchasesSettingValue MembersCanMakePurchasesSetting { get; set; }

		/// <summary>
		/// The setting value for whether members with admin permissions for repositories can update protected branches.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue MembersCanUpdateProtectedBranchesSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided members can update protected branches setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanUpdateProtectedBranchesSettingOrganizations { get; set; }

		/// <summary>
		/// The setting value for whether members can view dependency insights.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue MembersCanViewDependencyInsightsSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided members can view dependency insights setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection MembersCanViewDependencyInsightsSettingOrganizations { get; set; }

		/// <summary>
		/// Indicates if email notification delivery for this enterprise is restricted to verified or approved domains.
		/// </summary>
		public NotificationRestrictionSettingValue NotificationDeliveryRestrictionEnabledSetting { get; set; }

		/// <summary>
		/// The OIDC Identity Provider for the enterprise.
		/// </summary>
		public OIDCProvider OidcProvider { get; set; }

		/// <summary>
		/// The setting value for whether organization projects are enabled for organizations in this enterprise.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue OrganizationProjectsSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided organization projects setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection OrganizationProjectsSettingOrganizations { get; set; }

		/// <summary>
		/// A list of outside collaborators across the repositories in the enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="hasTwoFactorEnabled">Only return outside collaborators with this two-factor authentication status.</param>
		/// <param name="login">The login of one specific outside collaborator.</param>
		/// <param name="orderBy">Ordering options for outside collaborators returned from the connection.</param>
		/// <param name="organizationLogins">Only return outside collaborators within the organizations with these logins</param>
		/// <param name="query">The search string to look for.</param>
		/// <param name="visibility">Only return outside collaborators on repositories with this visibility.</param>
		public EnterpriseOutsideCollaboratorConnection OutsideCollaborators { get; set; }

		/// <summary>
		/// A list of pending administrator invitations for the enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for pending enterprise administrator invitations returned from the connection.</param>
		/// <param name="query">The search string to look for.</param>
		/// <param name="role">The role to filter by.</param>
		public EnterpriseAdministratorInvitationConnection PendingAdminInvitations { get; set; }

		/// <summary>
		/// A list of pending collaborator invitations across the repositories in the enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for pending repository collaborator invitations returned from the connection.</param>
		/// <param name="query">The search string to look for.</param>
		public RepositoryInvitationConnection PendingCollaboratorInvitations { get; set; }

		/// <summary>
		/// A list of pending member invitations for organizations in the enterprise.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="invitationSource">Only return invitations matching this invitation source</param>
		/// <param name="organizationLogins">Only return invitations within the organizations with these logins</param>
		/// <param name="query">The search string to look for.</param>
		public EnterprisePendingMemberInvitationConnection PendingMemberInvitations { get; set; }

		/// <summary>
		/// The setting value for whether repository projects are enabled in this enterprise.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue RepositoryProjectsSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided repository projects setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection RepositoryProjectsSettingOrganizations { get; set; }

		/// <summary>
		/// The SAML Identity Provider for the enterprise.
		/// </summary>
		public EnterpriseIdentityProvider SamlIdentityProvider { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the SAML single sign-on setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection SamlIdentityProviderSettingOrganizations { get; set; }

		/// <summary>
		/// A list of members with a support entitlement.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for support entitlement users returned from the connection.</param>
		public EnterpriseMemberConnection SupportEntitlements { get; set; }

		/// <summary>
		/// The setting value for whether team discussions are enabled for organizations in this enterprise.
		/// </summary>
		public EnterpriseEnabledDisabledSettingValue TeamDiscussionsSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the provided team discussions setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection TeamDiscussionsSettingOrganizations { get; set; }

		/// <summary>
		/// The setting value for whether the enterprise requires two-factor authentication for its organizations and users.
		/// </summary>
		public EnterpriseEnabledSettingValue TwoFactorRequiredSetting { get; set; }

		/// <summary>
		/// A list of enterprise organizations configured with the two-factor authentication setting value.
		/// </summary>
		/// <param name="value">The setting value to find organizations for.</param>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for organizations with this setting.</param>
		public OrganizationConnection TwoFactorRequiredSettingOrganizations { get; set; }
	}
}
