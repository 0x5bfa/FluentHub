namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A list of sponsorships either from the subject or received by the subject.
    /// </summary>
    public class SponsorshipConnection : QueryableValue<SponsorshipConnection>, IPagingConnection<Sponsorship>
    {
        internal SponsorshipConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<SponsorshipEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<Sponsorship> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// The total amount in cents of all recurring sponsorships in the connection whose amount you can view. Does not include one-time sponsorships.
        /// </summary>
        public int TotalRecurringMonthlyPriceInCents { get; }

        /// <summary>
        /// The total amount in USD of all recurring sponsorships in the connection whose amount you can view. Does not include one-time sponsorships.
        /// </summary>
        public int TotalRecurringMonthlyPriceInDollars { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static SponsorshipConnection Create(Expression expression)
        {
            return new SponsorshipConnection(expression);
        }
    }
}