namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents either a issue the viewer can access or a restricted contribution.
    /// </summary>
    public class CreatedIssueOrRestrictedContribution
    {
        /// <summary>
        /// Represents the contribution a user made on GitHub by opening an issue.
        /// </summary>
        public CreatedIssueContribution CreatedIssueContribution { get; set; }

        /// <summary>
        /// Represents a private contribution a user made on GitHub.
        /// </summary>
        public RestrictedContribution RestrictedContribution { get; set; }
    }
}