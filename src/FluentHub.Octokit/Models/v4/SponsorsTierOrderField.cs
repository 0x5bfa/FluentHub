// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which Sponsors tiers connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorsTierOrderField
	{
		/// <summary>
		/// Order tiers by creation time.
		/// </summary>
		[EnumMember(Value = "CREATED_AT")]
		CreatedAt,

		/// <summary>
		/// Order tiers by their monthly price in cents
		/// </summary>
		[EnumMember(Value = "MONTHLY_PRICE_IN_CENTS")]
		MonthlyPriceInCents,
	}
}
