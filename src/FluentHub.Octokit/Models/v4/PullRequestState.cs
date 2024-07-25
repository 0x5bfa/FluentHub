// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a pull request.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PullRequestState
	{
		/// <summary>
		/// A pull request that is still open.
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// A pull request that has been closed without being merged.
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,

		/// <summary>
		/// A pull request that has been closed by being merged.
		/// </summary>
		[EnumMember(Value = "MERGED")]
		Merged,
	}
}
