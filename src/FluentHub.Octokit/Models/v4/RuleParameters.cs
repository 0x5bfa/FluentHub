namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Types which can be parameters for `RepositoryRule` objects.
    /// </summary>
    public class RuleParameters
    {
        /// <summary>
        /// Parameters to be used for the branch_name_pattern rule
        /// </summary>
        public BranchNamePatternParameters BranchNamePatternParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the commit_author_email_pattern rule
        /// </summary>
        public CommitAuthorEmailPatternParameters CommitAuthorEmailPatternParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the commit_message_pattern rule
        /// </summary>
        public CommitMessagePatternParameters CommitMessagePatternParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the committer_email_pattern rule
        /// </summary>
        public CommitterEmailPatternParameters CommitterEmailPatternParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the pull_request rule
        /// </summary>
        public PullRequestParameters PullRequestParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the required_deployments rule
        /// </summary>
        public RequiredDeploymentsParameters RequiredDeploymentsParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the required_status_checks rule
        /// </summary>
        public RequiredStatusChecksParameters RequiredStatusChecksParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the tag_name_pattern rule
        /// </summary>
        public TagNamePatternParameters TagNamePatternParameters { get; set; }

        /// <summary>
        /// Parameters to be used for the update rule
        /// </summary>
        public UpdateParameters UpdateParameters { get; set; }
    }
}