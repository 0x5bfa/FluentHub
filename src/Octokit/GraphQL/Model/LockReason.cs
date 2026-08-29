using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible reasons that an issue or pull request was locked.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LockReason
    {
        /// <summary>
        /// The issue or pull request was locked because the conversation was off-topic.
        /// </summary>
        [EnumMember(Value = "OFF_TOPIC")]
        OffTopic,

        /// <summary>
        /// The issue or pull request was locked because the conversation was too heated.
        /// </summary>
        [EnumMember(Value = "TOO_HEATED")]
        TooHeated,

        /// <summary>
        /// The issue or pull request was locked because the conversation was resolved.
        /// </summary>
        [EnumMember(Value = "RESOLVED")]
        Resolved,

        /// <summary>
        /// The issue or pull request was locked because the conversation was spam.
        /// </summary>
        [EnumMember(Value = "SPAM")]
        Spam,
    }
}