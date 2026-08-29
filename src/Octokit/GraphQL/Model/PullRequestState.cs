using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a pull request.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestState
    {
        /// <summary>
        /// A pull request that is still open.
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// A pull request that has been closed without being merged.
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,

        /// <summary>
        /// A pull request that has been closed by being merged.
        /// </summary>
        [EnumMember(Value = "MERGED")]
        Merged,
    }
}