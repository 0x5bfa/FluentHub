namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The query root of GitHub's GraphQL interface.
    /// </summary>
    public class Query : QueryableValue<Query>, IQuery
    {
        public Query() : base(null)
        {
        }

        internal Query(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Look up a code of conduct by its key
        /// </summary>
        /// <param name="key">The code of conduct's key</param>
        public CodeOfConduct CodeOfConduct(Arg<string> key) => this.CreateMethodCall(x => x.CodeOfConduct(key), Octokit.GraphQL.Model.CodeOfConduct.Create);

        /// <summary>
        /// Look up a code of conduct by its key
        /// </summary>
        public IQueryableList<CodeOfConduct> CodesOfConduct => this.CreateProperty(x => x.CodesOfConduct);

        /// <summary>
        /// Look up an enterprise by URL slug.
        /// </summary>
        /// <param name="slug">The enterprise URL slug.</param>
        /// <param name="invitationToken">The enterprise invitation token.</param>
        public Enterprise Enterprise(Arg<string> slug, Arg<string>? invitationToken = null) => this.CreateMethodCall(x => x.Enterprise(slug, invitationToken), Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// Look up a pending enterprise administrator invitation by invitee, enterprise and role.
        /// </summary>
        /// <param name="enterpriseSlug">The slug of the enterprise the user was invited to join.</param>
        /// <param name="role">The role for the business member invitation.</param>
        /// <param name="userLogin">The login of the user invited to join the business.</param>
        public EnterpriseAdministratorInvitation EnterpriseAdministratorInvitation(Arg<string> enterpriseSlug, Arg<EnterpriseAdministratorRole> role, Arg<string> userLogin) => this.CreateMethodCall(x => x.EnterpriseAdministratorInvitation(enterpriseSlug, role, userLogin), Octokit.GraphQL.Model.EnterpriseAdministratorInvitation.Create);

        /// <summary>
        /// Look up a pending enterprise administrator invitation by invitation token.
        /// </summary>
        /// <param name="invitationToken">The invitation token sent with the invitation email.</param>
        public EnterpriseAdministratorInvitation EnterpriseAdministratorInvitationByToken(Arg<string> invitationToken) => this.CreateMethodCall(x => x.EnterpriseAdministratorInvitationByToken(invitationToken), Octokit.GraphQL.Model.EnterpriseAdministratorInvitation.Create);

        /// <summary>
        /// Look up an open source license by its key
        /// </summary>
        /// <param name="key">The license's downcased SPDX ID</param>
        public License License(Arg<string> key) => this.CreateMethodCall(x => x.License(key), Octokit.GraphQL.Model.License.Create);

        /// <summary>
        /// Return a list of known open source licenses
        /// </summary>
        public IQueryableList<License> Licenses => this.CreateProperty(x => x.Licenses);

        /// <summary>
        /// Get alphabetically sorted list of Marketplace categories
        /// </summary>
        /// <param name="excludeEmpty">Exclude categories with no listings.</param>
        /// <param name="excludeSubcategories">Returns top level categories only, excluding any subcategories.</param>
        /// <param name="includeCategories">Return only the specified categories.</param>
        public IQueryableList<MarketplaceCategory> MarketplaceCategories(Arg<bool>? excludeEmpty = null, Arg<bool>? excludeSubcategories = null, Arg<IEnumerable<string>>? includeCategories = null) => this.CreateMethodCall(x => x.MarketplaceCategories(excludeEmpty, excludeSubcategories, includeCategories));

        /// <summary>
        /// Look up a Marketplace category by its slug.
        /// </summary>
        /// <param name="slug">The URL slug of the category.</param>
        /// <param name="useTopicAliases">Also check topic aliases for the category slug</param>
        public MarketplaceCategory MarketplaceCategory(Arg<string> slug, Arg<bool>? useTopicAliases = null) => this.CreateMethodCall(x => x.MarketplaceCategory(slug, useTopicAliases), Octokit.GraphQL.Model.MarketplaceCategory.Create);

        /// <summary>
        /// Look up a single Marketplace listing
        /// </summary>
        /// <param name="slug">Select the listing that matches this slug. It's the short name of the listing used in its URL.</param>
        public MarketplaceListing MarketplaceListing(Arg<string> slug) => this.CreateMethodCall(x => x.MarketplaceListing(slug), Octokit.GraphQL.Model.MarketplaceListing.Create);

        /// <summary>
        /// Look up Marketplace listings
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="adminId">Select listings that can be administered by the specified user.</param>
        /// <param name="allStates">Select listings visible to the viewer even if they are not approved. If omitted or false, only approved listings will be returned.</param>
        /// <param name="categorySlug">Select only listings with the given category.</param>
        /// <param name="organizationId">Select listings for products owned by the specified organization.</param>
        /// <param name="primaryCategoryOnly">Select only listings where the primary category matches the given category slug.</param>
        /// <param name="slugs">Select the listings with these slugs, if they are visible to the viewer.</param>
        /// <param name="useTopicAliases">Also check topic aliases for the category slug</param>
        /// <param name="viewerCanAdmin">Select listings to which user has admin access. If omitted, listings visible to the viewer are returned.</param>
        /// <param name="withFreeTrialsOnly">Select only listings that offer a free trial.</param>
        public MarketplaceListingConnection MarketplaceListings(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ID>? adminId = null, Arg<bool>? allStates = null, Arg<string>? categorySlug = null, Arg<ID>? organizationId = null, Arg<bool>? primaryCategoryOnly = null, Arg<IEnumerable<string>>? slugs = null, Arg<bool>? useTopicAliases = null, Arg<bool>? viewerCanAdmin = null, Arg<bool>? withFreeTrialsOnly = null) => this.CreateMethodCall(x => x.MarketplaceListings(first, after, last, before, adminId, allStates, categorySlug, organizationId, primaryCategoryOnly, slugs, useTopicAliases, viewerCanAdmin, withFreeTrialsOnly), Octokit.GraphQL.Model.MarketplaceListingConnection.Create);

        /// <summary>
        /// Return information about the GitHub instance
        /// </summary>
        public GitHubMetadata Meta => this.CreateProperty(x => x.Meta, Octokit.GraphQL.Model.GitHubMetadata.Create);

        /// <summary>
        /// Fetches an object given its ID.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        public INode Node(Arg<ID> id) => this.CreateMethodCall(x => x.Node(id), Octokit.GraphQL.Model.Internal.StubINode.Create);

        /// <summary>
        /// Lookup nodes by a list of IDs.
        /// </summary>
        /// <param name="ids">The list of node IDs.</param>
        public IQueryableList<INode> Nodes(Arg<IEnumerable<ID>> ids) => this.CreateMethodCall(x => x.Nodes(ids));

        /// <summary>
        /// Lookup a organization by login.
        /// </summary>
        /// <param name="login">The organization's login.</param>
        public Organization Organization(Arg<string> login) => this.CreateMethodCall(x => x.Organization(login), Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The client's rate limit information.
        /// </summary>
        /// <param name="dryRun">If true, calculate the cost for the query without evaluating it</param>
        public RateLimit RateLimit(Arg<bool>? dryRun = null) => this.CreateMethodCall(x => x.RateLimit(dryRun), Octokit.GraphQL.Model.RateLimit.Create);

        /// <summary>
        /// Workaround for re-exposing the root query object. (Refer to https://github.com/facebook/relay/issues/112 for more information.)
        /// </summary>
        public Query Relay => this.CreateProperty(x => x.Relay, Octokit.GraphQL.Query.Create);

        /// <summary>
        /// Lookup a given repository by the owner and repository name.
        /// </summary>
        /// <param name="name">The name of the repository</param>
        /// <param name="owner">The login field of a user or organization</param>
        /// <param name="followRenames">Follow repository renames. If disabled, a repository referenced by its old name will return an error.</param>
        public Repository Repository(Arg<string> name, Arg<string> owner, Arg<bool>? followRenames = null) => this.CreateMethodCall(x => x.Repository(name, owner, followRenames), Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Lookup a repository owner (ie. either a User or an Organization) by login.
        /// </summary>
        /// <param name="login">The username to lookup the owner by.</param>
        public IRepositoryOwner RepositoryOwner(Arg<string> login) => this.CreateMethodCall(x => x.RepositoryOwner(login), Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// Lookup resource by a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public IUniformResourceLocatable Resource(Arg<string> url) => this.CreateMethodCall(x => x.Resource(url), Octokit.GraphQL.Model.Internal.StubIUniformResourceLocatable.Create);

        /// <summary>
        /// Perform a search across resources, returning a maximum of 1,000 results.
        /// </summary>
        /// <param name="query">The search string to look for. GitHub search syntax is supported. For more information, see "[Searching on GitHub](https://docs.github.com/search-github/searching-on-github)," "[Understanding the search syntax](https://docs.github.com/search-github/getting-started-with-searching-on-github/understanding-the-search-syntax)," and "[Sorting search results](https://docs.github.com/search-github/getting-started-with-searching-on-github/sorting-search-results)."</param>
        /// <param name="type">The types of search items to search within.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public SearchResultItemConnection Search(Arg<string> query, Arg<SearchType> type, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Search(query, type, first, after, last, before), Octokit.GraphQL.Model.SearchResultItemConnection.Create);

        /// <summary>
        /// GitHub Security Advisories
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="classifications">A list of classifications to filter advisories by.</param>
        /// <param name="identifier">Filter advisories by identifier, e.g. GHSA or CVE.</param>
        /// <param name="orderBy">Ordering options for the returned topics.</param>
        /// <param name="publishedSince">Filter advisories to those published since a time in the past.</param>
        /// <param name="updatedSince">Filter advisories to those updated since a time in the past.</param>
        public SecurityAdvisoryConnection SecurityAdvisories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<SecurityAdvisoryClassification>>? classifications = null, Arg<SecurityAdvisoryIdentifierFilter>? identifier = null, Arg<SecurityAdvisoryOrder>? orderBy = null, Arg<DateTimeOffset>? publishedSince = null, Arg<DateTimeOffset>? updatedSince = null) => this.CreateMethodCall(x => x.SecurityAdvisories(first, after, last, before, classifications, identifier, orderBy, publishedSince, updatedSince), Octokit.GraphQL.Model.SecurityAdvisoryConnection.Create);

        /// <summary>
        /// Fetch a Security Advisory by its GHSA ID
        /// </summary>
        /// <param name="ghsaId">GitHub Security Advisory ID.</param>
        public SecurityAdvisory SecurityAdvisory(Arg<string> ghsaId) => this.CreateMethodCall(x => x.SecurityAdvisory(ghsaId), Octokit.GraphQL.Model.SecurityAdvisory.Create);

        /// <summary>
        /// Software Vulnerabilities documented by GitHub Security Advisories
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="classifications">A list of advisory classifications to filter vulnerabilities by.</param>
        /// <param name="ecosystem">An ecosystem to filter vulnerabilities by.</param>
        /// <param name="orderBy">Ordering options for the returned topics.</param>
        /// <param name="package">A package name to filter vulnerabilities by.</param>
        /// <param name="severities">A list of severities to filter vulnerabilities by.</param>
        public SecurityVulnerabilityConnection SecurityVulnerabilities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<SecurityAdvisoryClassification>>? classifications = null, Arg<SecurityAdvisoryEcosystem>? ecosystem = null, Arg<SecurityVulnerabilityOrder>? orderBy = null, Arg<string>? package = null, Arg<IEnumerable<SecurityAdvisorySeverity>>? severities = null) => this.CreateMethodCall(x => x.SecurityVulnerabilities(first, after, last, before, classifications, ecosystem, orderBy, package, severities), Octokit.GraphQL.Model.SecurityVulnerabilityConnection.Create);

        /// <summary>
        /// Users and organizations who can be sponsored via GitHub Sponsors.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="dependencyEcosystem">Optional filter for which dependencies should be checked for sponsorable owners. Only sponsorable owners of dependencies in this ecosystem will be included. Used when onlyDependencies = true. **Upcoming Change on 2022-07-01 UTC** **Description:** `dependencyEcosystem` will be removed. Use the ecosystem argument instead. **Reason:** The type is switching from SecurityAdvisoryEcosystem to DependencyGraphEcosystem.</param>
        /// <param name="ecosystem">Optional filter for which dependencies should be checked for sponsorable owners. Only sponsorable owners of dependencies in this ecosystem will be included. Used when onlyDependencies = true.</param>
        /// <param name="onlyDependencies">Whether only sponsorables who own the viewer's dependencies will be returned. Must be authenticated to use. Can check an organization instead for their dependencies owned by sponsorables by passing orgLoginForDependencies.</param>
        /// <param name="orderBy">Ordering options for users and organizations returned from the connection.</param>
        /// <param name="orgLoginForDependencies">Optional organization username for whose dependencies should be checked. Used when onlyDependencies = true. Omit to check your own dependencies. If you are not an administrator of the organization, only dependencies from its public repositories will be considered.</param>
        public SponsorableItemConnection Sponsorables(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SecurityAdvisoryEcosystem>? dependencyEcosystem = null, Arg<DependencyGraphEcosystem>? ecosystem = null, Arg<bool>? onlyDependencies = null, Arg<SponsorableOrder>? orderBy = null, Arg<string>? orgLoginForDependencies = null) => this.CreateMethodCall(x => x.Sponsorables(first, after, last, before, dependencyEcosystem, ecosystem, onlyDependencies, orderBy, orgLoginForDependencies), Octokit.GraphQL.Model.SponsorableItemConnection.Create);

        /// <summary>
        /// Look up a topic by name.
        /// </summary>
        /// <param name="name">The topic's name.</param>
        public Topic Topic(Arg<string> name) => this.CreateMethodCall(x => x.Topic(name), Octokit.GraphQL.Model.Topic.Create);

        /// <summary>
        /// Lookup a user by login.
        /// </summary>
        /// <param name="login">The user's login.</param>
        public User User(Arg<string> login) => this.CreateMethodCall(x => x.User(login), Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The currently authenticated user.
        /// </summary>
        public User Viewer => this.CreateProperty(x => x.Viewer, Octokit.GraphQL.Model.User.Create);

        internal static Query Create(Expression expression)
        {
            return new Query(expression);
        }
    }
}