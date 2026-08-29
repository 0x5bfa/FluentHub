using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which issue connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueOrderField
    {
        /// <summary>
        /// Order issues by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order issues by update time
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// Order issues by comment count
        /// </summary>
        [EnumMember(Value = "COMMENTS")]
        Comments,
    }
}