// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub account and the total amount in USD they've paid for sponsorships to a particular maintainer. Does not include payments made via Patreon.
	/// </summary>
	public class SponsorAndLifetimeValue
	{
		/// <summary>
		/// The amount in cents.
		/// </summary>
		public int AmountInCents { get; set; }

		/// <summary>
		/// The amount in USD, formatted as a string.
		/// </summary>
		public string FormattedAmount { get; set; }

		/// <summary>
		/// The sponsor's GitHub account.
		/// </summary>
		public ISponsorable Sponsor { get; set; }

		/// <summary>
		/// The maintainer's GitHub account.
		/// </summary>
		public ISponsorable Sponsorable { get; set; }
	}
}
