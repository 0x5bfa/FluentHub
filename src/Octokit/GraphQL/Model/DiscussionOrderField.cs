using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which discussion connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiscussionOrderField
    {
        /// <summary>
        /// Order discussions by creation time.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order discussions by most recent modification time.
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,
    }
}