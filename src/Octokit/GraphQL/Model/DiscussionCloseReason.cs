using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible reasons for closing a discussion.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiscussionCloseReason
    {
        /// <summary>
        /// The discussion has been resolved
        /// </summary>
        [EnumMember(Value = "RESOLVED")]
        Resolved,

        /// <summary>
        /// The discussion is no longer relevant
        /// </summary>
        [EnumMember(Value = "OUTDATED")]
        Outdated,

        /// <summary>
        /// The discussion is a duplicate of another
        /// </summary>
        [EnumMember(Value = "DUPLICATE")]
        Duplicate,
    }
}