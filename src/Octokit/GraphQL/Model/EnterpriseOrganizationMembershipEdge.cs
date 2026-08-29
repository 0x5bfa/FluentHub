namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An enterprise organization that a user is a member of.
    /// </summary>
    public class EnterpriseOrganizationMembershipEdge : QueryableValue<EnterpriseOrganizationMembershipEdge>
    {
        internal EnterpriseOrganizationMembershipEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Organization Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The role of the user in the enterprise membership.
        /// </summary>
        public EnterpriseUserAccountMembershipRole Role { get; }

        internal static EnterpriseOrganizationMembershipEdge Create(Expression expression)
        {
            return new EnterpriseOrganizationMembershipEdge(expression);
        }
    }
}