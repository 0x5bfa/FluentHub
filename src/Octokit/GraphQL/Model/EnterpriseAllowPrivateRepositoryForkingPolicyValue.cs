using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible values for the enterprise allow private repository forking policy value.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseAllowPrivateRepositoryForkingPolicyValue
    {
        /// <summary>
        /// Members can fork a repository to an organization within this enterprise.
        /// </summary>
        [EnumMember(Value = "ENTERPRISE_ORGANIZATIONS")]
        EnterpriseOrganizations,

        /// <summary>
        /// Members can fork a repository only within the same organization (intra-org).
        /// </summary>
        [EnumMember(Value = "SAME_ORGANIZATION")]
        SameOrganization,

        /// <summary>
        /// Members can fork a repository to their user account or within the same organization.
        /// </summary>
        [EnumMember(Value = "SAME_ORGANIZATION_USER_ACCOUNTS")]
        SameOrganizationUserAccounts,

        /// <summary>
        /// Members can fork a repository to their enterprise-managed user account or an organization inside this enterprise.
        /// </summary>
        [EnumMember(Value = "ENTERPRISE_ORGANIZATIONS_USER_ACCOUNTS")]
        EnterpriseOrganizationsUserAccounts,

        /// <summary>
        /// Members can fork a repository to their user account.
        /// </summary>
        [EnumMember(Value = "USER_ACCOUNTS")]
        UserAccounts,

        /// <summary>
        /// Members can fork a repository to their user account or an organization, either inside or outside of this enterprise.
        /// </summary>
        [EnumMember(Value = "EVERYWHERE")]
        Everywhere,
    }
}