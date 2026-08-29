namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An account on GitHub, with one or more owners, that has repositories, members and teams.
    /// </summary>
    public class Organization : QueryableValue<Organization>
    {
        internal Organization(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The text of the announcement
        /// </summary>
        public string Announcement { get; }

        /// <summary>
        /// The expiration date of the announcement, if any
        /// </summary>
        public DateTimeOffset? AnnouncementExpiresAt { get; }

        /// <summary>
        /// Whether the announcement can be dismissed by the user
        /// </summary>
        public bool? AnnouncementUserDismissible { get; }

        /// <summary>
        /// Determine if this repository owner has any items that can be pinned to their profile.
        /// </summary>
        /// <param name="type">Filter to only a particular kind of pinnable item.</param>
        public bool AnyPinnableItems(Arg<PinnableItemType>? type = null) => default;

        /// <summary>
        /// Identifies the date and time when the organization was archived.
        /// </summary>
        public DateTimeOffset? ArchivedAt { get; }

        /// <summary>
        /// Audit log entries of the organization
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the returned audit log entries.</param>
        /// <param name="query">The query string to filter audit entries</param>
        public OrganizationAuditEntryConnection AuditLog(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<AuditLogOrder>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.AuditLog(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.OrganizationAuditEntryConnection.Create);

        /// <summary>
        /// A URL pointing to the organization's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The organization's public profile description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The organization's public profile description rendered to HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// A list of domains owned by the organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="isApproved">Filter by if the domain is approved.</param>
        /// <param name="isVerified">Filter by if the domain is verified.</param>
        /// <param name="orderBy">Ordering options for verifiable domains returned.</param>
        public VerifiableDomainConnection Domains(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? isApproved = null, Arg<bool>? isVerified = null, Arg<VerifiableDomainOrder>? orderBy = null) => this.CreateMethodCall(x => x.Domains(first, after, last, before, isApproved, isVerified, orderBy), Octokit.GraphQL.Model.VerifiableDomainConnection.Create);

        /// <summary>
        /// The organization's public email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// A list of owners of the organization's enterprise account.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for enterprise owners returned from the connection.</param>
        /// <param name="organizationRole">The organization role to filter by.</param>
        /// <param name="query">The search string to look for.</param>
        public OrganizationEnterpriseOwnerConnection EnterpriseOwners(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrgEnterpriseOwnerOrder>? orderBy = null, Arg<RoleInOrganization>? organizationRole = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.EnterpriseOwners(first, after, last, before, orderBy, organizationRole, query), Octokit.GraphQL.Model.OrganizationEnterpriseOwnerConnection.Create);

        /// <summary>
        /// The estimated next GitHub Sponsors payout for this user/organization in cents (USD).
        /// </summary>
        public int EstimatedNextSponsorsPayoutInCents { get; }

        /// <summary>
        /// True if this user/organization has a GitHub Sponsors listing.
        /// </summary>
        public bool HasSponsorsListing { get; }

        /// <summary>
        /// The Node ID of the Organization object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The interaction ability settings for this organization.
        /// </summary>
        public RepositoryInteractionAbility InteractionAbility => this.CreateProperty(x => x.InteractionAbility, Octokit.GraphQL.Model.RepositoryInteractionAbility.Create);

        /// <summary>
        /// The setting value for whether the organization has an IP allow list enabled.
        /// </summary>
        public IpAllowListEnabledSettingValue IpAllowListEnabledSetting { get; }

        /// <summary>
        /// The IP addresses that are allowed to access resources owned by the organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for IP allow list entries returned.</param>
        public IpAllowListEntryConnection IpAllowListEntries(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IpAllowListEntryOrder>? orderBy = null) => this.CreateMethodCall(x => x.IpAllowListEntries(first, after, last, before, orderBy), Octokit.GraphQL.Model.IpAllowListEntryConnection.Create);

        /// <summary>
        /// The setting value for whether the organization has IP allow list configuration for installed GitHub Apps enabled.
        /// </summary>
        public IpAllowListForInstalledAppsEnabledSettingValue IpAllowListForInstalledAppsEnabledSetting { get; }

        /// <summary>
        /// Whether the given account is sponsoring this user/organization.
        /// </summary>
        /// <param name="accountLogin">The target account's login.</param>
        public bool IsSponsoredBy(Arg<string> accountLogin) => default;

        /// <summary>
        /// True if the viewer is sponsored by this user/organization.
        /// </summary>
        public bool IsSponsoringViewer { get; }

        /// <summary>
        /// Whether the organization has verified its profile email and website.
        /// </summary>
        public bool IsVerified { get; }

        /// <summary>
        /// Showcases a selection of repositories and gists that the profile owner has either curated or that have been selected automatically based on popularity.
        /// </summary>
        public ProfileItemShowcase ItemShowcase => this.CreateProperty(x => x.ItemShowcase, Octokit.GraphQL.Model.ProfileItemShowcase.Create);

        /// <summary>
        /// Calculate how much each sponsor has ever paid total to this maintainer via GitHub Sponsors. Does not include sponsorships paid via Patreon.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for results returned from the connection.</param>
        public SponsorAndLifetimeValueConnection LifetimeReceivedSponsorshipValues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorAndLifetimeValueOrder>? orderBy = null) => this.CreateMethodCall(x => x.LifetimeReceivedSponsorshipValues(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorAndLifetimeValueConnection.Create);

        /// <summary>
        /// The organization's public profile location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// The organization's login name.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// A list of all mannequins for this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="login">Filter mannequins by login.</param>
        /// <param name="orderBy">Ordering options for mannequins returned from the connection.</param>
        public MannequinConnection Mannequins(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? login = null, Arg<MannequinOrder>? orderBy = null) => this.CreateMethodCall(x => x.Mannequins(first, after, last, before, login, orderBy), Octokit.GraphQL.Model.MannequinConnection.Create);

        /// <summary>
        /// Get the status messages members of this entity have set that are either public or visible only to the organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for user statuses returned from the connection.</param>
        public UserStatusConnection MemberStatuses(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<UserStatusOrder>? orderBy = null) => this.CreateMethodCall(x => x.MemberStatuses(first, after, last, before, orderBy), Octokit.GraphQL.Model.UserStatusConnection.Create);

        /// <summary>
        /// Members can fork private repositories in this organization
        /// </summary>
        public bool MembersCanForkPrivateRepositories { get; }

        /// <summary>
        /// A list of users who are members of this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public OrganizationMemberConnection MembersWithRole(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.MembersWithRole(first, after, last, before), Octokit.GraphQL.Model.OrganizationMemberConnection.Create);

        /// <summary>
        /// The estimated monthly GitHub Sponsors income for this user/organization in cents (USD).
        /// </summary>
        public int MonthlyEstimatedSponsorsIncomeInCents { get; }

        /// <summary>
        /// The organization's public profile name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The HTTP path creating a new team
        /// </summary>
        public string NewTeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL creating a new team
        /// </summary>
        public string NewTeamUrl { get; }

        /// <summary>
        /// Indicates if email notification delivery for this organization is restricted to verified or approved domains.
        /// </summary>
        public NotificationRestrictionSettingValue NotificationDeliveryRestrictionEnabledSetting { get; }

        /// <summary>
        /// The billing email for the organization.
        /// </summary>
        public string OrganizationBillingEmail { get; }

        /// <summary>
        /// A list of packages under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="names">Find packages by their names.</param>
        /// <param name="orderBy">Ordering of the returned packages.</param>
        /// <param name="packageType">Filter registry package by type.</param>
        /// <param name="repositoryId">Find packages in a repository by ID.</param>
        public PackageConnection Packages(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<string>>? names = null, Arg<PackageOrder>? orderBy = null, Arg<PackageType>? packageType = null, Arg<ID>? repositoryId = null) => this.CreateMethodCall(x => x.Packages(first, after, last, before, names, orderBy, packageType, repositoryId), Octokit.GraphQL.Model.PackageConnection.Create);

        /// <summary>
        /// A list of users who have been invited to join this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection PendingMembers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PendingMembers(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// A list of repositories and gists this profile owner can pin to their profile.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="types">Filter the types of pinnable items that are returned.</param>
        public PinnableItemConnection PinnableItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PinnableItemType>>? types = null) => this.CreateMethodCall(x => x.PinnableItems(first, after, last, before, types), Octokit.GraphQL.Model.PinnableItemConnection.Create);

        /// <summary>
        /// A list of repositories and gists this profile owner has pinned to their profile
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="types">Filter the types of pinned items that are returned.</param>
        public PinnableItemConnection PinnedItems(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<PinnableItemType>>? types = null) => this.CreateMethodCall(x => x.PinnedItems(first, after, last, before, types), Octokit.GraphQL.Model.PinnableItemConnection.Create);

        /// <summary>
        /// Returns how many more items this profile owner can pin to their profile.
        /// </summary>
        public int PinnedItemsRemaining { get; }

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        public Project Project(Arg<int> number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// Find a project by number.
        /// </summary>
        /// <param name="number">The project number.</param>
        public ProjectV2 ProjectV2(Arg<int> number) => this.CreateMethodCall(x => x.ProjectV2(number), Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for projects returned from the connection</param>
        /// <param name="search">Query to search projects by, currently only searching by name.</param>
        /// <param name="states">A list of states to filter the projects by.</param>
        public ProjectConnection Projects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectOrder>? orderBy = null, Arg<string>? search = null, Arg<IEnumerable<ProjectState>>? states = null) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octokit.GraphQL.Model.ProjectConnection.Create);

        /// <summary>
        /// The HTTP path listing organization's projects
        /// </summary>
        public string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing organization's projects
        /// </summary>
        public string ProjectsUrl { get; }

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">How to order the returned projects.</param>
        /// <param name="query">A project to search for under the the owner.</param>
        public ProjectV2Connection ProjectsV2(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2Order>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.ProjectsV2(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        /// <summary>
        /// Recent projects that this user has modified in the context of the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2Connection RecentProjects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.RecentProjects(first, after, last, before), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        /// <summary>
        /// A list of repositories that the user owns.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="hasIssuesEnabled">If non-null, filters repositories according to whether they have issues enabled</param>
        /// <param name="isArchived">If non-null, filters repositories according to whether they are archived and not maintained</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy. Internal repositories are considered private; consider using the visibility argument if only internal repositories are needed. Cannot be combined with the visibility argument.</param>
        /// <param name="visibility">If non-null, filters repositories according to visibility. Cannot be combined with the privacy argument.</param>
        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? hasIssuesEnabled = null, Arg<bool>? isArchived = null, Arg<bool>? isFork = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null, Arg<RepositoryVisibility>? visibility = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, affiliations, hasIssuesEnabled, isArchived, isFork, isLocked, orderBy, ownerAffiliations, privacy, visibility), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        /// <param name="followRenames">Follow repository renames. If disabled, a repository referenced by its old name will return an error.</param>
        public Repository Repository(Arg<string> name, Arg<bool>? followRenames = null) => this.CreateMethodCall(x => x.Repository(name, followRenames), Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Discussion comments this user has authored.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="onlyAnswers">Filter discussion comments to only those that were marked as the answer</param>
        /// <param name="repositoryId">Filter discussion comments to only those in a specific repository.</param>
        public DiscussionCommentConnection RepositoryDiscussionComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? onlyAnswers = null, Arg<ID>? repositoryId = null) => this.CreateMethodCall(x => x.RepositoryDiscussionComments(first, after, last, before, onlyAnswers, repositoryId), Octokit.GraphQL.Model.DiscussionCommentConnection.Create);

        /// <summary>
        /// Discussions this user has started.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="answered">Filter discussions to only those that have been answered or not. Defaults to including both answered and unanswered discussions.</param>
        /// <param name="orderBy">Ordering options for discussions returned from the connection.</param>
        /// <param name="repositoryId">Filter discussions to only those in a specific repository.</param>
        /// <param name="states">A list of states to filter the discussions by.</param>
        public DiscussionConnection RepositoryDiscussions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? answered = null, Arg<DiscussionOrder>? orderBy = null, Arg<ID>? repositoryId = null, Arg<IEnumerable<DiscussionState>>? states = null) => this.CreateMethodCall(x => x.RepositoryDiscussions(first, after, last, before, answered, orderBy, repositoryId, states), Octokit.GraphQL.Model.DiscussionConnection.Create);

        /// <summary>
        /// A list of all repository migrations for this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for repository migrations returned.</param>
        /// <param name="repositoryName">Filter repository migrations by repository name.</param>
        /// <param name="state">Filter repository migrations by state.</param>
        public RepositoryMigrationConnection RepositoryMigrations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryMigrationOrder>? orderBy = null, Arg<string>? repositoryName = null, Arg<MigrationState>? state = null) => this.CreateMethodCall(x => x.RepositoryMigrations(first, after, last, before, orderBy, repositoryName, state), Octokit.GraphQL.Model.RepositoryMigrationConnection.Create);

        /// <summary>
        /// When true the organization requires all members, billing managers, and outside collaborators to enable two-factor authentication.
        /// </summary>
        public bool? RequiresTwoFactorAuthentication { get; }

        /// <summary>
        /// The HTTP path for this organization.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Returns a single ruleset from the current organization by ID.
        /// </summary>
        /// <param name="databaseId">The ID of the ruleset to be returned.</param>
        public RepositoryRuleset Ruleset(Arg<int> databaseId) => this.CreateMethodCall(x => x.Ruleset(databaseId), Octokit.GraphQL.Model.RepositoryRuleset.Create);

        /// <summary>
        /// A list of rulesets for this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeParents">Return rulesets configured at higher levels that apply to this organization</param>
        public RepositoryRulesetConnection Rulesets(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includeParents = null) => this.CreateMethodCall(x => x.Rulesets(first, after, last, before, includeParents), Octokit.GraphQL.Model.RepositoryRulesetConnection.Create);

        /// <summary>
        /// The Organization's SAML identity provider. Visible to (1) organization owners, (2) organization owners' personal access tokens (classic) with read:org or admin:org scope, (3) GitHub App with an installation token with read or write access to members.
        /// </summary>
        public OrganizationIdentityProvider SamlIdentityProvider => this.CreateProperty(x => x.SamlIdentityProvider, Octokit.GraphQL.Model.OrganizationIdentityProvider.Create);

        /// <summary>
        /// List of users and organizations this entity is sponsoring.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the users and organizations returned from the connection.</param>
        public SponsorConnection Sponsoring(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null) => this.CreateMethodCall(x => x.Sponsoring(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorConnection.Create);

        /// <summary>
        /// List of sponsors for this user or organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsors returned from the connection.</param>
        /// <param name="tierId">If given, will filter for sponsors at the given tier. Will only return sponsors whose tier the viewer is permitted to see.</param>
        public SponsorConnection Sponsors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null, Arg<ID>? tierId = null) => this.CreateMethodCall(x => x.Sponsors(first, after, last, before, orderBy, tierId), Octokit.GraphQL.Model.SponsorConnection.Create);

        /// <summary>
        /// Events involving this sponsorable, such as new sponsorships.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="actions">Filter activities to only the specified actions.</param>
        /// <param name="includeAsSponsor">Whether to include those events where this sponsorable acted as the sponsor. Defaults to only including events where this sponsorable was the recipient of a sponsorship.</param>
        /// <param name="includePrivate">Whether or not to include private activities in the result set. Defaults to including public and private activities.</param>
        /// <param name="orderBy">Ordering options for activity returned from the connection.</param>
        /// <param name="period">Filter activities returned to only those that occurred in the most recent specified time period. Set to ALL to avoid filtering by when the activity occurred. Will be ignored if `since` or `until` is given.</param>
        /// <param name="since">Filter activities to those that occurred on or after this time.</param>
        /// <param name="until">Filter activities to those that occurred before this time.</param>
        public SponsorsActivityConnection SponsorsActivities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<SponsorsActivityAction>>? actions = null, Arg<bool>? includeAsSponsor = null, Arg<bool>? includePrivate = null, Arg<SponsorsActivityOrder>? orderBy = null, Arg<SponsorsActivityPeriod>? period = null, Arg<DateTimeOffset>? since = null, Arg<DateTimeOffset>? until = null) => this.CreateMethodCall(x => x.SponsorsActivities(first, after, last, before, actions, includeAsSponsor, includePrivate, orderBy, period, since, until), Octokit.GraphQL.Model.SponsorsActivityConnection.Create);

        /// <summary>
        /// The GitHub Sponsors listing for this user or organization.
        /// </summary>
        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        /// <summary>
        /// The sponsorship from the viewer to this user/organization; that is, the sponsorship where you're the sponsor.
        /// </summary>
        /// <param name="activeOnly">Whether to return the sponsorship only if it's still active. Pass false to get the viewer's sponsorship back even if it has been cancelled.</param>
        public Sponsorship SponsorshipForViewerAsSponsor(Arg<bool>? activeOnly = null) => this.CreateMethodCall(x => x.SponsorshipForViewerAsSponsor(activeOnly), Octokit.GraphQL.Model.Sponsorship.Create);

        /// <summary>
        /// The sponsorship from this user/organization to the viewer; that is, the sponsorship you're receiving.
        /// </summary>
        /// <param name="activeOnly">Whether to return the sponsorship only if it's still active. Pass false to get the sponsorship back even if it has been cancelled.</param>
        public Sponsorship SponsorshipForViewerAsSponsorable(Arg<bool>? activeOnly = null) => this.CreateMethodCall(x => x.SponsorshipForViewerAsSponsorable(activeOnly), Octokit.GraphQL.Model.Sponsorship.Create);

        /// <summary>
        /// List of sponsorship updates sent from this sponsorable to sponsors.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsorship updates returned from the connection.</param>
        public SponsorshipNewsletterConnection SponsorshipNewsletters(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipNewsletterOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipNewsletters(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorshipNewsletterConnection.Create);

        /// <summary>
        /// The sponsorships where this user or organization is the maintainer receiving the funds.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="activeOnly">Whether to include only sponsorships that are active right now, versus all sponsorships this maintainer has ever received.</param>
        /// <param name="includePrivate">Whether or not to include private sponsorships in the result set</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        public SponsorshipConnection SponsorshipsAsMaintainer(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? activeOnly = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipsAsMaintainer(first, after, last, before, activeOnly, includePrivate, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        /// <summary>
        /// The sponsorships where this user or organization is the funder.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="activeOnly">Whether to include only sponsorships that are active right now, versus all sponsorships this sponsor has ever made.</param>
        /// <param name="maintainerLogins">Filter sponsorships returned to those for the specified maintainers. That is, the recipient of the sponsorship is a user or organization with one of the given logins.</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        public SponsorshipConnection SponsorshipsAsSponsor(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? activeOnly = null, Arg<IEnumerable<string>>? maintainerLogins = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipsAsSponsor(first, after, last, before, activeOnly, maintainerLogins, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        /// <summary>
        /// Find an organization's team by its slug.
        /// </summary>
        /// <param name="slug">The name or slug of the team to find.</param>
        public Team Team(Arg<string> slug) => this.CreateMethodCall(x => x.Team(slug), Octokit.GraphQL.Model.Team.Create);

        /// <summary>
        /// A list of teams in this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="ldapMapped">If true, filters teams that are mapped to an LDAP Group (Enterprise only)</param>
        /// <param name="notificationSetting">If non-null, filters teams according to notification setting</param>
        /// <param name="orderBy">Ordering options for teams returned from the connection</param>
        /// <param name="privacy">If non-null, filters teams according to privacy</param>
        /// <param name="query">If non-null, filters teams with query on team name and team slug</param>
        /// <param name="role">If non-null, filters teams according to whether the viewer is an admin or member on team</param>
        /// <param name="rootTeamsOnly">If true, restrict to only root teams</param>
        /// <param name="userLogins">User logins to filter by</param>
        public TeamConnection Teams(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? ldapMapped = null, Arg<TeamNotificationSetting>? notificationSetting = null, Arg<TeamOrder>? orderBy = null, Arg<TeamPrivacy>? privacy = null, Arg<string>? query = null, Arg<TeamRole>? role = null, Arg<bool>? rootTeamsOnly = null, Arg<IEnumerable<string>>? userLogins = null) => this.CreateMethodCall(x => x.Teams(first, after, last, before, ldapMapped, notificationSetting, orderBy, privacy, query, role, rootTeamsOnly, userLogins), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// The HTTP path listing organization's teams
        /// </summary>
        public string TeamsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing organization's teams
        /// </summary>
        public string TeamsUrl { get; }

        /// <summary>
        /// The amount in United States cents (e.g., 500 = $5.00 USD) that this entity has spent on GitHub to fund sponsorships. Only returns a value when viewed by the user themselves or by a user who can manage sponsorships for the requested organization.
        /// </summary>
        /// <param name="since">Filter payments to those that occurred on or after this time.</param>
        /// <param name="sponsorableLogins">Filter payments to those made to the users or organizations with the specified usernames.</param>
        /// <param name="until">Filter payments to those that occurred before this time.</param>
        public int? TotalSponsorshipAmountAsSponsorInCents(Arg<DateTimeOffset>? since = null, Arg<IEnumerable<string>>? sponsorableLogins = null, Arg<DateTimeOffset>? until = null) => default;

        /// <summary>
        /// The organization's Twitter username.
        /// </summary>
        public string TwitterUsername { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this organization.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Organization is adminable by the viewer.
        /// </summary>
        public bool ViewerCanAdminister { get; }

        /// <summary>
        /// Can the viewer pin repositories and gists to the profile?
        /// </summary>
        public bool ViewerCanChangePinnedItems { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; }

        /// <summary>
        /// Viewer can create repositories on this organization
        /// </summary>
        public bool ViewerCanCreateRepositories { get; }

        /// <summary>
        /// Viewer can create teams on this organization.
        /// </summary>
        public bool ViewerCanCreateTeams { get; }

        /// <summary>
        /// Whether or not the viewer is able to sponsor this user/organization.
        /// </summary>
        public bool ViewerCanSponsor { get; }

        /// <summary>
        /// Viewer is an active member of this organization.
        /// </summary>
        public bool ViewerIsAMember { get; }

        /// <summary>
        /// Whether or not this Organization is followed by the viewer.
        /// </summary>
        public bool ViewerIsFollowing { get; }

        /// <summary>
        /// True if the viewer is sponsoring this user/organization.
        /// </summary>
        public bool ViewerIsSponsoring { get; }

        /// <summary>
        /// Whether contributors are required to sign off on web-based commits for repositories in this organization.
        /// </summary>
        public bool WebCommitSignoffRequired { get; }

        /// <summary>
        /// The organization's public profile URL.
        /// </summary>
        public string WebsiteUrl { get; }

        internal static Organization Create(Expression expression)
        {
            return new Organization(expression);
        }
    }
}