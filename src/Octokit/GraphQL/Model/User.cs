namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user is an individual's account on GitHub that owns repositories and can make new content.
    /// </summary>
    public class User : QueryableValue<User>
    {
        internal User(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Determine if this repository owner has any items that can be pinned to their profile.
        /// </summary>
        /// <param name="type">Filter to only a particular kind of pinnable item.</param>
        public bool AnyPinnableItems(Arg<PinnableItemType>? type = null) => default;

        /// <summary>
        /// A URL pointing to the user's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// The user's public profile bio.
        /// </summary>
        public string Bio { get; }

        /// <summary>
        /// The user's public profile bio as HTML.
        /// </summary>
        public string BioHTML { get; }

        /// <summary>
        /// Could this user receive email notifications, if the organization had notification restrictions enabled?
        /// </summary>
        /// <param name="login">The login of the organization to check.</param>
        public bool CanReceiveOrganizationEmailsWhenNotificationsRestricted(Arg<string> login) => default;

        /// <summary>
        /// A list of commit comments made by this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CommitCommentConnection CommitComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.CommitComments(first, after, last, before), Octokit.GraphQL.Model.CommitCommentConnection.Create);

        /// <summary>
        /// The user's public profile company.
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// The user's public profile company as HTML.
        /// </summary>
        public string CompanyHTML { get; }

        /// <summary>
        /// The collection of contributions this user has made to different repositories.
        /// </summary>
        /// <param name="from">Only contributions made at this time or later will be counted. If omitted, defaults to a year ago.</param>
        /// <param name="organizationID">The ID of the organization used to filter contributions.</param>
        /// <param name="to">Only contributions made before and up to (including) this time will be counted. If omitted, defaults to the current time or one year from the provided from argument.</param>
        public ContributionsCollection ContributionsCollection(Arg<DateTimeOffset>? from = null, Arg<ID>? organizationID = null, Arg<DateTimeOffset>? to = null) => this.CreateMethodCall(x => x.ContributionsCollection(from, organizationID, to), Octokit.GraphQL.Model.ContributionsCollection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The user's publicly visible profile email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// A list of enterprises that the user belongs to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="membershipType">Filter enterprises returned based on the user's membership type.</param>
        /// <param name="orderBy">Ordering options for the User's enterprises.</param>
        public EnterpriseConnection Enterprises(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseMembershipType>? membershipType = null, Arg<EnterpriseOrder>? orderBy = null) => this.CreateMethodCall(x => x.Enterprises(first, after, last, before, membershipType, orderBy), Octokit.GraphQL.Model.EnterpriseConnection.Create);

        /// <summary>
        /// The estimated next GitHub Sponsors payout for this user/organization in cents (USD).
        /// </summary>
        public int EstimatedNextSponsorsPayoutInCents { get; }

        /// <summary>
        /// A list of users the given user is followed by.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public FollowerConnection Followers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Followers(first, after, last, before), Octokit.GraphQL.Model.FollowerConnection.Create);

        /// <summary>
        /// A list of users the given user is following.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public FollowingConnection Following(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Following(first, after, last, before), Octokit.GraphQL.Model.FollowingConnection.Create);

        /// <summary>
        /// Find gist by repo name.
        /// </summary>
        /// <param name="name">The gist name to find.</param>
        public Gist Gist(Arg<string> name) => this.CreateMethodCall(x => x.Gist(name), Octokit.GraphQL.Model.Gist.Create);

        /// <summary>
        /// A list of gist comments made by this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public GistCommentConnection GistComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.GistComments(first, after, last, before), Octokit.GraphQL.Model.GistCommentConnection.Create);

        /// <summary>
        /// A list of the Gists the user has created.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for gists returned from the connection</param>
        /// <param name="privacy">Filters Gists according to privacy.</param>
        public GistConnection Gists(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<GistOrder>? orderBy = null, Arg<GistPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Gists(first, after, last, before, orderBy, privacy), Octokit.GraphQL.Model.GistConnection.Create);

        /// <summary>
        /// True if this user/organization has a GitHub Sponsors listing.
        /// </summary>
        public bool HasSponsorsListing { get; }

        /// <summary>
        /// The hovercard information for this user in a given context
        /// </summary>
        /// <param name="primarySubjectId">The ID of the subject to get the hovercard in the context of</param>
        public Hovercard Hovercard(Arg<ID>? primarySubjectId = null) => this.CreateMethodCall(x => x.Hovercard(primarySubjectId), Octokit.GraphQL.Model.Hovercard.Create);

        /// <summary>
        /// The Node ID of the User object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The interaction ability settings for this user.
        /// </summary>
        public RepositoryInteractionAbility InteractionAbility => this.CreateProperty(x => x.InteractionAbility, Octokit.GraphQL.Model.RepositoryInteractionAbility.Create);

        /// <summary>
        /// Whether or not this user is a participant in the GitHub Security Bug Bounty.
        /// </summary>
        public bool IsBountyHunter { get; }

        /// <summary>
        /// Whether or not this user is a participant in the GitHub Campus Experts Program.
        /// </summary>
        public bool IsCampusExpert { get; }

        /// <summary>
        /// Whether or not this user is a GitHub Developer Program member.
        /// </summary>
        public bool IsDeveloperProgramMember { get; }

        /// <summary>
        /// Whether or not this user is a GitHub employee.
        /// </summary>
        public bool IsEmployee { get; }

        /// <summary>
        /// Whether or not this user is following the viewer. Inverse of viewerIsFollowing
        /// </summary>
        public bool IsFollowingViewer { get; }

        /// <summary>
        /// Whether or not this user is a member of the GitHub Stars Program.
        /// </summary>
        public bool IsGitHubStar { get; }

        /// <summary>
        /// Whether or not the user has marked themselves as for hire.
        /// </summary>
        public bool IsHireable { get; }

        /// <summary>
        /// Whether or not this user is a site administrator.
        /// </summary>
        public bool IsSiteAdmin { get; }

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
        /// Whether or not this user is the viewing user.
        /// </summary>
        public bool IsViewer { get; }

        /// <summary>
        /// A list of issue comments made by this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for issue comments returned from the connection.</param>
        public IssueCommentConnection IssueComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IssueCommentOrder>? orderBy = null) => this.CreateMethodCall(x => x.IssueComments(first, after, last, before, orderBy), Octokit.GraphQL.Model.IssueCommentConnection.Create);

        /// <summary>
        /// A list of issues associated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="filterBy">Filtering options for issues returned from the connection.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection.</param>
        /// <param name="states">A list of states to filter the issues by.</param>
        public IssueConnection Issues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IssueFilters>? filterBy = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<IssueState>>? states = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before, filterBy, labels, orderBy, states), Octokit.GraphQL.Model.IssueConnection.Create);

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
        /// A user-curated list of repositories
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserListConnection Lists(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Lists(first, after, last, before), Octokit.GraphQL.Model.UserListConnection.Create);

        /// <summary>
        /// The user's public profile location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The estimated monthly GitHub Sponsors income for this user/organization in cents (USD).
        /// </summary>
        public int MonthlyEstimatedSponsorsIncomeInCents { get; }

        /// <summary>
        /// The user's public profile name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Find an organization by its login that the user belongs to.
        /// </summary>
        /// <param name="login">The login of the organization to find.</param>
        public Organization Organization(Arg<string> login) => this.CreateMethodCall(x => x.Organization(login), Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// Verified email addresses that match verified domains for a specified organization the user is a member of.
        /// </summary>
        /// <param name="login">The login of the organization to match verified domains from.</param>
        public IEnumerable<string> OrganizationVerifiedDomainEmails(Arg<string> login) => this.CreateMethodCall(x => x.OrganizationVerifiedDomainEmails(login));

        /// <summary>
        /// A list of organizations the user belongs to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the User's organizations.</param>
        public OrganizationConnection Organizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before, orderBy), Octokit.GraphQL.Model.OrganizationConnection.Create);

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
        /// The HTTP path listing user's projects
        /// </summary>
        public string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing user's projects
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
        /// The user's profile pronouns
        /// </summary>
        public string Pronouns { get; }

        /// <summary>
        /// A list of public keys associated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PublicKeyConnection PublicKeys(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PublicKeys(first, after, last, before), Octokit.GraphQL.Model.PublicKeyConnection.Create);

        /// <summary>
        /// A list of pull requests associated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        public PullRequestConnection PullRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? baseRefName = null, Arg<string>? headRefName = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<PullRequestState>>? states = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before, baseRefName, headRefName, labels, orderBy, states), Octokit.GraphQL.Model.PullRequestConnection.Create);

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
        /// A list of repositories that the user recently contributed to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="contributionTypes">If non-null, include only the specified types of contributions. The GitHub.com UI uses [COMMIT, ISSUE, PULL_REQUEST, REPOSITORY]</param>
        /// <param name="hasIssues">If non-null, filters repositories according to whether they have issues enabled</param>
        /// <param name="includeUserRepositories">If true, include user repositories</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        public RepositoryConnection RepositoriesContributedTo(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryContributionType?>>? contributionTypes = null, Arg<bool>? hasIssues = null, Arg<bool>? includeUserRepositories = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.RepositoriesContributedTo(first, after, last, before, contributionTypes, hasIssues, includeUserRepositories, isLocked, orderBy, privacy), Octokit.GraphQL.Model.RepositoryConnection.Create);

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
        /// The HTTP path for this user
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Replies this user has saved
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">The field to order saved replies by.</param>
        public SavedReplyConnection SavedReplies(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SavedReplyOrder>? orderBy = null) => this.CreateMethodCall(x => x.SavedReplies(first, after, last, before, orderBy), Octokit.GraphQL.Model.SavedReplyConnection.Create);

        /// <summary>
        /// The user's social media accounts, ordered as they appear on the user's profile.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public SocialAccountConnection SocialAccounts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.SocialAccounts(first, after, last, before), Octokit.GraphQL.Model.SocialAccountConnection.Create);

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
        /// Repositories the user has starred.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        /// <param name="ownedByViewer">Filters starred repositories to only return repositories owned by the viewer.</param>
        public StarredRepositoryConnection StarredRepositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null, Arg<bool>? ownedByViewer = null) => this.CreateMethodCall(x => x.StarredRepositories(first, after, last, before, orderBy, ownedByViewer), Octokit.GraphQL.Model.StarredRepositoryConnection.Create);

        /// <summary>
        /// The user's description of what they're currently doing.
        /// </summary>
        public UserStatus Status => this.CreateProperty(x => x.Status, Octokit.GraphQL.Model.UserStatus.Create);

        /// <summary>
        /// Suggested names for user lists
        /// </summary>
        public IQueryableList<UserListSuggestion> SuggestedListNames => this.CreateProperty(x => x.SuggestedListNames);

        /// <summary>
        /// Repositories the user has contributed to, ordered by contribution rank, plus repositories the user has created
        /// </summary>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="since">How far back in time to fetch contributed repositories</param>
        public RepositoryConnection TopRepositories(Arg<RepositoryOrder> orderBy, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<DateTimeOffset>? since = null) => this.CreateMethodCall(x => x.TopRepositories(orderBy, first, after, last, before, since), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// The amount in United States cents (e.g., 500 = $5.00 USD) that this entity has spent on GitHub to fund sponsorships. Only returns a value when viewed by the user themselves or by a user who can manage sponsorships for the requested organization.
        /// </summary>
        /// <param name="since">Filter payments to those that occurred on or after this time.</param>
        /// <param name="sponsorableLogins">Filter payments to those made to the users or organizations with the specified usernames.</param>
        /// <param name="until">Filter payments to those that occurred before this time.</param>
        public int? TotalSponsorshipAmountAsSponsorInCents(Arg<DateTimeOffset>? since = null, Arg<IEnumerable<string>>? sponsorableLogins = null, Arg<DateTimeOffset>? until = null) => default;

        /// <summary>
        /// The user's Twitter username.
        /// </summary>
        public string TwitterUsername { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this user
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Can the viewer pin repositories and gists to the profile?
        /// </summary>
        public bool ViewerCanChangePinnedItems { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; }

        /// <summary>
        /// Whether or not the viewer is able to follow the user.
        /// </summary>
        public bool ViewerCanFollow { get; }

        /// <summary>
        /// Whether or not the viewer is able to sponsor this user/organization.
        /// </summary>
        public bool ViewerCanSponsor { get; }

        /// <summary>
        /// Whether or not this user is followed by the viewer. Inverse of isFollowingViewer.
        /// </summary>
        public bool ViewerIsFollowing { get; }

        /// <summary>
        /// True if the viewer is sponsoring this user/organization.
        /// </summary>
        public bool ViewerIsSponsoring { get; }

        /// <summary>
        /// A list of repositories the given user is watching.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Affiliation options for repositories returned from the connection. If none specified, the results will include repositories for which the current viewer is an owner or collaborator, or member.</param>
        /// <param name="hasIssuesEnabled">If non-null, filters repositories according to whether they have issues enabled</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy. Internal repositories are considered private; consider using the visibility argument if only internal repositories are needed. Cannot be combined with the visibility argument.</param>
        /// <param name="visibility">If non-null, filters repositories according to visibility. Cannot be combined with the privacy argument.</param>
        public RepositoryConnection Watching(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? hasIssuesEnabled = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null, Arg<RepositoryVisibility>? visibility = null) => this.CreateMethodCall(x => x.Watching(first, after, last, before, affiliations, hasIssuesEnabled, isLocked, orderBy, ownerAffiliations, privacy, visibility), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// A URL pointing to the user's public website/blog.
        /// </summary>
        public string WebsiteUrl { get; }

        internal static User Create(Expression expression)
        {
            return new User(expression);
        }
    }
}