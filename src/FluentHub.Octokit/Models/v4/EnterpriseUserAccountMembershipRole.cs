using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
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
    }
}