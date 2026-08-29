using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which sponsorship connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorshipOrderField
    {
        /// <summary>
        /// Order sponsorship by creation time.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}