namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A User who is an administrator of an enterprise.
    /// </summary>
    public class EnterpriseAdministratorEdge : QueryableValue<EnterpriseAdministratorEdge>
    {
        internal EnterpriseAdministratorEdge(Expression expression) : base(expression)
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
        /// The role of the administrator.
        /// </summary>
        public EnterpriseAdministratorRole Role { get; }

        internal static EnterpriseAdministratorEdge Create(Expression expression)
        {
            return new EnterpriseAdministratorEdge(expression);
        }
    }
}