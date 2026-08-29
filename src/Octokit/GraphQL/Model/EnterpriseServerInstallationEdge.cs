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
    public class EnterpriseServerInstallationEdge : QueryableValue<EnterpriseServerInstallationEdge>
    {
        internal EnterpriseServerInstallationEdge(Expression expression) : base(expression)
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

        internal static EnterpriseServerInstallationEdge Create(Expression expression)
        {
            return new EnterpriseServerInstallationEdge(expression);
        }
    }
}