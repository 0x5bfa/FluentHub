using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a tracked issue.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TrackedIssueStates
    {
        /// <summary>
        /// The tracked issue is open
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// The tracked issue is closed
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,
    }
}