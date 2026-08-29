using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which IP allow list entry connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IpAllowListEntryOrderField
    {
        /// <summary>
        /// Order IP allow list entries by creation time.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order IP allow list entries by the allow list value.
        /// </summary>
        [EnumMember(Value = "ALLOW_LIST_VALUE")]
        AllowListValue,
    }
}