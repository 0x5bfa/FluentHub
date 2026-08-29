using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible state reasons of a closed issue.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueClosedStateReason
    {
        /// <summary>
        /// An issue that has been closed as completed
        /// </summary>
        [EnumMember(Value = "COMPLETED")]
        Completed,

        /// <summary>
        /// An issue that has been closed as not planned
        /// </summary>
        [EnumMember(Value = "NOT_PLANNED")]
        NotPlanned,
    }
}