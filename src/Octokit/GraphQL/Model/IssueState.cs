using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of an issue.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueState
    {
        /// <summary>
        /// An issue that is still open
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// An issue that has been closed
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,
    }
}