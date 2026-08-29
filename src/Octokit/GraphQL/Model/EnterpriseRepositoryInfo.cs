namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A subset of repository information queryable from an enterprise.
    /// </summary>
    public class EnterpriseRepositoryInfo : QueryableValue<EnterpriseRepositoryInfo>
    {
        internal EnterpriseRepositoryInfo(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Node ID of the EnterpriseRepositoryInfo object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies if the repository is private or internal.
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// The repository's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; }

        internal static EnterpriseRepositoryInfo Create(Expression expression)
        {
            return new EnterpriseRepositoryInfo(expression);
        }
    }
}