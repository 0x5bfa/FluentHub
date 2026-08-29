using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// State of the project; either 'open' or 'closed'
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectState
    {
        /// <summary>
        /// The project is open.
        /// </summary>
        [EnumMember(Value = "OPEN")]
        Open,

        /// <summary>
        /// The project is closed.
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,
    }
}