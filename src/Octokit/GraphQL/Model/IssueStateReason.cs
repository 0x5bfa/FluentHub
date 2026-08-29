using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible state reasons of an issue.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueStateReason
    {
        /// <summary>
        /// An issue that has been reopened
        /// </summary>
        [EnumMember(Value = "REOPENED")]
        Reopened,

        /// <summary>
        /// An issue that has been closed as not planned
        /// </summary>
        [EnumMember(Value = "NOT_PLANNED")]
        NotPlanned,

        /// <summary>
        /// An issue that has been closed as completed
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed,
    }
}