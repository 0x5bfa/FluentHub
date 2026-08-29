using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible states for a deployment review.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeploymentReviewState
    {
        /// <summary>
        /// The deployment was approved.
        /// </summary>
        [EnumMember(Value = "APPROVED")]
        Approved,

        /// <summary>
        /// The deployment was rejected.
        /// </summary>
        [EnumMember(Value = "REJECTED")]
        Rejected,
    }
}