namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A funding platform link for a repository.
    /// </summary>
    public class FundingLink : QueryableValue<FundingLink>
    {
        internal FundingLink(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The funding platform this link is for.
        /// </summary>
        public FundingPlatform Platform { get; }

        /// <summary>
        /// The configured URL for this funding link.
        /// </summary>
        public string Url { get; }

        internal static FundingLink Create(Expression expression)
        {
            return new FundingLink(expression);
        }
    }
}