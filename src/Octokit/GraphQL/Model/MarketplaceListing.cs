namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A listing in the GitHub integration marketplace.
    /// </summary>
    public class MarketplaceListing : QueryableValue<MarketplaceListing>
    {
        internal MarketplaceListing(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The GitHub App this listing represents.
        /// </summary>
        public App App => this.CreateProperty(x => x.App, Octokit.GraphQL.Model.App.Create);

        /// <summary>
        /// URL to the listing owner's company site.
        /// </summary>
        public string CompanyUrl { get; }

        /// <summary>
        /// The HTTP path for configuring access to the listing's integration or OAuth app
        /// </summary>
        public string ConfigurationResourcePath { get; }

        /// <summary>
        /// The HTTP URL for configuring access to the listing's integration or OAuth app
        /// </summary>
        public string ConfigurationUrl { get; }

        /// <summary>
        /// URL to the listing's documentation.
        /// </summary>
        public string DocumentationUrl { get; }

        /// <summary>
        /// The listing's detailed description.
        /// </summary>
        public string ExtendedDescription { get; }

        /// <summary>
        /// The listing's detailed description rendered to HTML.
        /// </summary>
        public string ExtendedDescriptionHTML { get; }

        /// <summary>
        /// The listing's introductory description.
        /// </summary>
        public string FullDescription { get; }

        /// <summary>
        /// The listing's introductory description rendered to HTML.
        /// </summary>
        public string FullDescriptionHTML { get; }

        /// <summary>
        /// Does this listing have any plans with a free trial?
        /// </summary>
        public bool HasPublishedFreeTrialPlans { get; }

        /// <summary>
        /// Does this listing have a terms of service link?
        /// </summary>
        public bool HasTermsOfService { get; }

        /// <summary>
        /// Whether the creator of the app is a verified org
        /// </summary>
        public bool HasVerifiedOwner { get; }

        /// <summary>
        /// A technical description of how this app works with GitHub.
        /// </summary>
        public string HowItWorks { get; }

        /// <summary>
        /// The listing's technical description rendered to HTML.
        /// </summary>
        public string HowItWorksHTML { get; }

        /// <summary>
        /// The Node ID of the MarketplaceListing object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// URL to install the product to the viewer's account or organization.
        /// </summary>
        public string InstallationUrl { get; }

        /// <summary>
        /// Whether this listing's app has been installed for the current viewer
        /// </summary>
        public bool InstalledForViewer { get; }

        /// <summary>
        /// Whether this listing has been removed from the Marketplace.
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        /// Whether this listing is still an editable draft that has not been submitted for review and is not publicly visible in the Marketplace.
        /// </summary>
        public bool IsDraft { get; }

        /// <summary>
        /// Whether the product this listing represents is available as part of a paid plan.
        /// </summary>
        public bool IsPaid { get; }

        /// <summary>
        /// Whether this listing has been approved for display in the Marketplace.
        /// </summary>
        public bool IsPublic { get; }

        /// <summary>
        /// Whether this listing has been rejected by GitHub for display in the Marketplace.
        /// </summary>
        public bool IsRejected { get; }

        /// <summary>
        /// Whether this listing has been approved for unverified display in the Marketplace.
        /// </summary>
        public bool IsUnverified { get; }

        /// <summary>
        /// Whether this draft listing has been submitted for review for approval to be unverified in the Marketplace.
        /// </summary>
        public bool IsUnverifiedPending { get; }

        /// <summary>
        /// Whether this draft listing has been submitted for review from GitHub for approval to be verified in the Marketplace.
        /// </summary>
        public bool IsVerificationPendingFromDraft { get; }

        /// <summary>
        /// Whether this unverified listing has been submitted for review from GitHub for approval to be verified in the Marketplace.
        /// </summary>
        public bool IsVerificationPendingFromUnverified { get; }

        /// <summary>
        /// Whether this listing has been approved for verified display in the Marketplace.
        /// </summary>
        public bool IsVerified { get; }

        /// <summary>
        /// The hex color code, without the leading '#', for the logo background.
        /// </summary>
        public string LogoBackgroundColor { get; }

        /// <summary>
        /// URL for the listing's logo image.
        /// </summary>
        /// <param name="size">The size in pixels of the resulting square image.</param>
        public string LogoUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// The listing's full name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The listing's very short description without a trailing period or ampersands.
        /// </summary>
        public string NormalizedShortDescription { get; }

        /// <summary>
        /// URL to the listing's detailed pricing.
        /// </summary>
        public string PricingUrl { get; }

        /// <summary>
        /// The category that best describes the listing.
        /// </summary>
        public MarketplaceCategory PrimaryCategory => this.CreateProperty(x => x.PrimaryCategory, Octokit.GraphQL.Model.MarketplaceCategory.Create);

        /// <summary>
        /// URL to the listing's privacy policy, may return an empty string for listings that do not require a privacy policy URL.
        /// </summary>
        public string PrivacyPolicyUrl { get; }

        /// <summary>
        /// The HTTP path for the Marketplace listing.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The URLs for the listing's screenshots.
        /// </summary>
        public IEnumerable<string> ScreenshotUrls { get; }

        /// <summary>
        /// An alternate category that describes the listing.
        /// </summary>
        public MarketplaceCategory SecondaryCategory => this.CreateProperty(x => x.SecondaryCategory, Octokit.GraphQL.Model.MarketplaceCategory.Create);

        /// <summary>
        /// The listing's very short description.
        /// </summary>
        public string ShortDescription { get; }

        /// <summary>
        /// The short name of the listing used in its URL.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// URL to the listing's status page.
        /// </summary>
        public string StatusUrl { get; }

        /// <summary>
        /// An email address for support for this listing's app.
        /// </summary>
        public string SupportEmail { get; }

        /// <summary>
        /// Either a URL or an email address for support for this listing's app, may return an empty string for listings that do not require a support URL.
        /// </summary>
        public string SupportUrl { get; }

        /// <summary>
        /// URL to the listing's terms of service.
        /// </summary>
        public string TermsOfServiceUrl { get; }

        /// <summary>
        /// The HTTP URL for the Marketplace listing.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Can the current viewer add plans for this Marketplace listing.
        /// </summary>
        public bool ViewerCanAddPlans { get; }

        /// <summary>
        /// Can the current viewer approve this Marketplace listing.
        /// </summary>
        public bool ViewerCanApprove { get; }

        /// <summary>
        /// Can the current viewer delist this Marketplace listing.
        /// </summary>
        public bool ViewerCanDelist { get; }

        /// <summary>
        /// Can the current viewer edit this Marketplace listing.
        /// </summary>
        public bool ViewerCanEdit { get; }

        /// <summary>
        /// Can the current viewer edit the primary and secondary category of this
        /// Marketplace listing.
        /// </summary>
        public bool ViewerCanEditCategories { get; }

        /// <summary>
        /// Can the current viewer edit the plans for this Marketplace listing.
        /// </summary>
        public bool ViewerCanEditPlans { get; }

        /// <summary>
        /// Can the current viewer return this Marketplace listing to draft state
        /// so it becomes editable again.
        /// </summary>
        public bool ViewerCanRedraft { get; }

        /// <summary>
        /// Can the current viewer reject this Marketplace listing by returning it to
        /// an editable draft state or rejecting it entirely.
        /// </summary>
        public bool ViewerCanReject { get; }

        /// <summary>
        /// Can the current viewer request this listing be reviewed for display in
        /// the Marketplace as verified.
        /// </summary>
        public bool ViewerCanRequestApproval { get; }

        /// <summary>
        /// Indicates whether the current user has an active subscription to this Marketplace listing.
        /// </summary>
        public bool ViewerHasPurchased { get; }

        /// <summary>
        /// Indicates if the current user has purchased a subscription to this Marketplace listing
        /// for all of the organizations the user owns.
        /// </summary>
        public bool ViewerHasPurchasedForAllOrganizations { get; }

        /// <summary>
        /// Does the current viewer role allow them to administer this Marketplace listing.
        /// </summary>
        public bool ViewerIsListingAdmin { get; }

        internal static MarketplaceListing Create(Expression expression)
        {
            return new MarketplaceListing(expression);
        }
    }
}