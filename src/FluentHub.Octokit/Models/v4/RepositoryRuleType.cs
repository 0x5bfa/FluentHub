// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The rule types supported in rulesets
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryRuleType
	{
		/// <summary>
		/// Only allow users with bypass permission to create matching refs.
		/// </summary>
		[EnumMember(Value = "CREATION")]
		Creation,

		/// <summary>
		/// Only allow users with bypass permission to update matching refs.
		/// </summary>
		[EnumMember(Value = "UPDATE")]
		Update,

		/// <summary>
		/// Only allow users with bypass permissions to delete matching refs.
		/// </summary>
		[EnumMember(Value = "DELETION")]
		Deletion,

		/// <summary>
		/// Prevent merge commits from being pushed to matching branches.
		/// </summary>
		[EnumMember(Value = "REQUIRED_LINEAR_HISTORY")]
		RequiredLinearHistory,

		/// <summary>
		/// Choose which environments must be successfully deployed to before branches can be merged into a branch that matches this rule.
		/// </summary>
		[EnumMember(Value = "REQUIRED_DEPLOYMENTS")]
		RequiredDeployments,

		/// <summary>
		/// Commits pushed to matching branches must have verified signatures.
		/// </summary>
		[EnumMember(Value = "REQUIRED_SIGNATURES")]
		RequiredSignatures,

		/// <summary>
		/// Require all commits be made to a non-target branch and submitted via a pull request before they can be merged.
		/// </summary>
		[EnumMember(Value = "PULL_REQUEST")]
		PullRequest,

		/// <summary>
		/// Choose which status checks must pass before branches can be merged into a branch that matches this rule. When enabled, commits must first be pushed to another branch, then merged or pushed directly to a branch that matches this rule after status checks have passed.
		/// </summary>
		[EnumMember(Value = "REQUIRED_STATUS_CHECKS")]
		RequiredStatusChecks,

		/// <summary>
		/// Prevent users with push access from force pushing to branches.
		/// </summary>
		[EnumMember(Value = "NON_FAST_FORWARD")]
		NonFastForward,

		/// <summary>
		/// Commit message pattern
		/// </summary>
		[EnumMember(Value = "COMMIT_MESSAGE_PATTERN")]
		CommitMessagePattern,

		/// <summary>
		/// Commit author email pattern
		/// </summary>
		[EnumMember(Value = "COMMIT_AUTHOR_EMAIL_PATTERN")]
		CommitAuthorEmailPattern,

		/// <summary>
		/// Committer email pattern
		/// </summary>
		[EnumMember(Value = "COMMITTER_EMAIL_PATTERN")]
		CommitterEmailPattern,

		/// <summary>
		/// Branch name pattern
		/// </summary>
		[EnumMember(Value = "BRANCH_NAME_PATTERN")]
		BranchNamePattern,

		/// <summary>
		/// Tag name pattern
		/// </summary>
		[EnumMember(Value = "TAG_NAME_PATTERN")]
		TagNamePattern,
	}
}
