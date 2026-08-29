namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository contains the content for a project.
    /// </summary>
    public class Repository : QueryableValue<Repository>
    {
        internal Repository(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Whether or not a pull request head branch that is behind its base branch can always be updated even if it is not required to be up to date before merging.
        /// </summary>
        public bool AllowUpdateBranch { get; }

        /// <summary>
        /// Identifies the date and time when the repository was archived.
        /// </summary>
        public DateTimeOffset? ArchivedAt { get; }

        /// <summary>
        /// A list of users that can be assigned to issues in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">Filters users with query on user name and login.</param>
        public UserConnection AssignableUsers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.AssignableUsers(first, after, last, before, query), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// Whether or not Auto-merge can be enabled on pull requests in this repository.
        /// </summary>
        public bool AutoMergeAllowed { get; }

        /// <summary>
        /// A list of branch protection rules for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BranchProtectionRuleConnection BranchProtectionRules(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.BranchProtectionRules(first, after, last, before), Octokit.GraphQL.Model.BranchProtectionRuleConnection.Create);

        /// <summary>
        /// Returns the code of conduct for this repository
        /// </summary>
        public CodeOfConduct CodeOfConduct => this.CreateProperty(x => x.CodeOfConduct, Octokit.GraphQL.Model.CodeOfConduct.Create);

        /// <summary>
        /// Information extracted from the repository's `CODEOWNERS` file.
        /// </summary>
        /// <param name="refName">The ref name used to return the associated `CODEOWNERS` file.</param>
        public RepositoryCodeowners Codeowners(Arg<string>? refName = null) => this.CreateMethodCall(x => x.Codeowners(refName), Octokit.GraphQL.Model.RepositoryCodeowners.Create);

        /// <summary>
        /// A list of collaborators associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliation">Collaborators affiliation level with a repository.</param>
        /// <param name="login">The login of one specific collaborator.</param>
        /// <param name="query">Filters users with query on user name and login</param>
        public RepositoryCollaboratorConnection Collaborators(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<CollaboratorAffiliation>? affiliation = null, Arg<string>? login = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Collaborators(first, after, last, before, affiliation, login, query), Octokit.GraphQL.Model.RepositoryCollaboratorConnection.Create);

        /// <summary>
        /// A list of commit comments associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CommitCommentConnection CommitComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.CommitComments(first, after, last, before), Octokit.GraphQL.Model.CommitCommentConnection.Create);

        /// <summary>
        /// Returns a list of contact links associated to the repository
        /// </summary>
        public IQueryableList<RepositoryContactLink> ContactLinks => this.CreateProperty(x => x.ContactLinks);

        /// <summary>
        /// Returns the contributing guidelines for this repository.
        /// </summary>
        public ContributingGuidelines ContributingGuidelines => this.CreateProperty(x => x.ContributingGuidelines, Octokit.GraphQL.Model.ContributingGuidelines.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Ref associated with the repository's default branch.
        /// </summary>
        public Ref DefaultBranchRef => this.CreateProperty(x => x.DefaultBranchRef, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Whether or not branches are automatically deleted when merged in this repository.
        /// </summary>
        public bool DeleteBranchOnMerge { get; }

        /// <summary>
        /// A list of deploy keys that are on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeployKeyConnection DeployKeys(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.DeployKeys(first, after, last, before), Octokit.GraphQL.Model.DeployKeyConnection.Create);

        /// <summary>
        /// Deployments associated with the repository
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="environments">Environments to list deployments for</param>
        /// <param name="orderBy">Ordering options for deployments returned from the connection.</param>
        public DeploymentConnection Deployments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<string>>? environments = null, Arg<DeploymentOrder>? orderBy = null) => this.CreateMethodCall(x => x.Deployments(first, after, last, before, environments, orderBy), Octokit.GraphQL.Model.DeploymentConnection.Create);

        /// <summary>
        /// The description of the repository.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// Returns a single discussion from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the discussion to be returned.</param>
        public Discussion Discussion(Arg<int> number) => this.CreateMethodCall(x => x.Discussion(number), Octokit.GraphQL.Model.Discussion.Create);

        /// <summary>
        /// A list of discussion categories that are available in the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="filterByAssignable">Filter by categories that are assignable by the viewer.</param>
        public DiscussionCategoryConnection DiscussionCategories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? filterByAssignable = null) => this.CreateMethodCall(x => x.DiscussionCategories(first, after, last, before, filterByAssignable), Octokit.GraphQL.Model.DiscussionCategoryConnection.Create);

        /// <summary>
        /// A discussion category by slug.
        /// </summary>
        /// <param name="slug">The slug of the discussion category to be returned.</param>
        public DiscussionCategory DiscussionCategory(Arg<string> slug) => this.CreateMethodCall(x => x.DiscussionCategory(slug), Octokit.GraphQL.Model.DiscussionCategory.Create);

        /// <summary>
        /// A list of discussions that have been opened in the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="answered">Only show answered or unanswered discussions</param>
        /// <param name="categoryId">Only include discussions that belong to the category with this ID.</param>
        /// <param name="orderBy">Ordering options for discussions returned from the connection.</param>
        /// <param name="states">A list of states to filter the discussions by.</param>
        public DiscussionConnection Discussions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? answered = null, Arg<ID>? categoryId = null, Arg<DiscussionOrder>? orderBy = null, Arg<IEnumerable<DiscussionState>>? states = null) => this.CreateMethodCall(x => x.Discussions(first, after, last, before, answered, categoryId, orderBy, states), Octokit.GraphQL.Model.DiscussionConnection.Create);

        /// <summary>
        /// The number of kilobytes this repository occupies on disk.
        /// </summary>
        public int? DiskUsage { get; }

        /// <summary>
        /// Returns a single active environment from the current repository by name.
        /// </summary>
        /// <param name="name">The name of the environment to be returned.</param>
        public Environment Environment(Arg<string> name) => this.CreateMethodCall(x => x.Environment(name), Octokit.GraphQL.Model.Environment.Create);

        /// <summary>
        /// A list of environments that are in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="names">The names of the environments to be returned.</param>
        /// <param name="orderBy">Ordering options for the environments</param>
        public EnvironmentConnection Environments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<string>>? names = null, Arg<Environments>? orderBy = null) => this.CreateMethodCall(x => x.Environments(first, after, last, before, names, orderBy), Octokit.GraphQL.Model.EnvironmentConnection.Create);

        /// <summary>
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        public int ForkCount { get; }

        /// <summary>
        /// Whether this repository allows forks.
        /// </summary>
        public bool ForkingAllowed { get; }

        /// <summary>
        /// A list of direct forked repositories.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="hasIssuesEnabled">If non-null, filters repositories according to whether they have issues enabled</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy. Internal repositories are considered private; consider using the visibility argument if only internal repositories are needed. Cannot be combined with the visibility argument.</param>
        /// <param name="visibility">If non-null, filters repositories according to visibility. Cannot be combined with the privacy argument.</param>
        public RepositoryConnection Forks(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? hasIssuesEnabled = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null, Arg<RepositoryVisibility>? visibility = null) => this.CreateMethodCall(x => x.Forks(first, after, last, before, affiliations, hasIssuesEnabled, isLocked, orderBy, ownerAffiliations, privacy, visibility), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// The funding links for this repository
        /// </summary>
        public IQueryableList<FundingLink> FundingLinks => this.CreateProperty(x => x.FundingLinks);

        /// <summary>
        /// Indicates if the repository has the Discussions feature enabled.
        /// </summary>
        public bool HasDiscussionsEnabled { get; }

        /// <summary>
        /// Indicates if the repository has issues feature enabled.
        /// </summary>
        public bool HasIssuesEnabled { get; }

        /// <summary>
        /// Indicates if the repository has the Projects feature enabled.
        /// </summary>
        public bool HasProjectsEnabled { get; }

        /// <summary>
        /// Indicates if the repository displays a Sponsor button for financial contributions.
        /// </summary>
        public bool HasSponsorshipsEnabled { get; }

        /// <summary>
        /// Whether vulnerability alerts are enabled for the repository.
        /// </summary>
        public bool HasVulnerabilityAlertsEnabled { get; }

        /// <summary>
        /// Indicates if the repository has wiki feature enabled.
        /// </summary>
        public bool HasWikiEnabled { get; }

        /// <summary>
        /// The repository's URL.
        /// </summary>
        public string HomepageUrl { get; }

        /// <summary>
        /// The Node ID of the Repository object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The interaction ability settings for this repository.
        /// </summary>
        public RepositoryInteractionAbility InteractionAbility => this.CreateProperty(x => x.InteractionAbility, Octokit.GraphQL.Model.RepositoryInteractionAbility.Create);

        /// <summary>
        /// Indicates if the repository is unmaintained.
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        /// Returns true if blank issue creation is allowed
        /// </summary>
        public bool IsBlankIssuesEnabled { get; }

        /// <summary>
        /// Returns whether or not this repository disabled.
        /// </summary>
        public bool IsDisabled { get; }

        /// <summary>
        /// Returns whether or not this repository is empty.
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        public bool IsFork { get; }

        /// <summary>
        /// Indicates if a repository is either owned by an organization, or is a private fork of an organization repository.
        /// </summary>
        public bool IsInOrganization { get; }

        /// <summary>
        /// Indicates if the repository has been locked or not.
        /// </summary>
        public bool IsLocked { get; }

        /// <summary>
        /// Identifies if the repository is a mirror.
        /// </summary>
        public bool IsMirror { get; }

        /// <summary>
        /// Identifies if the repository is private or internal.
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// Returns true if this repository has a security policy
        /// </summary>
        public bool? IsSecurityPolicyEnabled { get; }

        /// <summary>
        /// Identifies if the repository is a template that can be used to generate new repositories.
        /// </summary>
        public bool IsTemplate { get; }

        /// <summary>
        /// Is this repository a user configuration repository?
        /// </summary>
        public bool IsUserConfigurationRepository { get; }

        /// <summary>
        /// Returns a single issue from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the issue to be returned.</param>
        public Issue Issue(Arg<int> number) => this.CreateMethodCall(x => x.Issue(number), Octokit.GraphQL.Model.Issue.Create);

        /// <summary>
        /// Returns a single issue-like object from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the issue to be returned.</param>
        public IssueOrPullRequest IssueOrPullRequest(Arg<int> number) => this.CreateMethodCall(x => x.IssueOrPullRequest(number), Octokit.GraphQL.Model.IssueOrPullRequest.Create);

        /// <summary>
        /// Returns a list of issue templates associated to the repository
        /// </summary>
        public IQueryableList<IssueTemplate> IssueTemplates => this.CreateProperty(x => x.IssueTemplates);

        /// <summary>
        /// A list of issues that have been opened in the repository.
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
        /// Returns a single label by name
        /// </summary>
        /// <param name="name">Label name</param>
        public Label Label(Arg<string> name) => this.CreateMethodCall(x => x.Label(name), Octokit.GraphQL.Model.Label.Create);

        /// <summary>
        /// A list of labels associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        /// <param name="query">If provided, searches labels by name and description.</param>
        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LabelOrder>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.LabelConnection.Create);

        /// <summary>
        /// A list containing a breakdown of the language composition of the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public LanguageConnection Languages(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LanguageOrder>? orderBy = null) => this.CreateMethodCall(x => x.Languages(first, after, last, before, orderBy), Octokit.GraphQL.Model.LanguageConnection.Create);

        /// <summary>
        /// Get the latest release for the repository if one exists.
        /// </summary>
        public Release LatestRelease => this.CreateProperty(x => x.LatestRelease, Octokit.GraphQL.Model.Release.Create);

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        public License LicenseInfo => this.CreateProperty(x => x.LicenseInfo, Octokit.GraphQL.Model.License.Create);

        /// <summary>
        /// The reason the repository has been locked.
        /// </summary>
        public RepositoryLockReason? LockReason { get; }

        /// <summary>
        /// A list of Users that can be mentioned in the context of the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">Filters users with query on user name and login</param>
        public UserConnection MentionableUsers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.MentionableUsers(first, after, last, before, query), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// Whether or not PRs are merged with a merge commit on this repository.
        /// </summary>
        public bool MergeCommitAllowed { get; }

        /// <summary>
        /// How the default commit message will be generated when merging a pull request.
        /// </summary>
        public MergeCommitMessage MergeCommitMessage { get; }

        /// <summary>
        /// How the default commit title will be generated when merging a pull request.
        /// </summary>
        public MergeCommitTitle MergeCommitTitle { get; }

        /// <summary>
        /// The merge queue for a specified branch, otherwise the default branch if not provided.
        /// </summary>
        /// <param name="branch">The name of the branch to get the merge queue for. Case sensitive.</param>
        public MergeQueue MergeQueue(Arg<string>? branch = null) => this.CreateMethodCall(x => x.MergeQueue(branch), Octokit.GraphQL.Model.MergeQueue.Create);

        /// <summary>
        /// Returns a single milestone from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the milestone to be returned.</param>
        public Milestone Milestone(Arg<int> number) => this.CreateMethodCall(x => x.Milestone(number), Octokit.GraphQL.Model.Milestone.Create);

        /// <summary>
        /// A list of milestones associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for milestones.</param>
        /// <param name="query">Filters milestones with a query on the title</param>
        /// <param name="states">Filter by the state of the milestones.</param>
        public MilestoneConnection Milestones(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<MilestoneOrder>? orderBy = null, Arg<string>? query = null, Arg<IEnumerable<MilestoneState>>? states = null) => this.CreateMethodCall(x => x.Milestones(first, after, last, before, orderBy, query, states), Octokit.GraphQL.Model.MilestoneConnection.Create);

        /// <summary>
        /// The repository's original mirror URL.
        /// </summary>
        public string MirrorUrl { get; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; }

        /// <summary>
        /// A Git object in the repository
        /// </summary>
        /// <param name="expression">A Git revision expression suitable for rev-parse</param>
        /// <param name="oid">The Git object ID</param>
        public IGitObject Object(Arg<string>? expression = null, Arg<string>? oid = null) => this.CreateMethodCall(x => x.Object(expression, oid), Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        /// <summary>
        /// The image used to represent this repository in Open Graph data.
        /// </summary>
        public string OpenGraphImageUrl { get; }

        /// <summary>
        /// The User owner of the repository.
        /// </summary>
        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

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
        /// The repository parent, if this is a fork.
        /// </summary>
        public Repository Parent => this.CreateProperty(x => x.Parent, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// A list of discussions that have been pinned in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PinnedDiscussionConnection PinnedDiscussions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PinnedDiscussions(first, after, last, before), Octokit.GraphQL.Model.PinnedDiscussionConnection.Create);

        /// <summary>
        /// A list of pinned issues for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PinnedIssueConnection PinnedIssues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PinnedIssues(first, after, last, before), Octokit.GraphQL.Model.PinnedIssueConnection.Create);

        /// <summary>
        /// Returns information about the availability of certain features and limits based on the repository's billing plan.
        /// </summary>
        public RepositoryPlanFeatures PlanFeatures => this.CreateProperty(x => x.PlanFeatures, Octokit.GraphQL.Model.RepositoryPlanFeatures.Create);

        /// <summary>
        /// The primary language of the repository's code.
        /// </summary>
        public Language PrimaryLanguage => this.CreateProperty(x => x.PrimaryLanguage, Octokit.GraphQL.Model.Language.Create);

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        public Project Project(Arg<int> number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// Finds and returns the Project according to the provided Project number.
        /// </summary>
        /// <param name="number">The Project number.</param>
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
        /// The HTTP path listing the repository's projects
        /// </summary>
        public string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing the repository's projects
        /// </summary>
        public string ProjectsUrl { get; }

        /// <summary>
        /// List of projects linked to this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">How to order the returned projects.</param>
        /// <param name="query">A project to search for linked to the repo.</param>
        public ProjectV2Connection ProjectsV2(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2Order>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.ProjectsV2(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        /// <summary>
        /// Returns a single pull request from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the pull request to be returned.</param>
        public PullRequest PullRequest(Arg<int> number) => this.CreateMethodCall(x => x.PullRequest(number), Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Returns a list of pull request templates associated to the repository
        /// </summary>
        public IQueryableList<PullRequestTemplate> PullRequestTemplates => this.CreateProperty(x => x.PullRequestTemplates);

        /// <summary>
        /// A list of pull requests that have been opened in the repository.
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
        /// Identifies the date and time when the repository was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; }

        /// <summary>
        /// Whether or not rebase-merging is enabled on this repository.
        /// </summary>
        public bool RebaseMergeAllowed { get; }

        /// <summary>
        /// Recent projects that this user has modified in the context of the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2Connection RecentProjects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.RecentProjects(first, after, last, before), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        /// <summary>
        /// Fetch a given ref from the repository
        /// </summary>
        /// <param name="qualifiedName">The ref to retrieve. Fully qualified matches are checked in order (`refs/heads/master`) before falling back onto checks for short name matches (`master`).</param>
        public Ref Ref(Arg<string> qualifiedName) => this.CreateMethodCall(x => x.Ref(qualifiedName), Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Fetch a list of refs from the repository
        /// </summary>
        /// <param name="refPrefix">A ref name prefix like `refs/heads/`, `refs/tags/`, etc.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="direction">DEPRECATED: use orderBy. The ordering direction.</param>
        /// <param name="orderBy">Ordering options for refs returned from the connection.</param>
        /// <param name="query">Filters refs with query on name</param>
        public RefConnection Refs(Arg<string> refPrefix, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrderDirection>? direction = null, Arg<RefOrder>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Refs(refPrefix, first, after, last, before, direction, orderBy, query), Octokit.GraphQL.Model.RefConnection.Create);

        /// <summary>
        /// Lookup a single release given various criteria.
        /// </summary>
        /// <param name="tagName">The name of the Tag the Release was created from</param>
        public Release Release(Arg<string> tagName) => this.CreateMethodCall(x => x.Release(tagName), Octokit.GraphQL.Model.Release.Create);

        /// <summary>
        /// List of releases which are dependent on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public ReleaseConnection Releases(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ReleaseOrder>? orderBy = null) => this.CreateMethodCall(x => x.Releases(first, after, last, before, orderBy), Octokit.GraphQL.Model.ReleaseConnection.Create);

        /// <summary>
        /// A list of applied repository-topic associations for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RepositoryTopicConnection RepositoryTopics(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.RepositoryTopics(first, after, last, before), Octokit.GraphQL.Model.RepositoryTopicConnection.Create);

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Returns a single ruleset from the current repository by ID.
        /// </summary>
        /// <param name="databaseId">The ID of the ruleset to be returned.</param>
        /// <param name="includeParents">Include rulesets configured at higher levels that apply to this repository</param>
        public RepositoryRuleset Ruleset(Arg<int> databaseId, Arg<bool>? includeParents = null) => this.CreateMethodCall(x => x.Ruleset(databaseId, includeParents), Octokit.GraphQL.Model.RepositoryRuleset.Create);

        /// <summary>
        /// A list of rulesets for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includeParents">Return rulesets configured at higher levels that apply to this repository</param>
        public RepositoryRulesetConnection Rulesets(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includeParents = null) => this.CreateMethodCall(x => x.Rulesets(first, after, last, before, includeParents), Octokit.GraphQL.Model.RepositoryRulesetConnection.Create);

        /// <summary>
        /// The security policy URL.
        /// </summary>
        public string SecurityPolicyUrl { get; }

        /// <summary>
        /// A description of the repository, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        public string ShortDescriptionHTML(Arg<int>? limit = null) => default;

        /// <summary>
        /// Whether or not squash-merging is enabled on this repository.
        /// </summary>
        public bool SquashMergeAllowed { get; }

        /// <summary>
        /// How the default commit message will be generated when squash merging a pull request.
        /// </summary>
        public SquashMergeCommitMessage SquashMergeCommitMessage { get; }

        /// <summary>
        /// How the default commit title will be generated when squash merging a pull request.
        /// </summary>
        public SquashMergeCommitTitle SquashMergeCommitTitle { get; }

        /// <summary>
        /// Whether a squash merge commit can use the pull request title as default.
        /// </summary>
        [Obsolete(@"`squashPrTitleUsedAsDefault` will be removed. Use `Repository.squashMergeCommitTitle` instead. Removal on 2023-04-01 UTC.")]
        public bool SquashPrTitleUsedAsDefault { get; }

        /// <summary>
        /// The SSH URL to clone this repository
        /// </summary>
        public string SshUrl { get; }

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        public int StargazerCount { get; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null) => this.CreateMethodCall(x => x.Stargazers(first, after, last, before, orderBy), Octokit.GraphQL.Model.StargazerConnection.Create);

        /// <summary>
        /// Returns a list of all submodules in this repository parsed from the .gitmodules file as of the default branch's HEAD commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public SubmoduleConnection Submodules(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Submodules(first, after, last, before), Octokit.GraphQL.Model.SubmoduleConnection.Create);

        /// <summary>
        /// Temporary authentication token for cloning this repository.
        /// </summary>
        public string TempCloneToken { get; }

        /// <summary>
        /// The repository from which this repository was generated, if any.
        /// </summary>
        public Repository TemplateRepository => this.CreateProperty(x => x.TemplateRepository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Whether this repository has a custom image to use with Open Graph as opposed to being represented by the owner's avatar.
        /// </summary>
        public bool UsesCustomOpenGraphImage { get; }

        /// <summary>
        /// Indicates whether the viewer has admin permissions on this repository.
        /// </summary>
        public bool ViewerCanAdminister { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Indicates whether the viewer can update the topics of this repository.
        /// </summary>
        public bool ViewerCanUpdateTopics { get; }

        /// <summary>
        /// The last commit email for the viewer.
        /// </summary>
        public string ViewerDefaultCommitEmail { get; }

        /// <summary>
        /// The last used merge method by the viewer or the default for the repository.
        /// </summary>
        public PullRequestMergeMethod ViewerDefaultMergeMethod { get; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; }

        /// <summary>
        /// The users permission level on the repository. Will return null if authenticated as an GitHub App.
        /// </summary>
        public RepositoryPermission? ViewerPermission { get; }

        /// <summary>
        /// A list of emails this viewer can commit with.
        /// </summary>
        public IEnumerable<string> ViewerPossibleCommitEmails { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; }

        /// <summary>
        /// Indicates the repository's visibility level.
        /// </summary>
        public RepositoryVisibility Visibility { get; }

        /// <summary>
        /// Returns a single vulnerability alert from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the vulnerability alert to be returned.</param>
        public RepositoryVulnerabilityAlert VulnerabilityAlert(Arg<int> number) => this.CreateMethodCall(x => x.VulnerabilityAlert(number), Octokit.GraphQL.Model.RepositoryVulnerabilityAlert.Create);

        /// <summary>
        /// A list of vulnerability alerts that are on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="dependencyScopes">Filter by the scope of the alert's dependency</param>
        /// <param name="states">Filter by the state of the alert</param>
        public RepositoryVulnerabilityAlertConnection VulnerabilityAlerts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryVulnerabilityAlertDependencyScope>>? dependencyScopes = null, Arg<IEnumerable<RepositoryVulnerabilityAlertState>>? states = null) => this.CreateMethodCall(x => x.VulnerabilityAlerts(first, after, last, before, dependencyScopes, states), Octokit.GraphQL.Model.RepositoryVulnerabilityAlertConnection.Create);

        /// <summary>
        /// A list of users watching the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Watchers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Watchers(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// Whether contributors are required to sign off on web-based commits in this repository.
        /// </summary>
        public bool WebCommitSignoffRequired { get; }

        internal static Repository Create(Expression expression)
        {
            return new Repository(expression);
        }
    }
}