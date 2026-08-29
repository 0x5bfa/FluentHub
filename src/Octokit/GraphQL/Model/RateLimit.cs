namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents the client's rate limit.
    /// </summary>
    public class RateLimit : QueryableValue<RateLimit>
    {
        internal RateLimit(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The point cost for the current query counting against the rate limit.
        /// </summary>
        public int Cost { get; }

        /// <summary>
        /// The maximum number of points the client is permitted to consume in a 60 minute window.
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// The maximum number of nodes this query may return
        /// </summary>
        public int NodeCount { get; }

        /// <summary>
        /// The number of points remaining in the current rate limit window.
        /// </summary>
        public int Remaining { get; }

        /// <summary>
        /// The time at which the current rate limit window resets in UTC epoch seconds.
        /// </summary>
        public DateTimeOffset ResetAt { get; }

        /// <summary>
        /// The number of points used in the current rate limit window.
        /// </summary>
        public int Used { get; }

        internal static RateLimit Create(Expression expression)
        {
            return new RateLimit(expression);
        }
    }
}