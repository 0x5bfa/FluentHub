using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which sponsorship update connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorshipNewsletterOrderField
    {
        /// <summary>
        /// Order sponsorship newsletters by when they were created.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,
    }
}