using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which gist connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GistOrderField
    {
        /// <summary>
        /// Order gists by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order gists by update time
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// Order gists by push time
        /// </summary>
        [EnumMember(Value = "PUSHED_AT")]
        PushedAt,
    }
}