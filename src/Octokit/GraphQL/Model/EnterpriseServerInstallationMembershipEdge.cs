namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Enterprise Server installation that a user is a member of.
    /// </summary>
    public class EnterpriseServerInstallationMembershipEdge : QueryableValue<EnterpriseServerInstallationMembershipEdge>
    {
        internal EnterpriseServerInstallationMembershipEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public EnterpriseServerInstallation Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.EnterpriseServerInstallation.Create);

        /// <summary>
        /// The role of the user in the enterprise membership.
        /// </summary>
        public EnterpriseUserAccountMembershipRole Role { get; }

        internal static EnterpriseServerInstallationMembershipEdge Create(Expression expression)
        {
            return new EnterpriseServerInstallationMembershipEdge(expression);
        }
    }
}