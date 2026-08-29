namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for EnterpriseServerUserAccountsUpload.
    /// </summary>
    public class EnterpriseServerUserAccountsUploadConnection : QueryableValue<EnterpriseServerUserAccountsUploadConnection>, IPagingConnection<EnterpriseServerUserAccountsUpload>
    {
        internal EnterpriseServerUserAccountsUploadConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<EnterpriseServerUserAccountsUploadEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<EnterpriseServerUserAccountsUpload> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static EnterpriseServerUserAccountsUploadConnection Create(Expression expression)
        {
            return new EnterpriseServerUserAccountsUploadConnection(expression);
        }
    }
}