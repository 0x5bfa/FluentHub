namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Location information for an actor
    /// </summary>
    public class ActorLocation : QueryableValue<ActorLocation>
    {
        internal ActorLocation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// Region name
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Region or state code
        /// </summary>
        public string RegionCode { get; }

        internal static ActorLocation Create(Expression expression)
        {
            return new ActorLocation(expression);
        }
    }
}