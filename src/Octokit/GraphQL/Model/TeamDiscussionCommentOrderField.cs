using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Properties by which team discussion comment connections can be ordered.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TeamDiscussionCommentOrderField
    {
        /// <summary>
        /// Allows sequential ordering of team discussion comments (which is equivalent to chronological ordering).
        /// </summary>
        [EnumMember(Value = "NUMBER")]
        Number,
    }
}