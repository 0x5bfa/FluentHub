namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class UserListEdge : QueryableValue<UserListEdge>
    {
        internal UserListEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public UserList Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.UserList.Create);

        internal static UserListEdge Create(Expression expression)
        {
            return new UserListEdge(expression);
        }
    }
}