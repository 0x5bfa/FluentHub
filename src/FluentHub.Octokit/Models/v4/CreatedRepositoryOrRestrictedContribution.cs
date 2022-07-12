namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents either a repository the viewer can access or a restricted contribution.
    /// </summary>
    public class CreatedRepositoryOrRestrictedContribution
    {
        
        /// <summary>
        /// Represents the contribution a user made on GitHub by creating a repository.
        /// </summary>
            public CreatedRepositoryContribution CreatedRepositoryContribution { get; set; }

        /// <summary>
        /// Represents a private contribution a user made on GitHub.
        /// </summary>
            public RestrictedContribution RestrictedContribution { get; set; }
    }
}