// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible subject types of a pull request review comment.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PullRequestReviewThreadSubjectType
	{
		/// <summary>
		/// A comment that has been made against the line of a pull request
		/// </summary>
		[EnumMember(Value = "LINE")]
		Line,

		/// <summary>
		/// A comment that has been made against the file of a pull request
		/// </summary>
		[EnumMember(Value = "FILE")]
		File,
	}
}
