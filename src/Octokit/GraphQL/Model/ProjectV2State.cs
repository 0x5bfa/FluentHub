using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a project v2.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2State
    {
        /// <summary>
        /// A project v2 that is still open
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// A project v2 that has been closed
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,
    }
}