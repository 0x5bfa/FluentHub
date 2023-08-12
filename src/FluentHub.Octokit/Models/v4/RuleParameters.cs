// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
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
		/// Require all commits be made to a non-target branch and submitted via a pull request before they can be merged.
		/// </summary>
		public PullRequestParameters PullRequestParameters { get; set; }

		/// <summary>
		/// Choose which environments must be successfully deployed to before branches can be merged into a branch that matches this rule.
		/// </summary>
		public RequiredDeploymentsParameters RequiredDeploymentsParameters { get; set; }

		/// <summary>
		/// Choose which status checks must pass before branches can be merged into a branch that matches this rule. When enabled, commits must first be pushed to another branch, then merged or pushed directly to a branch that matches this rule after status checks have passed.
		/// </summary>
		public RequiredStatusChecksParameters RequiredStatusChecksParameters { get; set; }

		/// <summary>
		/// Parameters to be used for the tag_name_pattern rule
		/// </summary>
		public TagNamePatternParameters TagNamePatternParameters { get; set; }

		/// <summary>
		/// Only allow users with bypass permission to update matching refs.
		/// </summary>
		public UpdateParameters UpdateParameters { get; set; }
	}
}
