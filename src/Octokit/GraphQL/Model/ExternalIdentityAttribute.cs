namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An attribute for the External Identity attributes collection
    /// </summary>
    public class ExternalIdentityAttribute : QueryableValue<ExternalIdentityAttribute>
    {
        internal ExternalIdentityAttribute(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The attribute metadata as JSON
        /// </summary>
        public string Metadata { get; }

        /// <summary>
        /// The attribute name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The attribute value
        /// </summary>
        public string Value { get; }

        internal static ExternalIdentityAttribute Create(Expression expression)
        {
            return new ExternalIdentityAttribute(expression);
        }
    }
}