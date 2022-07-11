using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.v4.Model
{
    /// <summary>
    /// Properties by which enterprise member connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnterpriseMemberOrderField
    {
        /// <summary>
        /// Order enterprise members by login
        /// </summary>
        [EnumMember(Value = "LOGIN")]
        Login,

        /// <summary>
        /// Order enterprise members by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}