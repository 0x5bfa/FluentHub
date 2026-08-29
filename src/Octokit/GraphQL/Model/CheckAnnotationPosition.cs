namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A character position in a check annotation.
    /// </summary>
    public class CheckAnnotationPosition : QueryableValue<CheckAnnotationPosition>
    {
        internal CheckAnnotationPosition(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Column number (1 indexed).
        /// </summary>
        public int? Column { get; }

        /// <summary>
        /// Line number (1 indexed).
        /// </summary>
        public int Line { get; }

        internal static CheckAnnotationPosition Create(Expression expression)
        {
            return new CheckAnnotationPosition(expression);
        }
    }
}