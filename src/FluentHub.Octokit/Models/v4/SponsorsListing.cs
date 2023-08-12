// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub Sponsors listing.
	/// </summary>
	public class SponsorsListing
	{
		/// <summary>
		/// The current goal the maintainer is trying to reach with GitHub Sponsors, if any.
		/// </summary>
		public SponsorsGoal ActiveGoal { get; set; }

		/// <summary>
		/// The Stripe Connect account currently in use for payouts for this Sponsors listing, if any. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
		/// </summary>
		public StripeConnectAccount ActiveStripeConnectAccount { get; set; }

		/// <summary>
		/// The name of the country or region with the maintainer's bank account or fiscal host. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
		/// </summary>
		public string BillingCountryOrRegion { get; set; }

		/// <summary>
		/// The email address used by GitHub to contact the sponsorable about their GitHub Sponsors profile. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
		/// </summary>
		public string ContactEmailAddress { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP path for the Sponsors dashboard for this Sponsors listing.
		/// </summary>
		public string DashboardResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the Sponsors dashboard for this Sponsors listing.
		/// </summary>
		public string DashboardUrl { get; set; }

		/// <summary>
		/// The records featured on the GitHub Sponsors profile.
		/// </summary>
		/// <param name="featureableTypes">The types of featured items to return.</param>
		public List<SponsorsListingFeaturedItem> FeaturedItems { get; set; }

		/// <summary>
		/// The fiscal host used for payments, if any. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
		/// </summary>
		public Organization FiscalHost { get; set; }

		/// <summary>
		/// The full description of the listing.
		/// </summary>
		public string FullDescription { get; set; }

		/// <summary>
		/// The full description of the listing rendered to HTML.
		/// </summary>
		public string FullDescriptionHTML { get; set; }

		public ID Id { get; set; }

		/// <summary>
		/// Whether this listing is publicly visible.
		/// </summary>
		public bool IsPublic { get; set; }

		/// <summary>
		/// The listing's full name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A future date on which this listing is eligible to receive a payout.
		/// </summary>
		public string NextPayoutDate { get; set; }

		/// <summary>
		/// The name of the country or region where the maintainer resides. Will only return a value when queried by the maintainer themselves, or by an admin of the sponsorable organization.
		/// </summary>
		public string ResidenceCountryOrRegion { get; set; }

		/// <summary>
		/// The HTTP path for this Sponsors listing.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The short description of the listing.
		/// </summary>
		public string ShortDescription { get; set; }

		/// <summary>
		/// The short name of the listing.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// The entity this listing represents who can be sponsored on GitHub Sponsors.
		/// </summary>
		public ISponsorable Sponsorable { get; set; }

		/// <summary>
		/// The tiers for this GitHub Sponsors profile.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="includeUnpublished">Whether to include tiers that aren't published. Only admins of the Sponsors listing can see draft tiers. Only admins of the Sponsors listing and viewers who are currently sponsoring on a retired tier can see those retired tiers. Defaults to including only published tiers, which are visible to anyone who can see the GitHub Sponsors profile.</param>
		/// <param name="orderBy">Ordering options for Sponsors tiers returned from the connection.</param>
		public SponsorsTierConnection Tiers { get; set; }

		/// <summary>
		/// The HTTP URL for this Sponsors listing.
		/// </summary>
		public string Url { get; set; }
	}
}
