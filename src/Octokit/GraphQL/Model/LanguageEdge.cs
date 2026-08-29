namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents the language of a repository.
    /// </summary>
    public class LanguageEdge : QueryableValue<LanguageEdge>
    {
        internal LanguageEdge(Expression expression) : base(expression)
        {
        }

        public string Cursor { get; }

        public Language Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Language.Create);

        /// <summary>
        /// The number of bytes of code written in the language.
        /// </summary>
        public int Size { get; }

        internal static LanguageEdge Create(Expression expression)
        {
            return new LanguageEdge(expression);
        }
    }
}