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
    public class EnterpriseServerUserAccountEdge : QueryableValue<EnterpriseServerUserAccountEdge>
    {
        internal EnterpriseServerUserAccountEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public EnterpriseServerUserAccount Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.EnterpriseServerUserAccount.Create);

        internal static EnterpriseServerUserAccountEdge Create(Expression expression)
        {
            return new EnterpriseServerUserAccountEdge(expression);
        }
    }
}