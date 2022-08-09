namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Repository interaction limit that applies to this object.
    /// </summary>
    public class RepositoryInteractionAbility
    {
        /// <summary>
        /// The time the currently active limit expires.
        /// </summary>
        public DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// Humanized string of "The time the currently active limit expires."
        /// <summary>
        public string ExpiresAtHumanized { get; set; }

        /// <summary>
        /// The current limit that is enabled on this object.
        /// </summary>
        public RepositoryInteractionLimit Limit { get; set; }

        /// <summary>
        /// The origin of the currently active interaction limit.
        /// </summary>
        public RepositoryInteractionLimitOrigin Origin { get; set; }
    }
}