using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible organization invitation types.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrganizationInvitationType
    {
        /// <summary>
        /// The invitation was to an existing user.
        /// </summary>
        [EnumMember(Value = "USER")]
        User,

        /// <summary>
        /// The invitation was to an email address.
        /// </summary>
        [EnumMember(Value = "EMAIL")]
        Email,
    }
}