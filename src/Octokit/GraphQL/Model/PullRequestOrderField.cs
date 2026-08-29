using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which pull_requests connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestOrderField
    {
        /// <summary>
        /// Order pull_requests by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order pull_requests by update time
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,
    }
}