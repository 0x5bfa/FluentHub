namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user who is a collaborator of a repository.
    /// </summary>
    public class RepositoryCollaboratorEdge : QueryableValue<RepositoryCollaboratorEdge>
    {
        internal RepositoryCollaboratorEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The permission the user has on the repository.
        /// </summary>
        public RepositoryPermission Permission { get; }

        /// <summary>
        /// A list of sources for the user's access to the repository.
        /// </summary>
        public IQueryableList<PermissionSource> PermissionSources => this.CreateProperty(x => x.PermissionSources);

        internal static RepositoryCollaboratorEdge Create(Expression expression)
        {
            return new RepositoryCollaboratorEdge(expression);
        }
    }
}