// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Properties by which sponsor and lifetime value connections can be ordered.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorAndLifetimeValueOrderField
	{
		/// <summary>
		/// Order results by the sponsor's login (username).
		/// </summary>
		[EnumMember(Value = "SPONSOR_LOGIN")]
		SponsorLogin,

		/// <summary>
		/// Order results by the sponsor's relevance to the viewer.
		/// </summary>
		[EnumMember(Value = "SPONSOR_RELEVANCE")]
		SponsorRelevance,

		/// <summary>
		/// Order results by how much money the sponsor has paid in total.
		/// </summary>
		[EnumMember(Value = "LIFETIME_VALUE")]
		LifetimeValue,
	}
}
