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
    public class UserStatusEdge : QueryableValue<UserStatusEdge>
    {
        internal UserStatusEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public UserStatus Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.UserStatus.Create);

        internal static UserStatusEdge Create(Expression expression)
        {
            return new UserStatusEdge(expression);
        }
    }
}