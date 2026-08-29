using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The reasons a piece of content can be reported or minimized.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReportedContentClassifiers
    {
        /// <summary>
        /// A spammy piece of content
        /// </summary>
        [EnumMember(Value = "SPAM")]
        Spam,

        /// <summary>
        /// An abusive or harassing piece of content
        /// </summary>
        [EnumMember(Value = "ABUSE")]
        Abuse,

        /// <summary>
        /// An irrelevant piece of content
        /// </summary>
        [EnumMember(Value = "OFF_TOPIC")]
        OffTopic,

        /// <summary>
        /// An outdated piece of content
        /// </summary>
        [EnumMember(Value = "OUTDATED")]
        Outdated,

        /// <summary>
        /// A duplicated piece of content
        /// </summary>
        [EnumMember(Value = "DUPLICATE")]
        Duplicate,

        /// <summary>
        /// The content has been resolved
        /// </summary>
        [EnumMember(Value = "RESOLVED")]
        Resolved,
    }
}