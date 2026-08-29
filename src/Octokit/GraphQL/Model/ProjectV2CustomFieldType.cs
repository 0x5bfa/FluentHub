using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The type of a project field.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2CustomFieldType
    {
        /// <summary>
        /// Text
        /// </summary>
        [EnumMember(Value = "TEXT")]
        Text,

        /// <summary>
        /// Single Select
        /// </summary>
        [EnumMember(Value = "SINGLE_SELECT")]
        SingleSelect,

        /// <summary>
        /// Number
        /// </summary>
        [EnumMember(Value = "NUMBER")]
        Number,

        /// <summary>
        /// Date
        /// </summary>
        [EnumMember(Value = "DATE")]
        Date,
    }
}