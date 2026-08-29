namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An enterprise owner in the context of an organization that is part of the enterprise.
    /// </summary>
    public class OrganizationEnterpriseOwnerEdge : QueryableValue<OrganizationEnterpriseOwnerEdge>
    {
        internal OrganizationEnterpriseOwnerEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The role of the owner with respect to the organization.
        /// </summary>
        public RoleInOrganization OrganizationRole { get; }

        internal static OrganizationEnterpriseOwnerEdge Create(Expression expression)
        {
            return new OrganizationEnterpriseOwnerEdge(expression);
        }
    }
}