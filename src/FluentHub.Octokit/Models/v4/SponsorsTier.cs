// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub Sponsors tier associated with a GitHub Sponsors listing.
	/// </summary>
	public class SponsorsTier
	{
		/// <summary>
		/// SponsorsTier information only visible to users that can administer the associated Sponsors listing.
		/// </summary>
		public SponsorsTierAdminInfo AdminInfo { get; set; }

		/// <summary>
		/// Get a different tier for this tier's maintainer that is at the same frequency as this tier but with an equal or lesser cost. Returns the published tier with the monthly price closest to this tier's without going over.
		/// </summary>
		public SponsorsTier ClosestLesserValueTier { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The description of the tier.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The tier description rendered to HTML
		/// </summary>
		public string DescriptionHTML { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether this tier was chosen at checkout time by the sponsor rather than defined ahead of time by the maintainer who manages the Sponsors listing.
		/// </summary>
		public bool IsCustomAmount { get; set; }

		/// <summary>
		/// Whether this tier is only for use with one-time sponsorships.
		/// </summary>
		public bool IsOneTime { get; set; }

		/// <summary>
		/// How much this tier costs per month in cents.
		/// </summary>
		public int MonthlyPriceInCents { get; set; }

		/// <summary>
		/// How much this tier costs per month in USD.
		/// </summary>
		public int MonthlyPriceInDollars { get; set; }

		/// <summary>
		/// The name of the tier.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The sponsors listing that this tier belongs to.
		/// </summary>
		public SponsorsListing SponsorsListing { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
