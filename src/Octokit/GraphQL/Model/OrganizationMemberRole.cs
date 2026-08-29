using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible roles within an organization for its members.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrganizationMemberRole
    {
        /// <summary>
        /// The user is a member of the organization.
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,

        /// <summary>
        /// The user is an administrator of the organization.
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,
    }
}