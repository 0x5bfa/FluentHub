// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible state reasons of an issue.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IssueStateReason
	{
		/// <summary>
		/// An issue that has been reopened
		/// </summary>
		[EnumMember(Value = "REOPENED")]
		Reopened,

		/// <summary>
		/// An issue that has been closed as not planned
		/// </summary>
		[EnumMember(Value = "NOT_PLANNED")]
		NotPlanned,

		/// <summary>
		/// An issue that has been closed as completed
		/// </summary>
		[EnumMember(Value = "COMPLETED")]
		Completed,
	}
}
