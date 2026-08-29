using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which project workflows can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProjectV2WorkflowsOrderField
    {
        /// <summary>
        /// The name of the workflow
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,

        /// <summary>
        /// The number of the workflow
        /// </summary>
        [EnumMember(Value = "NUMBER")]
        Number,

        /// <summary>
        /// The date and time of the workflow update
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// The date and time of the workflow creation
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}