// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{

	/// <summary>
	/// Entities that can sponsor or be sponsored through GitHub Sponsors.
	/// </summary>
	public interface ISponsorable
	{
		/// <summary>
		/// The estimated next GitHub Sponsors payout for this user/organization in cents (USD).
		/// </summary>
		int EstimatedNextSponsorsPayoutInCents { get; set; }

		/// <summary>
		/// True if this user/organization has a GitHub Sponsors listing.
		/// </summary>
		bool HasSponsorsListing { get; set; }

		/// <summary>
		/// Whether the given account is sponsoring this user/organization.
		/// </summary>
		/// <param name="accountLogin">The target account's login.</param>
		bool IsSponsoredBy { get; set; }

		/// <summary>
		/// True if the viewer is sponsored by this user/organization.
		/// </summary>
		bool IsSponsoringViewer { get; set; }

		/// <summary>
		/// The estimated monthly GitHub Sponsors income for this user/organization in cents (USD).
		/// </summary>
		int MonthlyEstimatedSponsorsIncomeInCents { get; set; }

		/// <summary>
		/// List of users and organizations this entity is sponsoring.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for the users and organizations returned from the connection.</param>
		SponsorConnection Sponsoring { get; set; }

		/// <summary>
		/// List of sponsors for this user or organization.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for sponsors returned from the connection.</param>
		/// <param name="tierId">If given, will filter for sponsors at the given tier. Will only return sponsors whose tier the viewer is permitted to see.</param>
		SponsorConnection Sponsors { get; set; }

		/// <summary>
		/// Events involving this sponsorable, such as new sponsorships.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="actions">Filter activities to only the specified actions.</param>
		/// <param name="includeAsSponsor">Whether to include those events where this sponsorable acted as the sponsor. Defaults to only including events where this sponsorable was the recipient of a sponsorship.</param>
		/// <param name="includePrivate">Whether or not to include private activities in the result set. Defaults to including public and private activities.</param>
		/// <param name="orderBy">Ordering options for activity returned from the connection.</param>
		/// <param name="period">Filter activities returned to only those that occurred in the most recent specified time period. Set to ALL to avoid filtering by when the activity occurred. Will be ignored if `since` or `until` is given.</param>
		/// <param name="since">Filter activities to those that occurred on or after this time.</param>
		/// <param name="until">Filter activities to those that occurred before this time.</param>
		SponsorsActivityConnection SponsorsActivities { get; set; }

		/// <summary>
		/// The GitHub Sponsors listing for this user or organization.
		/// </summary>
		SponsorsListing SponsorsListing { get; set; }

		/// <summary>
		/// The sponsorship from the viewer to this user/organization; that is, the sponsorship where you're the sponsor.
		/// </summary>
		/// <param name="activeOnly">Whether to return the sponsorship only if it's still active. Pass false to get the viewer's sponsorship back even if it has been cancelled.</param>
		Sponsorship SponsorshipForViewerAsSponsor { get; set; }

		/// <summary>
		/// The sponsorship from this user/organization to the viewer; that is, the sponsorship you're receiving.
		/// </summary>
		/// <param name="activeOnly">Whether to return the sponsorship only if it's still active. Pass false to get the sponsorship back even if it has been cancelled.</param>
		Sponsorship SponsorshipForViewerAsSponsorable { get; set; }

		/// <summary>
		/// List of sponsorship updates sent from this sponsorable to sponsors.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for sponsorship updates returned from the connection.</param>
		SponsorshipNewsletterConnection SponsorshipNewsletters { get; set; }

		/// <summary>
		/// The sponsorships where this user or organization is the maintainer receiving the funds.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="activeOnly">Whether to include only sponsorships that are active right now, versus all sponsorships this maintainer has ever received.</param>
		/// <param name="includePrivate">Whether or not to include private sponsorships in the result set</param>
		/// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
		SponsorshipConnection SponsorshipsAsMaintainer { get; set; }

		/// <summary>
		/// The sponsorships where this user or organization is the funder.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="activeOnly">Whether to include only sponsorships that are active right now, versus all sponsorships this sponsor has ever made.</param>
		/// <param name="maintainerLogins">Filter sponsorships returned to those for the specified maintainers. That is, the recipient of the sponsorship is a user or organization with one of the given logins.</param>
		/// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
		SponsorshipConnection SponsorshipsAsSponsor { get; set; }

		/// <summary>
		/// The amount in United States cents (e.g., 500 = $5.00 USD) that this entity has spent on GitHub to fund sponsorships. Only returns a value when viewed by the user themselves or by a user who can manage sponsorships for the requested organization.
		/// </summary>
		/// <param name="since">Filter payments to those that occurred on or after this time.</param>
		/// <param name="sponsorableLogins">Filter payments to those made to the users or organizations with the specified usernames.</param>
		/// <param name="until">Filter payments to those that occurred before this time.</param>
		int? TotalSponsorshipAmountAsSponsorInCents { get; set; }

		/// <summary>
		/// Whether or not the viewer is able to sponsor this user/organization.
		/// </summary>
		bool ViewerCanSponsor { get; set; }

		/// <summary>
		/// True if the viewer is sponsoring this user/organization.
		/// </summary>
		bool ViewerIsSponsoring { get; set; }
	}
}

namespace FluentHub.Octokit.Models.v4
{
	public class Sponsorable : ISponsorable
	{
		public int EstimatedNextSponsorsPayoutInCents { get; set; }

		public bool HasSponsorsListing { get; set; }

		public bool IsSponsoredBy { get; set; }

		public bool IsSponsoringViewer { get; set; }

		public int MonthlyEstimatedSponsorsIncomeInCents { get; set; }

		public SponsorConnection Sponsoring { get; set; }

		public SponsorConnection Sponsors { get; set; }

		public SponsorsActivityConnection SponsorsActivities { get; set; }

		public SponsorsListing SponsorsListing { get; set; }

		public Sponsorship SponsorshipForViewerAsSponsor { get; set; }

		public Sponsorship SponsorshipForViewerAsSponsorable { get; set; }

		public SponsorshipNewsletterConnection SponsorshipNewsletters { get; set; }

		public SponsorshipConnection SponsorshipsAsMaintainer { get; set; }

		public SponsorshipConnection SponsorshipsAsSponsor { get; set; }

		public int? TotalSponsorshipAmountAsSponsorInCents { get; set; }

		public bool ViewerCanSponsor { get; set; }

		public bool ViewerIsSponsoring { get; set; }
	}
}

