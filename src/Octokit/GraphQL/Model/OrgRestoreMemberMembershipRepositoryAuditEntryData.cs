namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Metadata for a repository membership for org.restore_member actions
    /// </summary>
    public class OrgRestoreMemberMembershipRepositoryAuditEntryData : QueryableValue<OrgRestoreMemberMembershipRepositoryAuditEntryData>
    {
        internal OrgRestoreMemberMembershipRepositoryAuditEntryData(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The repository associated with the action
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The name of the repository
        /// </summary>
        public string RepositoryName { get; }

        /// <summary>
        /// The HTTP path for the repository
        /// </summary>
        public string RepositoryResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the repository
        /// </summary>
        public string RepositoryUrl { get; }

        internal static OrgRestoreMemberMembershipRepositoryAuditEntryData Create(Expression expression)
        {
            return new OrgRestoreMemberMembershipRepositoryAuditEntryData(expression);
        }
    }
}