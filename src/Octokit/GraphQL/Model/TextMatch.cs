namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A text match within a search result.
    /// </summary>
    public class TextMatch : QueryableValue<TextMatch>
    {
        internal TextMatch(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The specific text fragment within the property matched on.
        /// </summary>
        public string Fragment { get; }

        /// <summary>
        /// Highlights within the matched fragment.
        /// </summary>
        public IQueryableList<TextMatchHighlight> Highlights => this.CreateProperty(x => x.Highlights);

        /// <summary>
        /// The property matched on.
        /// </summary>
        public string Property { get; }

        internal static TextMatch Create(Expression expression)
        {
            return new TextMatch(expression);
        }
    }
}