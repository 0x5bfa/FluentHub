namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A generic hovercard context with a message and icon
    /// </summary>
    public class GenericHovercardContext : QueryableValue<GenericHovercardContext>
    {
        internal GenericHovercardContext(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A string describing this context
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// An octicon to accompany this context
        /// </summary>
        public string Octicon { get; }

        internal static GenericHovercardContext Create(Expression expression)
        {
            return new GenericHovercardContext(expression);
        }
    }
}