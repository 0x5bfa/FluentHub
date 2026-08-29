using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which sponsor and lifetime value connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorAndLifetimeValueOrderField
    {
        /// <summary>
        /// Order results by the sponsor's login (username).
        /// </summary>
        [EnumMember(Value = "SPONSOR_LOGIN")]
        SponsorLogin,

        /// <summary>
        /// Order results by the sponsor's relevance to the viewer.
        /// </summary>
        [EnumMember(Value = "SPONSOR_RELEVANCE")]
        SponsorRelevance,

        /// <summary>
        /// Order results by how much money the sponsor has paid in total.
        /// </summary>
        [EnumMember(Value = "LIFETIME_VALUE")]
        LifetimeValue,
    }
}