namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository contact link.
    /// </summary>
    public class RepositoryContactLink : QueryableValue<RepositoryContactLink>
    {
        internal RepositoryContactLink(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The contact link purpose.
        /// </summary>
        public string About { get; }

        /// <summary>
        /// The contact link name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The contact link URL.
        /// </summary>
        public string Url { get; }

        internal static RepositoryContactLink Create(Expression expression)
        {
            return new RepositoryContactLink(expression);
        }
    }
}