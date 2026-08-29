using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Preconfigured background patterns that may be used to style discussions pinned within a repository.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PinnedDiscussionPattern
    {
        /// <summary>
        /// A solid dot pattern
        /// </summary>
        [EnumMember(Value = "DOT_FILL")]
        DotFill,

        /// <summary>
        /// A plus sign pattern
        /// </summary>
        [EnumMember(Value = "PLUS")]
        Plus,

        /// <summary>
        /// A lightning bolt pattern
        /// </summary>
        [EnumMember(Value = "ZAP")]
        Zap,

        /// <summary>
        /// An upward-facing chevron pattern
        /// </summary>
        [EnumMember(Value = "CHEVRON_UP")]
        ChevronUp,

        /// <summary>
        /// A hollow dot pattern
        /// </summary>
        [EnumMember(Value = "DOT")]
        Dot,

        /// <summary>
        /// A heart pattern
        /// </summary>
        [EnumMember(Value = "HEART_FILL")]
        HeartFill,
    }
}