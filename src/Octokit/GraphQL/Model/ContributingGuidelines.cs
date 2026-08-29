namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The Contributing Guidelines for a repository.
    /// </summary>
    public class ContributingGuidelines : QueryableValue<ContributingGuidelines>
    {
        internal ContributingGuidelines(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the Contributing Guidelines.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The HTTP path for the Contributing Guidelines.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the Contributing Guidelines.
        /// </summary>
        public string Url { get; }

        internal static ContributingGuidelines Create(Expression expression)
        {
            return new ContributingGuidelines(expression);
        }
    }
}