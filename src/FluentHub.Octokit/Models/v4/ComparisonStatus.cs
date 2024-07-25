// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The status of a git comparison between two refs.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ComparisonStatus
	{
		/// <summary>
		/// The head ref is both ahead and behind of the base ref, indicating git history has diverged.
		/// </summary>
		[EnumMember(Value = "DIVERGED")]
		Diverged,

		/// <summary>
		/// The head ref is ahead of the base ref.
		/// </summary>
		[EnumMember(Value = "AHEAD")]
		Ahead,

		/// <summary>
		/// The head ref is behind the base ref.
		/// </summary>
		[EnumMember(Value = "BEHIND")]
		Behind,

		/// <summary>
		/// The head ref and base ref are identical.
		/// </summary>
		[EnumMember(Value = "IDENTICAL")]
		Identical,
	}
}
