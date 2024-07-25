// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible default commit titles for merges.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MergeCommitTitle
	{
		/// <summary>
		/// Default to the pull request's title.
		/// </summary>
		[EnumMember(Value = "PR_TITLE")]
		PrTitle,

		/// <summary>
		/// Default to the classic title for a merge message (e.g., Merge pull request #123 from branch-name).
		/// </summary>
		[EnumMember(Value = "MERGE_MESSAGE")]
		MergeMessage,
	}
}
