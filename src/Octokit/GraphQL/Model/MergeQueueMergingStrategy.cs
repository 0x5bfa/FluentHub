using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible merging strategies for a merge queue.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MergeQueueMergingStrategy
    {
        /// <summary>
        /// Entries only allowed to merge if they are passing.
        /// </summary>
        [EnumMember(Value = "ALLGREEN")]
        Allgreen,

        /// <summary>
        /// Failing Entires are allowed to merge if they are with a passing entry.
        /// </summary>
        [EnumMember(Value = "HEADGREEN")]
        Headgreen,
    }
}