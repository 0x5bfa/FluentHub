namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Required status check
    /// </summary>
    public class StatusCheckConfiguration : QueryableValue<StatusCheckConfiguration>
    {
        internal StatusCheckConfiguration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The status check context name that must be present on the commit.
        /// </summary>
        public string Context { get; }

        /// <summary>
        /// The optional integration ID that this status check must originate from.
        /// </summary>
        public int? IntegrationId { get; }

        internal static StatusCheckConfiguration Create(Expression expression)
        {
            return new StatusCheckConfiguration(expression);
        }
    }
}