using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a milestone.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MilestoneState
    {
        /// <summary>
        /// A milestone that is still open.
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// A milestone that has been closed.
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,
    }
}