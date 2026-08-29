using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states of a pull request review.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestReviewState
    {
        /// <summary>
        /// A review that has not yet been submitted.
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// An informational review.
        /// </summary>
        [EnumMember(Value = "COMMENTED")]
        Commented,

        /// <summary>
        /// A review allowing the pull request to merge.
        /// </summary>
        [EnumMember(Value = "APPROVED")]
        Approved,

        /// <summary>
        /// A review blocking the pull request from merging.
        /// </summary>
        [EnumMember(Value = "CHANGES_REQUESTED")]
        ChangesRequested,

        /// <summary>
        /// A review that has been dismissed.
        /// </summary>
        [EnumMember(Value = "DISMISSED")]
        Dismissed,
    }
}