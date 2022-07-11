using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.v4.Model
{
    /// <summary>
    /// Properties by which label connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LabelOrderField
    {
        /// <summary>
        /// Order labels by name 
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,

        /// <summary>
        /// Order labels by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}