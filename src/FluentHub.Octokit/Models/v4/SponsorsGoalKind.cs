// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// The different kinds of goals a GitHub Sponsors member can have.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorsGoalKind
	{
		/// <summary>
		/// The goal is about reaching a certain number of sponsors.
		/// </summary>
		[EnumMember(Value = "TOTAL_SPONSORS_COUNT")]
		TotalSponsorsCount,

		/// <summary>
		/// The goal is about getting a certain amount in USD from sponsorships each month.
		/// </summary>
		[EnumMember(Value = "MONTHLY_SPONSORSHIP_AMOUNT")]
		MonthlySponsorshipAmount,
	}
}
