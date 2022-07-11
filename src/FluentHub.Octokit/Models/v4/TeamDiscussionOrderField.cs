using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.v4.Model
{
    /// <summary>
    /// Properties by which team discussion connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamDiscussionOrderField
    {
        /// <summary>
        /// Allows chronological ordering of team discussions.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}