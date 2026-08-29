namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Email attributes from External Identity
    /// </summary>
    public class UserEmailMetadata : QueryableValue<UserEmailMetadata>
    {
        internal UserEmailMetadata(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Boolean to identify primary emails
        /// </summary>
        public bool? Primary { get; }

        /// <summary>
        /// Type of email
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Email id
        /// </summary>
        public string Value { get; }

        internal static UserEmailMetadata Create(Expression expression)
        {
            return new UserEmailMetadata(expression);
        }
    }
}