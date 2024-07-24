// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which team discussion comment connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TeamDiscussionCommentOrderField
	{
		/// <summary>
		/// Allows sequential ordering of team discussion comments (which is equivalent to chronological ordering).
		/// </summary>
		[EnumMember(Value = "NUMBER")]
		Number,
	}
}
