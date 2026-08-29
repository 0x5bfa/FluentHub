using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Represents an annotation's information level.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CheckAnnotationLevel
    {
        /// <summary>
        /// An annotation indicating an inescapable error.
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,

        /// <summary>
        /// An annotation indicating some information.
        /// </summary>
        [EnumMember(Value = "NOTICE")]
        Notice,

        /// <summary>
        /// An annotation indicating an ignorable error.
        /// </summary>
        [EnumMember(Value = "WARNING")]
        Warning,
    }
}