using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which security advisory connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SecurityAdvisoryOrderField
    {
        /// <summary>
        /// Order advisories by publication time
        /// </summary>
        [EnumMember(Value = "PUBLISHED_AT")]
        PublishedAt,

        /// <summary>
        /// Order advisories by update time
        /// </summary>
        [EnumMember(Value = "UPDATED_AT")]
        UpdatedAt,
    }
}