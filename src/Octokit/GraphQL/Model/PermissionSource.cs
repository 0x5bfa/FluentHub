namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A level of permission and source for a user's access to a repository.
    /// </summary>
    public class PermissionSource : QueryableValue<PermissionSource>
    {
        internal PermissionSource(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The organization the repository belongs to.
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The level of access this source has granted to the user.
        /// </summary>
        public DefaultRepositoryPermissionField Permission { get; }

        /// <summary>
        /// The name of the role this source has granted to the user.
        /// </summary>
        public string RoleName { get; }

        /// <summary>
        /// The source of this permission.
        /// </summary>
        public PermissionGranter Source => this.CreateProperty(x => x.Source, Octokit.GraphQL.Model.PermissionGranter.Create);

        internal static PermissionSource Create(Expression expression)
        {
            return new PermissionSource(expression);
        }
    }
}