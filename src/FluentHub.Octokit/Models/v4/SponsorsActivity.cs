// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// An event related to sponsorship activity.
	/// </summary>
	public class SponsorsActivity
	{
		/// <summary>
		/// What action this activity indicates took place.
		/// </summary>
		public SponsorsActivityAction Action { get; set; }

		/// <summary>
		/// The sponsor's current privacy level.
		/// </summary>
		public SponsorshipPrivacy? CurrentPrivacyLevel { get; set; }

		/// <summary>
		/// The Node ID of the SponsorsActivity object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// The platform that was used to pay for the sponsorship.
		/// </summary>
		public SponsorshipPaymentSource? PaymentSource { get; set; }

		/// <summary>
		/// The tier that the sponsorship used to use, for tier change events.
		/// </summary>
		public SponsorsTier PreviousSponsorsTier { get; set; }

		/// <summary>
		/// The user or organization who triggered this activity and was/is sponsoring the sponsorable.
		/// </summary>
		public Sponsor Sponsor { get; set; }

		/// <summary>
		/// The user or organization that is being sponsored, the maintainer.
		/// </summary>
		public ISponsorable Sponsorable { get; set; }

		/// <summary>
		/// The associated sponsorship tier.
		/// </summary>
		public SponsorsTier SponsorsTier { get; set; }

		/// <summary>
		/// The timestamp of this event.
		/// </summary>
		public DateTimeOffset? Timestamp { get; set; }

		/// <summary>
		/// Humanized string of "The timestamp of this event."
		/// <summary>
		public string TimestampHumanized { get; set; }

		/// <summary>
		/// Was this sponsorship made alongside other sponsorships at the same time from the same sponsor?
		/// </summary>
		public bool ViaBulkSponsorship { get; set; }
	}
}
