namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Enterprise information visible to enterprise owners or enterprise owners' personal access tokens (classic) with read:enterprise or admin:enterprise scope.
    /// </summary>
    public class EnterpriseOwnerInfo : QueryableValue<EnterpriseOwnerInfo>
    {
        internal EnterpriseOwnerInfo(Expression expression) : base(expression)
        {
        }

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
        public EnterpriseAdministratorConnection Admins(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? hasTwoFactorEnabled = null, Arg<EnterpriseMemberOrder>? orderBy = null, Arg<IEnumerable<string>>? organizationLogins = null, Arg<string>? query = null, Arg<EnterpriseAdministratorRole>? role = null) => this.CreateMethodCall(x => x.Admins(first, after, last, before, hasTwoFactorEnabled, orderBy, organizationLogins, query, role), Octokit.GraphQL.Model.EnterpriseAdministratorConnection.Create);

        /// <summary>
        /// A list of users in the enterprise who currently have two-factor authentication disabled.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection AffiliatedUsersWithTwoFactorDisabled(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.AffiliatedUsersWithTwoFactorDisabled(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// Whether or not affiliated users with two-factor authentication disabled exist in the enterprise.
        /// </summary>
        public bool AffiliatedUsersWithTwoFactorDisabledExist { get; }

        /// <summary>
        /// The setting value for whether private repository forking is enabled for repositories in organizations in this enterprise.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue AllowPrivateRepositoryForkingSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided private repository forking setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection AllowPrivateRepositoryForkingSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.AllowPrivateRepositoryForkingSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The value for the allow private repository forking policy on the enterprise.
        /// </summary>
        public EnterpriseAllowPrivateRepositoryForkingPolicyValue? AllowPrivateRepositoryForkingSettingPolicyValue { get; }

        /// <summary>
        /// The setting value for base repository permissions for organizations in this enterprise.
        /// </summary>
        public EnterpriseDefaultRepositoryPermissionSettingValue DefaultRepositoryPermissionSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided base repository permission.
        /// </summary>
        /// <param name="value">The permission to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection DefaultRepositoryPermissionSettingOrganizations(Arg<DefaultRepositoryPermissionField> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.DefaultRepositoryPermissionSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

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
        public VerifiableDomainConnection Domains(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? isApproved = null, Arg<bool>? isVerified = null, Arg<VerifiableDomainOrder>? orderBy = null) => this.CreateMethodCall(x => x.Domains(first, after, last, before, isApproved, isVerified, orderBy), Octokit.GraphQL.Model.VerifiableDomainConnection.Create);

        /// <summary>
        /// Enterprise Server installations owned by the enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="connectedOnly">Whether or not to only return installations discovered via GitHub Connect.</param>
        /// <param name="orderBy">Ordering options for Enterprise Server installations returned.</param>
        public EnterpriseServerInstallationConnection EnterpriseServerInstallations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? connectedOnly = null, Arg<EnterpriseServerInstallationOrder>? orderBy = null) => this.CreateMethodCall(x => x.EnterpriseServerInstallations(first, after, last, before, connectedOnly, orderBy), Octokit.GraphQL.Model.EnterpriseServerInstallationConnection.Create);

        /// <summary>
        /// A list of failed invitations in the enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">The search string to look for.</param>
        public EnterpriseFailedInvitationConnection FailedInvitations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.FailedInvitations(first, after, last, before, query), Octokit.GraphQL.Model.EnterpriseFailedInvitationConnection.Create);

        /// <summary>
        /// The setting value for whether the enterprise has an IP allow list enabled.
        /// </summary>
        public IpAllowListEnabledSettingValue IpAllowListEnabledSetting { get; }

        /// <summary>
        /// The IP addresses that are allowed to access resources owned by the enterprise. Visible to enterprise owners or enterprise owners' personal access tokens (classic) with admin:enterprise scope.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for IP allow list entries returned.</param>
        public IpAllowListEntryConnection IpAllowListEntries(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IpAllowListEntryOrder>? orderBy = null) => this.CreateMethodCall(x => x.IpAllowListEntries(first, after, last, before, orderBy), Octokit.GraphQL.Model.IpAllowListEntryConnection.Create);

        /// <summary>
        /// The setting value for whether the enterprise has IP allow list configuration for installed GitHub Apps enabled.
        /// </summary>
        public IpAllowListForInstalledAppsEnabledSettingValue IpAllowListForInstalledAppsEnabledSetting { get; }

        /// <summary>
        /// Whether or not the base repository permission is currently being updated.
        /// </summary>
        public bool IsUpdatingDefaultRepositoryPermission { get; }

        /// <summary>
        /// Whether the two-factor authentication requirement is currently being enforced.
        /// </summary>
        public bool IsUpdatingTwoFactorRequirement { get; }

        /// <summary>
        /// The setting value for whether organization members with admin permissions on a repository can change repository visibility.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue MembersCanChangeRepositoryVisibilitySetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided can change repository visibility setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanChangeRepositoryVisibilitySettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanChangeRepositoryVisibilitySettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The setting value for whether members of organizations in the enterprise can create internal repositories.
        /// </summary>
        public bool? MembersCanCreateInternalRepositoriesSetting { get; }

        /// <summary>
        /// The setting value for whether members of organizations in the enterprise can create private repositories.
        /// </summary>
        public bool? MembersCanCreatePrivateRepositoriesSetting { get; }

        /// <summary>
        /// The setting value for whether members of organizations in the enterprise can create public repositories.
        /// </summary>
        public bool? MembersCanCreatePublicRepositoriesSetting { get; }

        /// <summary>
        /// The setting value for whether members of organizations in the enterprise can create repositories.
        /// </summary>
        public EnterpriseMembersCanCreateRepositoriesSettingValue? MembersCanCreateRepositoriesSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided repository creation setting value.
        /// </summary>
        /// <param name="value">The setting to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanCreateRepositoriesSettingOrganizations(Arg<OrganizationMembersCanCreateRepositoriesSettingValue> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanCreateRepositoriesSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The setting value for whether members with admin permissions for repositories can delete issues.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue MembersCanDeleteIssuesSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided members can delete issues setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanDeleteIssuesSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanDeleteIssuesSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The setting value for whether members with admin permissions for repositories can delete or transfer repositories.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue MembersCanDeleteRepositoriesSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided members can delete repositories setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanDeleteRepositoriesSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanDeleteRepositoriesSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The setting value for whether members of organizations in the enterprise can invite outside collaborators.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue MembersCanInviteCollaboratorsSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided members can invite collaborators setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanInviteCollaboratorsSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanInviteCollaboratorsSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// Indicates whether members of this enterprise's organizations can purchase additional services for those organizations.
        /// </summary>
        public EnterpriseMembersCanMakePurchasesSettingValue MembersCanMakePurchasesSetting { get; }

        /// <summary>
        /// The setting value for whether members with admin permissions for repositories can update protected branches.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue MembersCanUpdateProtectedBranchesSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided members can update protected branches setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanUpdateProtectedBranchesSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanUpdateProtectedBranchesSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The setting value for whether members can view dependency insights.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue MembersCanViewDependencyInsightsSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided members can view dependency insights setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection MembersCanViewDependencyInsightsSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.MembersCanViewDependencyInsightsSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// Indicates if email notification delivery for this enterprise is restricted to verified or approved domains.
        /// </summary>
        public NotificationRestrictionSettingValue NotificationDeliveryRestrictionEnabledSetting { get; }

        /// <summary>
        /// The OIDC Identity Provider for the enterprise.
        /// </summary>
        public OIDCProvider OidcProvider => this.CreateProperty(x => x.OidcProvider, Octokit.GraphQL.Model.OIDCProvider.Create);

        /// <summary>
        /// The setting value for whether organization projects are enabled for organizations in this enterprise.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue OrganizationProjectsSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided organization projects setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection OrganizationProjectsSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.OrganizationProjectsSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

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
        public EnterpriseOutsideCollaboratorConnection OutsideCollaborators(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? hasTwoFactorEnabled = null, Arg<string>? login = null, Arg<EnterpriseMemberOrder>? orderBy = null, Arg<IEnumerable<string>>? organizationLogins = null, Arg<string>? query = null, Arg<RepositoryVisibility>? visibility = null) => this.CreateMethodCall(x => x.OutsideCollaborators(first, after, last, before, hasTwoFactorEnabled, login, orderBy, organizationLogins, query, visibility), Octokit.GraphQL.Model.EnterpriseOutsideCollaboratorConnection.Create);

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
        public EnterpriseAdministratorInvitationConnection PendingAdminInvitations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseAdministratorInvitationOrder>? orderBy = null, Arg<string>? query = null, Arg<EnterpriseAdministratorRole>? role = null) => this.CreateMethodCall(x => x.PendingAdminInvitations(first, after, last, before, orderBy, query, role), Octokit.GraphQL.Model.EnterpriseAdministratorInvitationConnection.Create);

        /// <summary>
        /// A list of pending collaborator invitations across the repositories in the enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for pending repository collaborator invitations returned from the connection.</param>
        /// <param name="query">The search string to look for.</param>
        public RepositoryInvitationConnection PendingCollaboratorInvitations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryInvitationOrder>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.PendingCollaboratorInvitations(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.RepositoryInvitationConnection.Create);

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
        public EnterprisePendingMemberInvitationConnection PendingMemberInvitations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationInvitationSource>? invitationSource = null, Arg<IEnumerable<string>>? organizationLogins = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.PendingMemberInvitations(first, after, last, before, invitationSource, organizationLogins, query), Octokit.GraphQL.Model.EnterprisePendingMemberInvitationConnection.Create);

        /// <summary>
        /// The setting value for whether repository projects are enabled in this enterprise.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue RepositoryProjectsSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided repository projects setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection RepositoryProjectsSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.RepositoryProjectsSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The SAML Identity Provider for the enterprise.
        /// </summary>
        public EnterpriseIdentityProvider SamlIdentityProvider => this.CreateProperty(x => x.SamlIdentityProvider, Octokit.GraphQL.Model.EnterpriseIdentityProvider.Create);

        /// <summary>
        /// A list of enterprise organizations configured with the SAML single sign-on setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection SamlIdentityProviderSettingOrganizations(Arg<IdentityProviderConfigurationState> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.SamlIdentityProviderSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// A list of members with a support entitlement.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for support entitlement users returned from the connection.</param>
        public EnterpriseMemberConnection SupportEntitlements(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseMemberOrder>? orderBy = null) => this.CreateMethodCall(x => x.SupportEntitlements(first, after, last, before, orderBy), Octokit.GraphQL.Model.EnterpriseMemberConnection.Create);

        /// <summary>
        /// The setting value for whether team discussions are enabled for organizations in this enterprise.
        /// </summary>
        public EnterpriseEnabledDisabledSettingValue TeamDiscussionsSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the provided team discussions setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection TeamDiscussionsSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.TeamDiscussionsSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// The setting value for whether the enterprise requires two-factor authentication for its organizations and users.
        /// </summary>
        public EnterpriseEnabledSettingValue TwoFactorRequiredSetting { get; }

        /// <summary>
        /// A list of enterprise organizations configured with the two-factor authentication setting value.
        /// </summary>
        /// <param name="value">The setting value to find organizations for.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations with this setting.</param>
        public OrganizationConnection TwoFactorRequiredSettingOrganizations(Arg<bool> value, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.TwoFactorRequiredSettingOrganizations(value, first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

        internal static EnterpriseOwnerInfo Create(Expression expression)
        {
            return new EnterpriseOwnerInfo(expression);
        }
    }
}