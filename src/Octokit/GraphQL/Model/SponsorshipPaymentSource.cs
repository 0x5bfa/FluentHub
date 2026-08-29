using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// How payment was made for funding a GitHub Sponsors sponsorship.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorshipPaymentSource
    {
        /// <summary>
        /// Payment was made through GitHub.
        /// </summary>
        [EnumMember(Value = "GITHUB")]
        Github,

        /// <summary>
        /// Payment was made through Patreon.
        /// </summary>
        [EnumMember(Value = "PATREON")]
        Patreon,
    }
}