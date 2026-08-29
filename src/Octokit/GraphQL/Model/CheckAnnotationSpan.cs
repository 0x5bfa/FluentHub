namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An inclusive pair of positions for a check annotation.
    /// </summary>
    public class CheckAnnotationSpan : QueryableValue<CheckAnnotationSpan>
    {
        internal CheckAnnotationSpan(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// End position (inclusive).
        /// </summary>
        public CheckAnnotationPosition End => this.CreateProperty(x => x.End, Octokit.GraphQL.Model.CheckAnnotationPosition.Create);

        /// <summary>
        /// Start position (inclusive).
        /// </summary>
        public CheckAnnotationPosition Start => this.CreateProperty(x => x.Start, Octokit.GraphQL.Model.CheckAnnotationPosition.Create);

        internal static CheckAnnotationSpan Create(Expression expression)
        {
            return new CheckAnnotationSpan(expression);
        }
    }
}