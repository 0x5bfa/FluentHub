using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which project v2 field connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2FieldOrderField
    {
        /// <summary>
        /// Order project v2 fields by position
        /// </summary>
        [EnumMember(Value = "POSITION")]
        Position,

        /// <summary>
        /// Order project v2 fields by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order project v2 fields by name
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,
    }
}