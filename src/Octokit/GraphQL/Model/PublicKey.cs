namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user's public key.
    /// </summary>
    public class PublicKey : QueryableValue<PublicKey>
    {
        internal PublicKey(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The last time this authorization was used to perform an action. Values will be null for keys not owned by the user.
        /// </summary>
        public DateTimeOffset? AccessedAt { get; }

        /// <summary>
        /// Identifies the date and time when the key was created. Keys created before March 5th, 2014 have inaccurate values. Values will be null for keys not owned by the user.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// The fingerprint for this PublicKey.
        /// </summary>
        public string Fingerprint { get; }

        /// <summary>
        /// The Node ID of the PublicKey object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether this PublicKey is read-only or not. Values will be null for keys not owned by the user.
        /// </summary>
        public bool? IsReadOnly { get; }

        /// <summary>
        /// The public key string.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Identifies the date and time when the key was updated. Keys created before March 5th, 2014 may have inaccurate values. Values will be null for keys not owned by the user.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; }

        internal static PublicKey Create(Expression expression)
        {
            return new PublicKey(expression);
        }
    }
}