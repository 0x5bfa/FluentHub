using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The different kinds of goals a GitHub Sponsors member can have.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsGoalKind
    {
        /// <summary>
        /// The goal is about reaching a certain number of sponsors.
        /// </summary>
        [EnumMember(Value = "TOTAL_SPONSORS_COUNT")]
        TotalSponsorsCount,

        /// <summary>
        /// The goal is about getting a certain amount in USD from sponsorships each month.
        /// </summary>
        [EnumMember(Value = "MONTHLY_SPONSORSHIP_AMOUNT")]
        MonthlySponsorshipAmount,
    }
}