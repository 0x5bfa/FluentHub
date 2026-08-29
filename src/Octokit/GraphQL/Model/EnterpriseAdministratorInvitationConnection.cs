namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for EnterpriseAdministratorInvitation.
    /// </summary>
    public class EnterpriseAdministratorInvitationConnection : QueryableValue<EnterpriseAdministratorInvitationConnection>, IPagingConnection<EnterpriseAdministratorInvitation>
    {
        internal EnterpriseAdministratorInvitationConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<EnterpriseAdministratorInvitationEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<EnterpriseAdministratorInvitation> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static EnterpriseAdministratorInvitationConnection Create(Expression expression)
        {
            return new EnterpriseAdministratorInvitationConnection(expression);
        }
    }
}