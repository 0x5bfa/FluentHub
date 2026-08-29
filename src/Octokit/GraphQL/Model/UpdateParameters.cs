namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Only allow users with bypass permission to update matching refs.
    /// </summary>
    public class UpdateParameters : QueryableValue<UpdateParameters>
    {
        internal UpdateParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Branch can pull changes from its upstream repository
        /// </summary>
        public bool UpdateAllowsFetchAndMerge { get; }

        internal static UpdateParameters Create(Expression expression)
        {
            return new UpdateParameters(expression);
        }
    }
}