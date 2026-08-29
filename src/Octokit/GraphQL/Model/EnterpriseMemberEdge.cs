namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A User who is a member of an enterprise through one or more organizations.
    /// </summary>
    public class EnterpriseMemberEdge : QueryableValue<EnterpriseMemberEdge>
    {
        internal EnterpriseMemberEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public EnterpriseMember Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.EnterpriseMember.Create);

        internal static EnterpriseMemberEdge Create(Expression expression)
        {
            return new EnterpriseMemberEdge(expression);
        }
    }
}