using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a pull request review comment.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestReviewCommentState
    {
        /// <summary>
        /// A comment that is part of a pending review
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// A comment that is part of a submitted review
        /// </summary>
        [EnumMember(Value = "SUBMITTED")]
        Submitted,
    }
}