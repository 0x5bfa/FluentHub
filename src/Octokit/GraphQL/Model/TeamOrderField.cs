using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which team connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamOrderField
    {
        /// <summary>
        /// Allows ordering a list of teams by name.
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,
    }
}