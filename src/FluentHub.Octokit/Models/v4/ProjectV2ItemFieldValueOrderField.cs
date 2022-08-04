using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Properties by which project v2 item field value connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2ItemFieldValueOrderField
    {
        /// <summary>
        /// Order project v2 item field values by the their position in the project
        /// </summary>
        [EnumMember(Value = "POSITION")]
        Position,
    }
}