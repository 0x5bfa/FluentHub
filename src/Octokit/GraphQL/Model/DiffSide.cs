using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible sides of a diff.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiffSide
    {
        /// <summary>
        /// The left side of the diff.
        /// </summary>
        [EnumMember(Value = "LEFT")]
        Left,

        /// <summary>
        /// The right side of the diff.
        /// </summary>
        [EnumMember(Value = "RIGHT")]
        Right,
    }
}