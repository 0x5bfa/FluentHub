using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which projects can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2OrderField
    {
        /// <summary>
        /// The project's title
        /// </summary>
        [EnumMember(Value = "TITLE")]
        Title,

        /// <summary>
        /// The project's number
        /// </summary>
        [EnumMember(Value = "NUMBER")]
        Number,

        /// <summary>
        /// The project's date and time of update
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// The project's date and time of creation
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}