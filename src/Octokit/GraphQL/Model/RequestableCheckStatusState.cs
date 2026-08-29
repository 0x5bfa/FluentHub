using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states that can be requested when creating a check run.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestableCheckStatusState
    {
        /// <summary>
        /// The check suite or run has been queued.
        /// </summary>
        [EnumMember(Value = "QUEUED")]
        Queued,

        /// <summary>
        /// The check suite or run is in progress.
        /// </summary>
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,

        /// <summary>
        /// The check suite or run has been completed.
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed,

        /// <summary>
        /// The check suite or run is in waiting state.
        /// </summary>
        [EnumMember(Value = "WAITING")]
        Waiting,

        /// <summary>
        /// The check suite or run is in pending state.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,
    }
}