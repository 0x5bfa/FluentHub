using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states in which a deployment can be.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeploymentState
    {
        /// <summary>
        /// The pending deployment was not updated after 30 minutes.
        /// </summary>
        [EnumMember(Value = "ABANDONED")]
        Abandoned,

        /// <summary>
        /// The deployment is currently active.
        /// </summary>
        [EnumMember(Value = "ACTIVE")]
        Active,

        /// <summary>
        /// An inactive transient deployment.
        /// </summary>
        [EnumMember(Value = "DESTROYED")]
        Destroyed,

        /// <summary>
        /// The deployment experienced an error.
        /// </summary>
        [EnumMember(Value = "ERROR")]
        Error,

        /// <summary>
        /// The deployment has failed.
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,

        /// <summary>
        /// The deployment is inactive.
        /// </summary>
        [EnumMember(Value = "INACTIVE")]
        Inactive,

        /// <summary>
        /// The deployment is pending.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// The deployment was successful.
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// The deployment has queued
        /// </summary>
        [EnumMember(Value = "QUEUED")]
        Queued,

        /// <summary>
        /// The deployment is in progress.
        /// </summary>
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,

        /// <summary>
        /// The deployment is waiting.
        /// </summary>
        [EnumMember(Value = "WAITING")]
        Waiting,
    }
}