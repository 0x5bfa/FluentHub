using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states for a check suite or run conclusion.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CheckConclusionState
    {
        /// <summary>
        /// The check suite or run requires action.
        /// </summary>
        [EnumMember(Value = "ACTION_REQUIRED")]
        ActionRequired,

        /// <summary>
        /// The check suite or run has timed out.
        /// </summary>
        [EnumMember(Value = "TIMED_OUT")]
        TimedOut,

        /// <summary>
        /// The check suite or run has been cancelled.
        /// </summary>
        [EnumMember(Value = "CANCELLED")]
        Cancelled,

        /// <summary>
        /// The check suite or run has failed.
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,

        /// <summary>
        /// The check suite or run has succeeded.
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// The check suite or run was neutral.
        /// </summary>
        [EnumMember(Value = "NEUTRAL")]
        Neutral,

        /// <summary>
        /// The check suite or run was skipped.
        /// </summary>
        [EnumMember(Value = "SKIPPED")]
        Skipped,

        /// <summary>
        /// The check suite or run has failed at startup.
        /// </summary>
        [EnumMember(Value = "STARTUP_FAILURE")]
        StartupFailure,

        /// <summary>
        /// The check suite or run was marked stale by GitHub. Only GitHub can use this conclusion.
        /// </summary>
        [EnumMember(Value = "STALE")]
        Stale,
    }
}