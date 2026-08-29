namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user that's starred a repository.
    /// </summary>
    public class StargazerEdge : QueryableValue<StargazerEdge>
    {
        internal StargazerEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// Identifies when the item was starred.
        /// </summary>
        public DateTimeOffset StarredAt { get; }

        internal static StargazerEdge Create(Expression expression)
        {
            return new StargazerEdge(expression);
        }
    }
}