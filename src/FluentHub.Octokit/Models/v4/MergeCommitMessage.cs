// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible default commit messages for merges.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MergeCommitMessage
	{
		/// <summary>
		/// Default to the pull request's title.
		/// </summary>
		[EnumMember(Value = "PR_TITLE")]
		PrTitle,

		/// <summary>
		/// Default to the pull request's body.
		/// </summary>
		[EnumMember(Value = "PR_BODY")]
		PrBody,

		/// <summary>
		/// Default to a blank commit message.
		/// </summary>
		[EnumMember(Value = "BLANK")]
		Blank,
	}
}
