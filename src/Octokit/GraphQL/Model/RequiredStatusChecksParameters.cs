namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Choose which status checks must pass before the ref is updated. When enabled, commits must first be pushed to another ref where the checks pass.
    /// </summary>
    public class RequiredStatusChecksParameters : QueryableValue<RequiredStatusChecksParameters>
    {
        internal RequiredStatusChecksParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Status checks that are required.
        /// </summary>
        public IQueryableList<StatusCheckConfiguration> RequiredStatusChecks => this.CreateProperty(x => x.RequiredStatusChecks);

        /// <summary>
        /// Whether pull requests targeting a matching branch must be tested with the latest code. This setting will not take effect unless at least one status check is enabled.
        /// </summary>
        public bool StrictRequiredStatusChecksPolicy { get; }

        internal static RequiredStatusChecksParameters Create(Expression expression)
        {
            return new RequiredStatusChecksParameters(expression);
        }
    }
}