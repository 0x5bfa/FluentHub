// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible methods for updating a pull request's head branch with the base branch.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PullRequestBranchUpdateMethod
	{
		/// <summary>
		/// Update branch via merge
		/// </summary>
		[EnumMember(Value = "MERGE")]
		Merge,

		/// <summary>
		/// Update branch via rebase
		/// </summary>
		[EnumMember(Value = "REBASE")]
		Rebase,
	}
}
