namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An invitation to be a member in an enterprise organization.
    /// </summary>
    public class EnterprisePendingMemberInvitationEdge : QueryableValue<EnterprisePendingMemberInvitationEdge>
    {
        internal EnterprisePendingMemberInvitationEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public OrganizationInvitation Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.OrganizationInvitation.Create);

        internal static EnterprisePendingMemberInvitationEdge Create(Expression expression)
        {
            return new EnterprisePendingMemberInvitationEdge(expression);
        }
    }
}