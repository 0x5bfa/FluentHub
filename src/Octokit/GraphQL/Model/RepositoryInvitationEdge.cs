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
    public class RepositoryInvitationEdge : QueryableValue<RepositoryInvitationEdge>
    {
        internal RepositoryInvitationEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public RepositoryInvitation Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.RepositoryInvitation.Create);

        internal static RepositoryInvitationEdge Create(Expression expression)
        {
            return new RepositoryInvitationEdge(expression);
        }
    }
}