using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible roles for enterprise membership.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseUserAccountMembershipRole
    {
        /// <summary>
        /// The user is a member of an organization in the enterprise.
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,

        /// <summary>
        /// The user is an owner of an organization in the enterprise.
        /// </summary>
        [EnumMember(Value = "OWNER")]
        Owner,

        /// <summary>
        /// The user is not an owner of the enterprise, and not a member or owner of any organizations in the enterprise; only for EMU-enabled enterprises.
        /// </summary>
        [EnumMember(Value = "UNAFFILIATED")]
        Unaffiliated,
    }
}