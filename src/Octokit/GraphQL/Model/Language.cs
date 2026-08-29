namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a given language found in repositories.
    /// </summary>
    public class Language : QueryableValue<Language>
    {
        internal Language(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The color defined for the current language.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// The Node ID of the Language object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The name of the current language.
        /// </summary>
        public string Name { get; }

        internal static Language Create(Expression expression)
        {
            return new Language(expression);
        }
    }
}