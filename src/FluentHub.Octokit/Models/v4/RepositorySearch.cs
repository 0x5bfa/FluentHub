namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A summary of a repository.
    /// </summary>
    public class RepositorySearch
    {
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
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        public int ForkCount { get; set; }

        /// <summary>
        /// Indicates if the repository is unmaintained.
        /// </summary>
        public bool IsArchived { get; set; }
        

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        public bool IsFork { get; set; }

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
        /// The license associated with the repository
        /// </summary>
        public String LicenseInfo { get; set; }
        
        /// <summary>
        /// The name of the repository.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; set; }
        
        /// <summary>
        /// The User owner of the repository.
        /// </summary>
        public string Owner { get; set; }
        
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
        /// Humanized string of "Identifies the date and time when the object was last updated."
        /// <summary>
        public string UpdatedAtHumanized { get; set; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        public string Url { get; set; }
    }
}