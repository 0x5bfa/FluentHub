using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The billing plans available for organizations.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrgCreateAuditEntryBillingPlan
    {
        /// <summary>
        /// Free Plan
        /// </summary>
        [EnumMember(Value = "FREE")]
        Free,

        /// <summary>
        /// Team Plan
        /// </summary>
        [EnumMember(Value = "BUSINESS")]
        Business,

        /// <summary>
        /// Enterprise Cloud Plan
        /// </summary>
        [EnumMember(Value = "BUSINESS_PLUS")]
        BusinessPlus,

        /// <summary>
        /// Legacy Unlimited Plan
        /// </summary>
        [EnumMember(Value = "UNLIMITED")]
        Unlimited,

        /// <summary>
        /// Tiered Per Seat Plan
        /// </summary>
        [EnumMember(Value = "TIERED_PER_SEAT")]
        TieredPerSeat,
    }
}