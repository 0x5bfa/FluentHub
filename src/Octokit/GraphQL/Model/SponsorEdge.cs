namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user or organization who is sponsoring someone in GitHub Sponsors.
    /// </summary>
    public class SponsorEdge : QueryableValue<SponsorEdge>
    {
        internal SponsorEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Sponsor Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Sponsor.Create);

        internal static SponsorEdge Create(Expression expression)
        {
            return new SponsorEdge(expression);
        }
    }
}