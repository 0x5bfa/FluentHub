using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible commit status states.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusState
    {
        /// <summary>
        /// Status is expected.
        /// </summary>
        [EnumMember(Value = "EXPECTED")]
        Expected,

        /// <summary>
        /// Status is errored.
        /// </summary>
        [EnumMember(Value = "ERROR")]
        Error,

        /// <summary>
        /// Status is failing.
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,

        /// <summary>
        /// Status is pending.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// Status is successful.
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,
    }
}