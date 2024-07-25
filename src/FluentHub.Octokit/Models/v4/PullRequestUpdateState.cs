// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible target states when updating a pull request.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PullRequestUpdateState
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
	}
}
