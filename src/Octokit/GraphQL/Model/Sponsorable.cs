namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entities that can sponsor or be sponsored through GitHub Sponsors.
    /// </summary>
    [GraphQLIdentifier("Sponsorable")]
    public interface ISponsorable : IQueryableValue<ISponsorable>, IQueryableInterface
    {
        /// <summary>
        /// The estimated next GitHub Sponsors payout for this user/organization in cents (USD).
        /// </summary>
        int EstimatedNextSponsorsPayoutInCents { get; }

        /// <summary>
        /// True if this user/organization has a GitHub Sponsors listing.
        /// </summary>
        bool HasSponsorsListing { get; }

        /// <summary>
        /// Whether the given account is sponsoring this user/organization.
        /// </summary>
        /// <param name="accountLogin">The target account's login.</param>
        bool IsSponsoredBy(Arg<string> accountLogin);

        /// <summary>
        /// True if the viewer is sponsored by this user/organization.
        /// </summary>
        bool IsSponsoringViewer { get; }

        /// <summary>
        /// Calculate how much each sponsor has ever paid total to this maintainer via GitHub Sponsors. Does not include sponsorships paid via Patreon.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for results returned from the connection.</param>
        SponsorAndLifetimeValueConnection LifetimeReceivedSponsorshipValues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorAndLifetimeValueOrder>? orderBy = null);

        /// <summary>
        /// The estimated monthly GitHub Sponsors income for this user/organization in cents (USD).
        /// </summary>
        int MonthlyEstimatedSponsorsIncomeInCents { get; }

        /// <summary>
        /// List of users and organizations this entity is sponsoring.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the users and organizations returned from the connection.</param>
        SponsorConnection Sponsoring(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null);

        /// <summary>
        /// List of sponsors for this user or organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsors returned from the connection.</param>
        /// <param name="tierId">If given, will filter for sponsors at the given tier. Will only return sponsors whose tier the viewer is permitted to see.</param>
        SponsorConnection Sponsors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null, Arg<ID>? tierId = null);

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
        SponsorsActivityConnection SponsorsActivities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<SponsorsActivityAction>>? actions = null, Arg<bool>? includeAsSponsor = null, Arg<bool>? includePrivate = null, Arg<SponsorsActivityOrder>? orderBy = null, Arg<SponsorsActivityPeriod>? period = null, Arg<DateTimeOffset>? since = null, Arg<DateTimeOffset>? until = null);

        /// <summary>
        /// The GitHub Sponsors listing for this user or organization.
        /// </summary>
        SponsorsListing SponsorsListing { get; }

        /// <summary>
        /// The sponsorship from the viewer to this user/organization; that is, the sponsorship where you're the sponsor.
        /// </summary>
        /// <param name="activeOnly">Whether to return the sponsorship only if it's still active. Pass false to get the viewer's sponsorship back even if it has been cancelled.</param>
        Sponsorship SponsorshipForViewerAsSponsor(Arg<bool>? activeOnly = null);

        /// <summary>
        /// The sponsorship from this user/organization to the viewer; that is, the sponsorship you're receiving.
        /// </summary>
        /// <param name="activeOnly">Whether to return the sponsorship only if it's still active. Pass false to get the sponsorship back even if it has been cancelled.</param>
        Sponsorship SponsorshipForViewerAsSponsorable(Arg<bool>? activeOnly = null);

        /// <summary>
        /// List of sponsorship updates sent from this sponsorable to sponsors.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsorship updates returned from the connection.</param>
        SponsorshipNewsletterConnection SponsorshipNewsletters(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipNewsletterOrder>? orderBy = null);

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
        SponsorshipConnection SponsorshipsAsMaintainer(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? activeOnly = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null);

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
        SponsorshipConnection SponsorshipsAsSponsor(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? activeOnly = null, Arg<IEnumerable<string>>? maintainerLogins = null, Arg<SponsorshipOrder>? orderBy = null);

        /// <summary>
        /// The amount in United States cents (e.g., 500 = $5.00 USD) that this entity has spent on GitHub to fund sponsorships. Only returns a value when viewed by the user themselves or by a user who can manage sponsorships for the requested organization.
        /// </summary>
        /// <param name="since">Filter payments to those that occurred on or after this time.</param>
        /// <param name="sponsorableLogins">Filter payments to those made to the users or organizations with the specified usernames.</param>
        /// <param name="until">Filter payments to those that occurred before this time.</param>
        int? TotalSponsorshipAmountAsSponsorInCents(Arg<DateTimeOffset>? since = null, Arg<IEnumerable<string>>? sponsorableLogins = null, Arg<DateTimeOffset>? until = null);

        /// <summary>
        /// Whether or not the viewer is able to sponsor this user/organization.
        /// </summary>
        bool ViewerCanSponsor { get; }

        /// <summary>
        /// True if the viewer is sponsoring this user/organization.
        /// </summary>
        bool ViewerIsSponsoring { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("Sponsorable")]
    internal class StubISponsorable : QueryableValue<StubISponsorable>, ISponsorable
    {
        internal StubISponsorable(Expression expression) : base(expression)
        {
        }

        public int EstimatedNextSponsorsPayoutInCents { get; }

        public bool HasSponsorsListing { get; }

        public bool IsSponsoredBy(Arg<string> accountLogin) => default;

        public bool IsSponsoringViewer { get; }

        public SponsorAndLifetimeValueConnection LifetimeReceivedSponsorshipValues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorAndLifetimeValueOrder>? orderBy = null) => this.CreateMethodCall(x => x.LifetimeReceivedSponsorshipValues(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorAndLifetimeValueConnection.Create);

        public int MonthlyEstimatedSponsorsIncomeInCents { get; }

        public SponsorConnection Sponsoring(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null) => this.CreateMethodCall(x => x.Sponsoring(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorConnection.Create);

        public SponsorConnection Sponsors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null, Arg<ID>? tierId = null) => this.CreateMethodCall(x => x.Sponsors(first, after, last, before, orderBy, tierId), Octokit.GraphQL.Model.SponsorConnection.Create);

        public SponsorsActivityConnection SponsorsActivities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<SponsorsActivityAction>>? actions = null, Arg<bool>? includeAsSponsor = null, Arg<bool>? includePrivate = null, Arg<SponsorsActivityOrder>? orderBy = null, Arg<SponsorsActivityPeriod>? period = null, Arg<DateTimeOffset>? since = null, Arg<DateTimeOffset>? until = null) => this.CreateMethodCall(x => x.SponsorsActivities(first, after, last, before, actions, includeAsSponsor, includePrivate, orderBy, period, since, until), Octokit.GraphQL.Model.SponsorsActivityConnection.Create);

        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        public Sponsorship SponsorshipForViewerAsSponsor(Arg<bool>? activeOnly = null) => this.CreateMethodCall(x => x.SponsorshipForViewerAsSponsor(activeOnly), Octokit.GraphQL.Model.Sponsorship.Create);

        public Sponsorship SponsorshipForViewerAsSponsorable(Arg<bool>? activeOnly = null) => this.CreateMethodCall(x => x.SponsorshipForViewerAsSponsorable(activeOnly), Octokit.GraphQL.Model.Sponsorship.Create);

        public SponsorshipNewsletterConnection SponsorshipNewsletters(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipNewsletterOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipNewsletters(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorshipNewsletterConnection.Create);

        public SponsorshipConnection SponsorshipsAsMaintainer(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? activeOnly = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipsAsMaintainer(first, after, last, before, activeOnly, includePrivate, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        public SponsorshipConnection SponsorshipsAsSponsor(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? activeOnly = null, Arg<IEnumerable<string>>? maintainerLogins = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipsAsSponsor(first, after, last, before, activeOnly, maintainerLogins, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        public int? TotalSponsorshipAmountAsSponsorInCents(Arg<DateTimeOffset>? since = null, Arg<IEnumerable<string>>? sponsorableLogins = null, Arg<DateTimeOffset>? until = null) => default;

        public bool ViewerCanSponsor { get; }

        public bool ViewerIsSponsoring { get; }

        internal static StubISponsorable Create(Expression expression)
        {
            return new StubISponsorable(expression);
        }
    }
}