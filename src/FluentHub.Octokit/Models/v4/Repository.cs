namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A repository contains the content for a project.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Whether or not a pull request head branch that is behind its base branch can always be updated even if it is not required to be up to date before merging.
        /// </summary>
        public bool AllowUpdateBranch { get; set; }

        /// <summary>
        /// A list of users that can be assigned to issues in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">Filters users with query on user name and login</param>
        public UserConnection AssignableUsers { get; set; }

        /// <summary>
        /// Whether or not Auto-merge can be enabled on pull requests in this repository.
        /// </summary>
        public bool AutoMergeAllowed { get; set; }

        /// <summary>
        /// A list of branch protection rules for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BranchProtectionRuleConnection BranchProtectionRules { get; set; }

        /// <summary>
        /// Returns the code of conduct for this repository
        /// </summary>
        public CodeOfConduct CodeOfConduct { get; set; }

        /// <summary>
        /// Information extracted from the repository's `CODEOWNERS` file.
        /// </summary>
        /// <param name="refName">The ref name used to return the associated `CODEOWNERS` file.</param>
        public RepositoryCodeowners Codeowners { get; set; }

        /// <summary>
        /// A list of collaborators associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliation">Collaborators affiliation level with a repository.</param>
        /// <param name="query">Filters users with query on user name and login</param>
        public RepositoryCollaboratorConnection Collaborators { get; set; }

        /// <summary>
        /// A list of commit comments associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CommitCommentConnection CommitComments { get; set; }

        /// <summary>
        /// Returns a list of contact links associated to the repository
        /// </summary>
        public List<RepositoryContactLink> ContactLinks { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// The Ref associated with the repository's default branch.
        /// </summary>
        public Ref DefaultBranchRef { get; set; }

        /// <summary>
        /// Whether or not branches are automatically deleted when merged in this repository.
        /// </summary>
        public bool DeleteBranchOnMerge { get; set; }

        /// <summary>
        /// A list of deploy keys that are on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeployKeyConnection DeployKeys { get; set; }

        /// <summary>
        /// Deployments associated with the repository
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="environments">Environments to list deployments for</param>
        /// <param name="orderBy">Ordering options for deployments returned from the connection.</param>
        public DeploymentConnection Deployments { get; set; }

        /// <summary>
        /// The description of the repository.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        public string DescriptionHTML { get; set; }

        /// <summary>
        /// Returns a single discussion from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the discussion to be returned.</param>
        public Discussion Discussion { get; set; }

        /// <summary>
        /// A list of discussion categories that are available in the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="filterByAssignable">Filter by categories that are assignable by the viewer.</param>
        public DiscussionCategoryConnection DiscussionCategories { get; set; }

        /// <summary>
        /// A list of discussions that have been opened in the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="categoryId">Only include discussions that belong to the category with this ID.</param>
        /// <param name="orderBy">Ordering options for discussions returned from the connection.</param>
        public DiscussionConnection Discussions { get; set; }

        /// <summary>
        /// The number of kilobytes this repository occupies on disk.
        /// </summary>
        public int? DiskUsage { get; set; }

        /// <summary>
        /// Returns a single active environment from the current repository by name.
        /// </summary>
        /// <param name="name">The name of the environment to be returned.</param>
        public Environment Environment { get; set; }

        /// <summary>
        /// A list of environments that are in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public EnvironmentConnection Environments { get; set; }

        /// <summary>
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        public int ForkCount { get; set; }

        /// <summary>
        /// Whether this repository allows forks.
        /// </summary>
        public bool ForkingAllowed { get; set; }

        /// <summary>
        /// A list of direct forked repositories.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        public RepositoryConnection Forks { get; set; }

        /// <summary>
        /// The funding links for this repository
        /// </summary>
        public List<FundingLink> FundingLinks { get; set; }

        /// <summary>
        /// Indicates if the repository has issues feature enabled.
        /// </summary>
        public bool HasIssuesEnabled { get; set; }

        /// <summary>
        /// Indicates if the repository has the Projects feature enabled.
        /// </summary>
        public bool HasProjectsEnabled { get; set; }

        /// <summary>
        /// Indicates if the repository has wiki feature enabled.
        /// </summary>
        public bool HasWikiEnabled { get; set; }

        /// <summary>
        /// The repository's URL.
        /// </summary>
        public string HomepageUrl { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The interaction ability settings for this repository.
        /// </summary>
        public RepositoryInteractionAbility InteractionAbility { get; set; }

        /// <summary>
        /// Indicates if the repository is unmaintained.
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Returns true if blank issue creation is allowed
        /// </summary>
        public bool IsBlankIssuesEnabled { get; set; }

        /// <summary>
        /// Returns whether or not this repository disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Returns whether or not this repository is empty.
        /// </summary>
        public bool IsEmpty { get; set; }

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        public bool IsFork { get; set; }

        /// <summary>
        /// Indicates if a repository is either owned by an organization, or is a private fork of an organization repository.
        /// </summary>
        public bool IsInOrganization { get; set; }

        /// <summary>
        /// Indicates if the repository has been locked or not.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Identifies if the repository is a mirror.
        /// </summary>
        public bool IsMirror { get; set; }

        /// <summary>
        /// Identifies if the repository is private or internal.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Returns true if this repository has a security policy
        /// </summary>
        public bool? IsSecurityPolicyEnabled { get; set; }

        /// <summary>
        /// Identifies if the repository is a template that can be used to generate new repositories.
        /// </summary>
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Is this repository a user configuration repository?
        /// </summary>
        public bool IsUserConfigurationRepository { get; set; }

        /// <summary>
        /// Returns a single issue from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the issue to be returned.</param>
        public Issue Issue { get; set; }

        /// <summary>
        /// Returns a single issue-like object from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the issue to be returned.</param>
        public IssueOrPullRequest IssueOrPullRequest { get; set; }

        /// <summary>
        /// Returns a list of issue templates associated to the repository
        /// </summary>
        public List<IssueTemplate> IssueTemplates { get; set; }

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
        public IssueConnection Issues { get; set; }

        /// <summary>
        /// Returns a single label by name
        /// </summary>
        /// <param name="name">Label name</param>
        public Label Label { get; set; }

        /// <summary>
        /// A list of labels associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for labels returned from the connection.</param>
        /// <param name="query">If provided, searches labels by name and description.</param>
        public LabelConnection Labels { get; set; }

        /// <summary>
        /// A list containing a breakdown of the language composition of the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public LanguageConnection Languages { get; set; }

        /// <summary>
        /// Get the latest release for the repository if one exists.
        /// </summary>
        public Release LatestRelease { get; set; }

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        public License LicenseInfo { get; set; }

        /// <summary>
        /// The reason the repository has been locked.
        /// </summary>
        public RepositoryLockReason? LockReason { get; set; }

        /// <summary>
        /// A list of Users that can be mentioned in the context of the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">Filters users with query on user name and login</param>
        public UserConnection MentionableUsers { get; set; }

        /// <summary>
        /// Whether or not PRs are merged with a merge commit on this repository.
        /// </summary>
        public bool MergeCommitAllowed { get; set; }

        /// <summary>
        /// Returns a single milestone from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the milestone to be returned.</param>
        public Milestone Milestone { get; set; }

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
        public MilestoneConnection Milestones { get; set; }

        /// <summary>
        /// The repository's original mirror URL.
        /// </summary>
        public string MirrorUrl { get; set; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; set; }

        /// <summary>
        /// A Git object in the repository
        /// </summary>
        /// <param name="expression">A Git revision expression suitable for rev-parse</param>
        /// <param name="oid">The Git object ID</param>
        public IGitObject Object { get; set; }

        /// <summary>
        /// The image used to represent this repository in Open Graph data.
        /// </summary>
        public string OpenGraphImageUrl { get; set; }

        /// <summary>
        /// The User owner of the repository.
        /// </summary>
        public IRepositoryOwner Owner { get; set; }

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
        public PackageConnection Packages { get; set; }

        /// <summary>
        /// The repository parent, if this is a fork.
        /// </summary>
        public Repository Parent { get; set; }

        /// <summary>
        /// A list of discussions that have been pinned in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PinnedDiscussionConnection PinnedDiscussions { get; set; }

        /// <summary>
        /// A list of pinned issues for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PinnedIssueConnection PinnedIssues { get; set; }

        /// <summary>
        /// The primary language of the repository's code.
        /// </summary>
        public Language PrimaryLanguage { get; set; }

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        public Project Project { get; set; }

        /// <summary>
        /// Finds and returns the Project (beta) according to the provided Project (beta) number.
        /// </summary>
        /// <param name="number">The ProjectNext number.</param>
        public ProjectNext ProjectNext { get; set; }

        /// <summary>
        /// Finds and returns the Project according to the provided Project number.
        /// </summary>
        /// <param name="number">The Project number.</param>
        public ProjectV2 ProjectV2 { get; set; }

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
        public ProjectConnection Projects { get; set; }

        /// <summary>
        /// List of projects (beta) linked to this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">A project (beta) to search for linked to the repo.</param>
        /// <param name="sortBy">How to order the returned project (beta) objects.</param>
        public ProjectNextConnection ProjectsNext { get; set; }

        /// <summary>
        /// The HTTP path listing the repository's projects
        /// </summary>
        public string ProjectsResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL listing the repository's projects
        /// </summary>
        public string ProjectsUrl { get; set; }

        /// <summary>
        /// List of projects linked to this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">How to order the returned projects.</param>
        /// <param name="query">A project to search for linked to the repo.</param>
        public ProjectV2Connection ProjectsV2 { get; set; }

        /// <summary>
        /// Returns a single pull request from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the pull request to be returned.</param>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// Returns a list of pull request templates associated to the repository
        /// </summary>
        public List<PullRequestTemplate> PullRequestTemplates { get; set; }

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
        public PullRequestConnection PullRequests { get; set; }

        /// <summary>
        /// Identifies when the repository was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; set; }

        /// <summary>
        /// Whether or not rebase-merging is enabled on this repository.
        /// </summary>
        public bool RebaseMergeAllowed { get; set; }

        /// <summary>
        /// Recent projects that this user has modified in the context of the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2Connection RecentProjects { get; set; }

        /// <summary>
        /// Fetch a given ref from the repository
        /// </summary>
        /// <param name="qualifiedName">The ref to retrieve. Fully qualified matches are checked in order (`refs/heads/master`) before falling back onto checks for short name matches (`master`).</param>
        public Ref Ref { get; set; }

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
        public RefConnection Refs { get; set; }

        /// <summary>
        /// Lookup a single release given various criteria.
        /// </summary>
        /// <param name="tagName">The name of the Tag the Release was created from</param>
        public Release Release { get; set; }

        /// <summary>
        /// List of releases which are dependent on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public ReleaseConnection Releases { get; set; }

        /// <summary>
        /// A list of applied repository-topic associations for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RepositoryTopicConnection RepositoryTopics { get; set; }

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The security policy URL.
        /// </summary>
        public string SecurityPolicyUrl { get; set; }

        /// <summary>
        /// A description of the repository, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        public string ShortDescriptionHTML { get; set; }

        /// <summary>
        /// Whether or not squash-merging is enabled on this repository.
        /// </summary>
        public bool SquashMergeAllowed { get; set; }

        /// <summary>
        /// Whether a squash merge commit can use the pull request title as default.
        /// </summary>
        public bool SquashPrTitleUsedAsDefault { get; set; }

        /// <summary>
        /// The SSH URL to clone this repository
        /// </summary>
        public string SshUrl { get; set; }

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        public int StargazerCount { get; set; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers { get; set; }

        /// <summary>
        /// Returns a list of all submodules in this repository parsed from the .gitmodules file as of the default branch's HEAD commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public SubmoduleConnection Submodules { get; set; }

        /// <summary>
        /// Temporary authentication token for cloning this repository.
        /// </summary>
        public string TempCloneToken { get; set; }

        /// <summary>
        /// The repository from which this repository was generated, if any.
        /// </summary>
        public Repository TemplateRepository { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Whether this repository has a custom image to use with Open Graph as opposed to being represented by the owner's avatar.
        /// </summary>
        public bool UsesCustomOpenGraphImage { get; set; }

        /// <summary>
        /// Indicates whether the viewer has admin permissions on this repository.
        /// </summary>
        public bool ViewerCanAdminister { get; set; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; set; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; set; }

        /// <summary>
        /// Indicates whether the viewer can update the topics of this repository.
        /// </summary>
        public bool ViewerCanUpdateTopics { get; set; }

        /// <summary>
        /// The last commit email for the viewer.
        /// </summary>
        public string ViewerDefaultCommitEmail { get; set; }

        /// <summary>
        /// The last used merge method by the viewer or the default for the repository.
        /// </summary>
        public PullRequestMergeMethod ViewerDefaultMergeMethod { get; set; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; set; }

        /// <summary>
        /// The users permission level on the repository. Will return null if authenticated as an GitHub App.
        /// </summary>
        public RepositoryPermission? ViewerPermission { get; set; }

        /// <summary>
        /// A list of emails this viewer can commit with.
        /// </summary>
        public List<string> ViewerPossibleCommitEmails { get; set; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; set; }

        /// <summary>
        /// Indicates the repository's visibility level.
        /// </summary>
        public RepositoryVisibility Visibility { get; set; }

        /// <summary>
        /// A list of vulnerability alerts that are on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="dependencyScopes">Filter by the scope of the alert's dependency</param>
        /// <param name="states">Filter by the state of the alert</param>
        public RepositoryVulnerabilityAlertConnection VulnerabilityAlerts { get; set; }

        /// <summary>
        /// A list of users watching the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Watchers { get; set; }
    }
}