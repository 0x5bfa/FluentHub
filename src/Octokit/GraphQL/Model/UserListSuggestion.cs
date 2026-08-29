namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a suggested user list.
    /// </summary>
    public class UserListSuggestion : QueryableValue<UserListSuggestion>
    {
        internal UserListSuggestion(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The ID of the suggested user list
        /// </summary>
        public ID? Id { get; }

        /// <summary>
        /// The name of the suggested user list
        /// </summary>
        public string Name { get; }

        internal static UserListSuggestion Create(Expression expression)
        {
            return new UserListSuggestion(expression);
        }
    }
}