namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A User who is an outside collaborator of an enterprise through one or more organizations.
    /// </summary>
    public class EnterpriseOutsideCollaboratorEdge : QueryableValue<EnterpriseOutsideCollaboratorEdge>
    {
        internal EnterpriseOutsideCollaboratorEdge(Expression expression) : base(expression)
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
        /// The enterprise organization repositories this user is a member of.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for repositories.</param>
        public EnterpriseRepositoryInfoConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryOrder>? orderBy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, orderBy), Octokit.GraphQL.Model.EnterpriseRepositoryInfoConnection.Create);

        internal static EnterpriseOutsideCollaboratorEdge Create(Expression expression)
        {
            return new EnterpriseOutsideCollaboratorEdge(expression);
        }
    }
}