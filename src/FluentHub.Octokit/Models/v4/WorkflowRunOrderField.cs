using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Properties by which workflow run connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WorkflowRunOrderField
    {
        /// <summary>
        /// Order workflow runs by most recently created
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}