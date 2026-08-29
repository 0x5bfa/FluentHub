using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which Sponsors tiers connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsTierOrderField
    {
        /// <summary>
        /// Order tiers by creation time.
        /// </summary>
        [EnumMember(Value = "CREATED_AT")]
        CreatedAt,

        /// <summary>
        /// Order tiers by their monthly price in cents
        /// </summary>
        [EnumMember(Value = "MONTHLY_PRICE_IN_CENTS")]
        MonthlyPriceInCents,
    }
}