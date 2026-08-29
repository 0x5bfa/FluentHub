using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible events to perform on a pull request review.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestReviewEvent
    {
        /// <summary>
        /// Submit general feedback without explicit approval.
        /// </summary>
        [EnumMember(Value = "COMMENT")]
        Comment,

        /// <summary>
        /// Submit feedback and approve merging these changes.
        /// </summary>
        [EnumMember(Value = "APPROVE")]
        Approve,

        /// <summary>
        /// Submit feedback that must be addressed before merging.
        /// </summary>
        [EnumMember(Value = "REQUEST_CHANGES")]
        RequestChanges,

        /// <summary>
        /// Dismiss review so it now longer effects merging.
        /// </summary>
        [EnumMember(Value = "DISMISS")]
        Dismiss,
    }
}