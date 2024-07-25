// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible default commit messages for squash merges.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SquashMergeCommitMessage
	{
		/// <summary>
		/// Default to the pull request's body.
		/// </summary>
		[EnumMember(Value = "PR_BODY")]
		PrBody,

		/// <summary>
		/// Default to the branch's commit messages.
		/// </summary>
		[EnumMember(Value = "COMMIT_MESSAGES")]
		CommitMessages,

		/// <summary>
		/// Default to a blank commit message.
		/// </summary>
		[EnumMember(Value = "BLANK")]
		Blank,
	}
}
