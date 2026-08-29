using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Whether or not a PullRequest can be merged.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MergeableState
    {
        /// <summary>
        /// The pull request can be merged.
        /// </summary>
        [EnumMember(Value = "MERGEABLE")]
        Mergeable,

        /// <summary>
        /// The pull request cannot be merged due to merge conflicts.
        /// </summary>
        [EnumMember(Value = "CONFLICTING")]
        Conflicting,

        /// <summary>
        /// The mergeability of the pull request is still being calculated.
        /// </summary>
        [EnumMember(Value = "UNKNOWN")]
        Unknown,
    }
}