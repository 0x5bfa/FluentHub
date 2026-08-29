namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An error produced from a Dependabot Update
    /// </summary>
    public class DependabotUpdateError : QueryableValue<DependabotUpdateError>
    {
        internal DependabotUpdateError(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the error
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The error code
        /// </summary>
        public string ErrorType { get; }

        /// <summary>
        /// The title of the error
        /// </summary>
        public string Title { get; }

        internal static DependabotUpdateError Create(Expression expression)
        {
            return new DependabotUpdateError(expression);
        }
    }
}