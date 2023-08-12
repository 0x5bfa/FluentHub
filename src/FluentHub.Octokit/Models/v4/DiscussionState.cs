// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a discussion.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DiscussionState
	{
		/// <summary>
		/// A discussion that is open
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// A discussion that has been closed
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,
	}
}
