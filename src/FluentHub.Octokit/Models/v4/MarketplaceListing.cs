// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A listing in the GitHub integration marketplace.
	/// </summary>
	public class MarketplaceListing
	{
		/// <summary>
		/// The GitHub App this listing represents.
		/// </summary>
		public App App { get; set; }

		/// <summary>
		/// URL to the listing owner's company site.
		/// </summary>
		public string CompanyUrl { get; set; }

		/// <summary>
		/// The HTTP path for configuring access to the listing's integration or OAuth app
		/// </summary>
		public string ConfigurationResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for configuring access to the listing's integration or OAuth app
		/// </summary>
		public string ConfigurationUrl { get; set; }

		/// <summary>
		/// URL to the listing's documentation.
		/// </summary>
		public string DocumentationUrl { get; set; }

		/// <summary>
		/// The listing's detailed description.
		/// </summary>
		public string ExtendedDescription { get; set; }

		/// <summary>
		/// The listing's detailed description rendered to HTML.
		/// </summary>
		public string ExtendedDescriptionHTML { get; set; }

		/// <summary>
		/// The listing's introductory description.
		/// </summary>
		public string FullDescription { get; set; }

		/// <summary>
		/// The listing's introductory description rendered to HTML.
		/// </summary>
		public string FullDescriptionHTML { get; set; }

		/// <summary>
		/// Does this listing have any plans with a free trial?
		/// </summary>
		public bool HasPublishedFreeTrialPlans { get; set; }

		/// <summary>
		/// Does this listing have a terms of service link?
		/// </summary>
		public bool HasTermsOfService { get; set; }

		/// <summary>
		/// Whether the creator of the app is a verified org
		/// </summary>
		public bool HasVerifiedOwner { get; set; }

		/// <summary>
		/// A technical description of how this app works with GitHub.
		/// </summary>
		public string HowItWorks { get; set; }

		/// <summary>
		/// The listing's technical description rendered to HTML.
		/// </summary>
		public string HowItWorksHTML { get; set; }

		/// <summary>
		/// The Node ID of the MarketplaceListing object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// URL to install the product to the viewer's account or organization.
		/// </summary>
		public string InstallationUrl { get; set; }

		/// <summary>
		/// Whether this listing's app has been installed for the current viewer
		/// </summary>
		public bool InstalledForViewer { get; set; }

		/// <summary>
		/// Whether this listing has been removed from the Marketplace.
		/// </summary>
		public bool IsArchived { get; set; }

		/// <summary>
		/// Whether this listing is still an editable draft that has not been submitted for review and is not publicly visible in the Marketplace.
		/// </summary>
		public bool IsDraft { get; set; }

		/// <summary>
		/// Whether the product this listing represents is available as part of a paid plan.
		/// </summary>
		public bool IsPaid { get; set; }

		/// <summary>
		/// Whether this listing has been approved for display in the Marketplace.
		/// </summary>
		public bool IsPublic { get; set; }

		/// <summary>
		/// Whether this listing has been rejected by GitHub for display in the Marketplace.
		/// </summary>
		public bool IsRejected { get; set; }

		/// <summary>
		/// Whether this listing has been approved for unverified display in the Marketplace.
		/// </summary>
		public bool IsUnverified { get; set; }

		/// <summary>
		/// Whether this draft listing has been submitted for review for approval to be unverified in the Marketplace.
		/// </summary>
		public bool IsUnverifiedPending { get; set; }

		/// <summary>
		/// Whether this draft listing has been submitted for review from GitHub for approval to be verified in the Marketplace.
		/// </summary>
		public bool IsVerificationPendingFromDraft { get; set; }

		/// <summary>
		/// Whether this unverified listing has been submitted for review from GitHub for approval to be verified in the Marketplace.
		/// </summary>
		public bool IsVerificationPendingFromUnverified { get; set; }

		/// <summary>
		/// Whether this listing has been approved for verified display in the Marketplace.
		/// </summary>
		public bool IsVerified { get; set; }

		/// <summary>
		/// The hex color code, without the leading '#', for the logo background.
		/// </summary>
		public string LogoBackgroundColor { get; set; }

		/// <summary>
		/// URL for the listing's logo image.
		/// </summary>
		/// <param name="size">The size in pixels of the resulting square image.</param>
		public string LogoUrl { get; set; }

		/// <summary>
		/// The listing's full name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The listing's very short description without a trailing period or ampersands.
		/// </summary>
		public string NormalizedShortDescription { get; set; }

		/// <summary>
		/// URL to the listing's detailed pricing.
		/// </summary>
		public string PricingUrl { get; set; }

		/// <summary>
		/// The category that best describes the listing.
		/// </summary>
		public MarketplaceCategory PrimaryCategory { get; set; }

		/// <summary>
		/// URL to the listing's privacy policy, may return an empty string for listings that do not require a privacy policy URL.
		/// </summary>
		public string PrivacyPolicyUrl { get; set; }

		/// <summary>
		/// The HTTP path for the Marketplace listing.
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// The URLs for the listing's screenshots.
		/// </summary>
		public List<string> ScreenshotUrls { get; set; }

		/// <summary>
		/// An alternate category that describes the listing.
		/// </summary>
		public MarketplaceCategory SecondaryCategory { get; set; }

		/// <summary>
		/// The listing's very short description.
		/// </summary>
		public string ShortDescription { get; set; }

		/// <summary>
		/// The short name of the listing used in its URL.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// URL to the listing's status page.
		/// </summary>
		public string StatusUrl { get; set; }

		/// <summary>
		/// An email address for support for this listing's app.
		/// </summary>
		public string SupportEmail { get; set; }

		/// <summary>
		/// Either a URL or an email address for support for this listing's app, may return an empty string for listings that do not require a support URL.
		/// </summary>
		public string SupportUrl { get; set; }

		/// <summary>
		/// URL to the listing's terms of service.
		/// </summary>
		public string TermsOfServiceUrl { get; set; }

		/// <summary>
		/// The HTTP URL for the Marketplace listing.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Can the current viewer add plans for this Marketplace listing.
		/// </summary>
		public bool ViewerCanAddPlans { get; set; }

		/// <summary>
		/// Can the current viewer approve this Marketplace listing.
		/// </summary>
		public bool ViewerCanApprove { get; set; }

		/// <summary>
		/// Can the current viewer delist this Marketplace listing.
		/// </summary>
		public bool ViewerCanDelist { get; set; }

		/// <summary>
		/// Can the current viewer edit this Marketplace listing.
		/// </summary>
		public bool ViewerCanEdit { get; set; }

		/// <summary>
		/// Can the current viewer edit the primary and secondary category of this
		/// Marketplace listing.
		/// </summary>
		public bool ViewerCanEditCategories { get; set; }

		/// <summary>
		/// Can the current viewer edit the plans for this Marketplace listing.
		/// </summary>
		public bool ViewerCanEditPlans { get; set; }

		/// <summary>
		/// Can the current viewer return this Marketplace listing to draft state
		/// so it becomes editable again.
		/// </summary>
		public bool ViewerCanRedraft { get; set; }

		/// <summary>
		/// Can the current viewer reject this Marketplace listing by returning it to
		/// an editable draft state or rejecting it entirely.
		/// </summary>
		public bool ViewerCanReject { get; set; }

		/// <summary>
		/// Can the current viewer request this listing be reviewed for display in
		/// the Marketplace as verified.
		/// </summary>
		public bool ViewerCanRequestApproval { get; set; }

		/// <summary>
		/// Indicates whether the current user has an active subscription to this Marketplace listing.
		/// </summary>
		public bool ViewerHasPurchased { get; set; }

		/// <summary>
		/// Indicates if the current user has purchased a subscription to this Marketplace listing
		/// for all of the organizations the user owns.
		/// </summary>
		public bool ViewerHasPurchasedForAllOrganizations { get; set; }

		/// <summary>
		/// Does the current viewer role allow them to administer this Marketplace listing.
		/// </summary>
		public bool ViewerIsListingAdmin { get; set; }
	}
}
