// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The possible durations that a user can be blocked for.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserBlockDuration
	{
		/// <summary>
		/// The user was blocked for 1 day
		/// </summary>
		[EnumMember(Value = "ONE_DAY")]
		OneDay,

		/// <summary>
		/// The user was blocked for 3 days
		/// </summary>
		[EnumMember(Value = "THREE_DAYS")]
		ThreeDays,

		/// <summary>
		/// The user was blocked for 7 days
		/// </summary>
		[EnumMember(Value = "ONE_WEEK")]
		OneWeek,

		/// <summary>
		/// The user was blocked for 30 days
		/// </summary>
		[EnumMember(Value = "ONE_MONTH")]
		OneMonth,

		/// <summary>
		/// The user was blocked permanently
		/// </summary>
		[EnumMember(Value = "PERMANENT")]
		Permanent,
	}
}
