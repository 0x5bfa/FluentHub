using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible actions that GitHub Sponsors activities can represent.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsActivityAction
    {
        /// <summary>
        /// The activity was starting a sponsorship.
        /// </summary>
        [EnumMember(Value = "NEW_SPONSORSHIP")]
        NewSponsorship,

        /// <summary>
        /// The activity was cancelling a sponsorship.
        /// </summary>
        [EnumMember(Value = "CANCELLED_SPONSORSHIP")]
        CancelledSponsorship,

        /// <summary>
        /// The activity was changing the sponsorship tier, either directly by the sponsor or by a scheduled/pending change.
        /// </summary>
        [EnumMember(Value = "TIER_CHANGE")]
        TierChange,

        /// <summary>
        /// The activity was funds being refunded to the sponsor or GitHub.
        /// </summary>
        [EnumMember(Value = "REFUND")]
        Refund,

        /// <summary>
        /// The activity was scheduling a downgrade or cancellation.
        /// </summary>
        [EnumMember(Value = "PENDING_CHANGE")]
        PendingChange,

        /// <summary>
        /// The activity was disabling matching for a previously matched sponsorship.
        /// </summary>
        [EnumMember(Value = "SPONSOR_MATCH_DISABLED")]
        SponsorMatchDisabled,
    }
}