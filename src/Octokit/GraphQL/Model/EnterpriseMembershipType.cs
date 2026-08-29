using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible values we have for filtering Platform::Objects::User#enterprises.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseMembershipType
    {
        /// <summary>
        /// Returns all enterprises in which the user is a member, admin, or billing manager.
        /// </summary>
        [EnumMember(Value = "ALL")]
        All,

        /// <summary>
        /// Returns all enterprises in which the user is an admin.
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,

        /// <summary>
        /// Returns all enterprises in which the user is a billing manager.
        /// </summary>
        [EnumMember(Value = "BILLING_MANAGER")]
        BillingManager,

        /// <summary>
        /// Returns all enterprises in which the user is a member of an org that is owned by the enterprise.
        /// </summary>
        [EnumMember(Value = "ORG_MEMBERSHIP")]
        OrgMembership,
    }
}