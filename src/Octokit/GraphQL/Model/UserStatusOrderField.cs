using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which user status connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserStatusOrderField
    {
        /// <summary>
        /// Order user statuses by when they were updated.
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,
    }
}