using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states for a merge queue entry.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MergeQueueEntryState
    {
        /// <summary>
        /// The entry is currently queued.
        /// </summary>
        [EnumMember(Value = "QUEUED")]
        Queued,

        /// <summary>
        /// The entry is currently waiting for checks to pass.
        /// </summary>
        [EnumMember(Value = "AWAITING_CHECKS")]
        AwaitingChecks,

        /// <summary>
        /// The entry is currently mergeable.
        /// </summary>
        [EnumMember(Value = "MERGEABLE")]
        Mergeable,

        /// <summary>
        /// The entry is currently unmergeable.
        /// </summary>
        [EnumMember(Value = "UNMERGEABLE")]
        Unmergeable,

        /// <summary>
        /// The entry is currently locked.
        /// </summary>
        [EnumMember(Value = "LOCKED")]
        Locked,
    }
}