namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository deploy key.
    /// </summary>
    public class DeployKey : QueryableValue<DeployKey>
    {
        internal DeployKey(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The Node ID of the DeployKey object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The deploy key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Whether or not the deploy key is read only.
        /// </summary>
        public bool ReadOnly { get; }

        /// <summary>
        /// The deploy key title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Whether or not the deploy key has been verified.
        /// </summary>
        public bool Verified { get; }

        internal static DeployKey Create(Expression expression)
        {
            return new DeployKey(expression);
        }
    }
}