using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which repository connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryOrderField
    {
        /// <summary>
        /// Order repositories by creation time
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order repositories by update time
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,

        /// <summary>
        /// Order repositories by push time
        /// </summary>
        [EnumMember(Value = "PUSHED_AT")]
        PushedAt,

        /// <summary>
        /// Order repositories by name
        /// </summary>
        [EnumMember(Value = "NAME")]
        Name,

        /// <summary>
        /// Order repositories by number of stargazers
        /// </summary>
        [EnumMember(Value = "STARGAZERS")]
        Stargazers,
    }
}