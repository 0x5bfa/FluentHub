using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// Properties by which project workflows can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2WorkflowsOrderField
    {
        /// <summary>
        /// The workflows' name
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,

        /// <summary>
        /// The workflows' number
        /// </summary>
        [EnumMember(Value = "NUMBER")]
        Number,

        /// <summary>
        /// The workflows' date and time of update
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// The workflows' date and time of creation
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}