using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible protection rule types.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeploymentProtectionRuleType
    {
        /// <summary>
        /// Required reviewers
        /// </summary>
        [EnumMember(Value = "REQUIRED_REVIEWERS")]
        RequiredReviewers,

        /// <summary>
        /// Wait timer
        /// </summary>
        [EnumMember(Value = "WAIT_TIMER")]
        WaitTimer,
    }
}