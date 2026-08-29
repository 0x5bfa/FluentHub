namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Repository interaction limit that applies to this object.
    /// </summary>
    public class RepositoryInteractionAbility : QueryableValue<RepositoryInteractionAbility>
    {
        internal RepositoryInteractionAbility(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The time the currently active limit expires.
        /// </summary>
        public DateTimeOffset? ExpiresAt { get; }

        /// <summary>
        /// The current limit that is enabled on this object.
        /// </summary>
        public RepositoryInteractionLimit Limit { get; }

        /// <summary>
        /// The origin of the currently active interaction limit.
        /// </summary>
        public RepositoryInteractionLimitOrigin Origin { get; }

        internal static RepositoryInteractionAbility Create(Expression expression)
        {
            return new RepositoryInteractionAbility(expression);
        }
    }
}