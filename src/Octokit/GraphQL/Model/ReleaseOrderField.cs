using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which release connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReleaseOrderField
    {
        /// <summary>
        /// Order releases by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order releases alphabetically by name
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,
    }
}