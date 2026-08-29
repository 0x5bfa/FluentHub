namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Parameters to be used for the tag_name_pattern rule
    /// </summary>
    public class TagNamePatternParameters : QueryableValue<TagNamePatternParameters>
    {
        internal TagNamePatternParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// How this rule will appear to users.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// If true, the rule will fail if the pattern matches.
        /// </summary>
        public bool Negate { get; }

        /// <summary>
        /// The operator to use for matching.
        /// </summary>
        public string Operator { get; }

        /// <summary>
        /// The pattern to match with.
        /// </summary>
        public string Pattern { get; }

        internal static TagNamePatternParameters Create(Expression expression)
        {
            return new TagNamePatternParameters(expression);
        }
    }
}