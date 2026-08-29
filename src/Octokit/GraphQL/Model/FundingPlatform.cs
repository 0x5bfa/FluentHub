using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible funding platforms for repository funding links.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FundingPlatform
    {
        /// <summary>
        /// GitHub funding platform.
        /// </summary>
        [EnumMember(Value = "GITHUB")]
        Github,

        /// <summary>
        /// Patreon funding platform.
        /// </summary>
        [EnumMember(Value = "PATREON")]
        Patreon,

        /// <summary>
        /// Open Collective funding platform.
        /// </summary>
        [EnumMember(Value = "OPEN_COLLECTIVE")]
        OpenCollective,

        /// <summary>
        /// Ko-fi funding platform.
        /// </summary>
        [EnumMember(Value = "KO_FI")]
        KoFi,

        /// <summary>
        /// Tidelift funding platform.
        /// </summary>
        [EnumMember(Value = "TIDELIFT")]
        Tidelift,

        /// <summary>
        /// Community Bridge funding platform.
        /// </summary>
        [EnumMember(Value = "COMMUNITY_BRIDGE")]
        CommunityBridge,

        /// <summary>
        /// Liberapay funding platform.
        /// </summary>
        [EnumMember(Value = "LIBERAPAY")]
        Liberapay,

        /// <summary>
        /// IssueHunt funding platform.
        /// </summary>
        [EnumMember(Value = "ISSUEHUNT")]
        Issuehunt,

        /// <summary>
        /// LFX Crowdfunding funding platform.
        /// </summary>
        [EnumMember(Value = "LFX_CROWDFUNDING")]
        LfxCrowdfunding,

        /// <summary>
        /// Polar funding platform.
        /// </summary>
        [EnumMember(Value = "POLAR")]
        Polar,

        /// <summary>
        /// Buy Me a Coffee funding platform.
        /// </summary>
        [EnumMember(Value = "BUY_ME_A_COFFEE")]
        BuyMeACoffee,

        /// <summary>
        /// Custom funding platform.
        /// </summary>
        [EnumMember(Value = "CUSTOM")]
        Custom,
    }
}