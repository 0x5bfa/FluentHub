using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which project connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectOrderField
    {
        /// <summary>
        /// Order projects by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order projects by update time
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// Order projects by name
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,
    }
}