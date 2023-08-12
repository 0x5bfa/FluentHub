// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a pull request review comment.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PullRequestReviewCommentState
	{
		/// <summary>
		/// A comment that is part of a pending review
		/// </summary>
		[EnumMember(Value = "PENDING")]
		Pending,

		/// <summary>
		/// A comment that is part of a submitted review
		/// </summary>
		[EnumMember(Value = "SUBMITTED")]
		Submitted,
	}
}
