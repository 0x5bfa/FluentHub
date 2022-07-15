using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Properties by which star connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StarOrderField
    {
        /// <summary>
        /// Allows ordering a list of stars by when they were created.
        /// </summary>
        [EnumMember(Value = "STARRED_AT")]
        StarredAt,
    }
}