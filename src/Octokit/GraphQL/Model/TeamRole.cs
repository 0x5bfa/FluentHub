using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The role of a user on a team.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamRole
    {
        /// <summary>
        /// User has admin rights on the team.
        /// </summary>
        [EnumMember(Value = "ADMIN")]
        Admin,

        /// <summary>
        /// User is a member of the team.
        /// </summary>
        [EnumMember(Value = "MEMBER")]
        Member,
    }
}