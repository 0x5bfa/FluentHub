using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a check run in a status rollup.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CheckRunState
    {
        /// <summary>
        /// The check run requires action.
        /// </summary>
        [EnumMember(Value = "ACTION_REQUIRED")]
        ActionRequired,

        /// <summary>
        /// The check run has been cancelled.
        /// </summary>
        [EnumMember(Value = "CANCELLED")]
        Cancelled,

        /// <summary>
        /// The check run has been completed.
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed,

        /// <summary>
        /// The check run has failed.
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,

        /// <summary>
        /// The check run is in progress.
        /// </summary>
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,

        /// <summary>
        /// The check run was neutral.
        /// </summary>
        [EnumMember(Value = "NEUTRAL")]
        Neutral,

        /// <summary>
        /// The check run is in pending state.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// The check run has been queued.
        /// </summary>
        [EnumMember(Value = "QUEUED")]
        Queued,

        /// <summary>
        /// The check run was skipped.
        /// </summary>
        [EnumMember(Value = "SKIPPED")]
        Skipped,

        /// <summary>
        /// The check run was marked stale by GitHub. Only GitHub can use this conclusion.
        /// </summary>
        [EnumMember(Value = "STALE")]
        Stale,

        /// <summary>
        /// The check run has failed at startup.
        /// </summary>
        [EnumMember(Value = "STARTUP_FAILURE")]
        StartupFailure,

        /// <summary>
        /// The check run has succeeded.
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// The check run has timed out.
        /// </summary>
        [EnumMember(Value = "TIMED_OUT")]
        TimedOut,

        /// <summary>
        /// The check run is in waiting state.
        /// </summary>
        [EnumMember(Value = "WAITING")]
        Waiting,
    }
}