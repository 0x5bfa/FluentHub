// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The length for a repository interaction limit to be enabled for.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RepositoryInteractionLimitExpiry
	{
		/// <summary>
		/// The interaction limit will expire after 1 day.
		/// </summary>
		[EnumMember(Value = "ONE_DAY")]
		OneDay,

		/// <summary>
		/// The interaction limit will expire after 3 days.
		/// </summary>
		[EnumMember(Value = "THREE_DAYS")]
		ThreeDays,

		/// <summary>
		/// The interaction limit will expire after 1 week.
		/// </summary>
		[EnumMember(Value = "ONE_WEEK")]
		OneWeek,

		/// <summary>
		/// The interaction limit will expire after 1 month.
		/// </summary>
		[EnumMember(Value = "ONE_MONTH")]
		OneMonth,

		/// <summary>
		/// The interaction limit will expire after 6 months.
		/// </summary>
		[EnumMember(Value = "SIX_MONTHS")]
		SixMonths,
	}
}
