// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The reason a repository is listed as 'contributed'.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryContributionType
	{
		/// <summary>
		/// Created a commit
		/// </summary>
		[EnumMember(Value = "COMMIT")]
		Commit,

		/// <summary>
		/// Created an issue
		/// </summary>
		[EnumMember(Value = "ISSUE")]
		Issue,

		/// <summary>
		/// Created a pull request
		/// </summary>
		[EnumMember(Value = "PULL_REQUEST")]
		PullRequest,

		/// <summary>
		/// Created the repository
		/// </summary>
		[EnumMember(Value = "REPOSITORY")]
		Repository,

		/// <summary>
		/// Reviewed a pull request
		/// </summary>
		[EnumMember(Value = "PULL_REQUEST_REVIEW")]
		PullRequestReview,
	}
}
