// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a tracked issue.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TrackedIssueStates
	{
		/// <summary>
		/// The tracked issue is open
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// The tracked issue is closed
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,
	}
}
