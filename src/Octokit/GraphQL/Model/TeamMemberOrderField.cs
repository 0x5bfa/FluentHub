using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which team member connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamMemberOrderField
    {
        /// <summary>
        /// Order team members by login
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,

        /// <summary>
        /// Order team members by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}