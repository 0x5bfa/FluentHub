namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Metadata for an audit entry with action org.*
    /// </summary>
    public interface IOrganizationAuditEntryData
    {        /// <summary>
        /// The Organization associated with the Audit Entry.
        /// </summary>
        Organization Organization { get; set; }

        /// <summary>
        /// The name of the Organization.
        /// </summary>
        string OrganizationName { get; set; }

        /// <summary>
        /// The HTTP path for the organization
        /// </summary>
        string OrganizationResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for the organization
        /// </summary>
        string OrganizationUrl { get; set; }
    }
}

namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class OrganizationAuditEntryData : IOrganizationAuditEntryData
    {
        public Organization Organization { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationResourcePath { get; set; }

        public string OrganizationUrl { get; set; }
    }
}