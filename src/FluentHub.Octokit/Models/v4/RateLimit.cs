namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the client's rate limit.
    /// </summary>
    public class RateLimit
    {
        /// <summary>
        /// The point cost for the current query counting against the rate limit.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// The maximum number of points the client is permitted to consume in a 60 minute window.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// The maximum number of nodes this query may return
        /// </summary>
        public int NodeCount { get; set; }

        /// <summary>
        /// The number of points remaining in the current rate limit window.
        /// </summary>
        public int Remaining { get; set; }

        /// <summary>
        /// The time at which the current rate limit window resets in UTC epoch seconds.
        /// </summary>
        public DateTimeOffset ResetAt { get; set; }

        /// <summary>
        /// The number of points used in the current rate limit window.
        /// </summary>
        public int Used { get; set; }
    }
}