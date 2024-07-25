// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of an issue.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IssueState
	{
		/// <summary>
		/// An issue that is still open
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// An issue that has been closed
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,
	}
}
