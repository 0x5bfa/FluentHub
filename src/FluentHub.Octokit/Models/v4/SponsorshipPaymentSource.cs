// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// How payment was made for funding a GitHub Sponsors sponsorship.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SponsorshipPaymentSource
	{
		/// <summary>
		/// Payment was made through GitHub.
		/// </summary>
		[EnumMember(Value = "GITHUB")]
		Github,

		/// <summary>
		/// Payment was made through Patreon.
		/// </summary>
		[EnumMember(Value = "PATREON")]
		Patreon,
	}
}
