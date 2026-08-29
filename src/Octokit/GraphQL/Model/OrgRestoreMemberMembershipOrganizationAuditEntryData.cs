namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Metadata for an organization membership for org.restore_member actions
    /// </summary>
    public class OrgRestoreMemberMembershipOrganizationAuditEntryData : QueryableValue<OrgRestoreMemberMembershipOrganizationAuditEntryData>
    {
        internal OrgRestoreMemberMembershipOrganizationAuditEntryData(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Organization associated with the Audit Entry.
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The name of the Organization.
        /// </summary>
        public string OrganizationName { get; }

        /// <summary>
        /// The HTTP path for the organization
        /// </summary>
        public string OrganizationResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the organization
        /// </summary>
        public string OrganizationUrl { get; }

        internal static OrgRestoreMemberMembershipOrganizationAuditEntryData Create(Expression expression)
        {
            return new OrgRestoreMemberMembershipOrganizationAuditEntryData(expression);
        }
    }
}