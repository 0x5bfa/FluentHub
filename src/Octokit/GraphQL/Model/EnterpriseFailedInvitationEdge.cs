namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A failed invitation to be a member in an enterprise organization.
    /// </summary>
    public class EnterpriseFailedInvitationEdge : QueryableValue<EnterpriseFailedInvitationEdge>
    {
        internal EnterpriseFailedInvitationEdge(Expression expression) : base(expression)
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

        internal static EnterpriseFailedInvitationEdge Create(Expression expression)
        {
            return new EnterpriseFailedInvitationEdge(expression);
        }
    }
}