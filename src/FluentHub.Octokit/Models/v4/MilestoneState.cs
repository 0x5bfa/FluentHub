// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible states of a milestone.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MilestoneState
	{
		/// <summary>
		/// A milestone that is still open.
		/// </summary>
		[EnumMember(Value = "OPEN")]
		Open,

		/// <summary>
		/// A milestone that has been closed.
		/// </summary>
		[EnumMember(Value = "CLOSED")]
		Closed,
	}
}
