namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub account and the total amount in USD they've paid for sponsorships to a particular maintainer. Does not include payments made via Patreon.
    /// </summary>
    public class SponsorAndLifetimeValue : QueryableValue<SponsorAndLifetimeValue>
    {
        internal SponsorAndLifetimeValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The amount in cents.
        /// </summary>
        public int AmountInCents { get; }

        /// <summary>
        /// The amount in USD, formatted as a string.
        /// </summary>
        public string FormattedAmount { get; }

        /// <summary>
        /// The sponsor's GitHub account.
        /// </summary>
        public ISponsorable Sponsor => this.CreateProperty(x => x.Sponsor, Octokit.GraphQL.Model.Internal.StubISponsorable.Create);

        /// <summary>
        /// The maintainer's GitHub account.
        /// </summary>
        public ISponsorable Sponsorable => this.CreateProperty(x => x.Sponsorable, Octokit.GraphQL.Model.Internal.StubISponsorable.Create);

        internal static SponsorAndLifetimeValue Create(Expression expression)
        {
            return new SponsorAndLifetimeValue(expression);
        }
    }
}