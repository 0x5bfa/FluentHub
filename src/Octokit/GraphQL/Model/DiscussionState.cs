using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a discussion.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiscussionState
    {
        /// <summary>
        /// A discussion that is open
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// A discussion that has been closed
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,
    }
}