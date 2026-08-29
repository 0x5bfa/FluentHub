using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The level of enforcement for a rule or ruleset.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RuleEnforcement
    {
        /// <summary>
        /// Do not evaluate or enforce rules
        /// </summary>
        [EnumMember(Value = "DISABLED")]
        Disabled,

        /// <summary>
        /// Rules will be enforced
        /// </summary>
        [EnumMember(Value = "ACTIVE")]
        Active,

        /// <summary>
        /// Allow admins to test rules before enforcing them. Admins can view insights on the Rule Insights page (`evaluate` is only available with GitHub Enterprise).
        /// </summary>
        [EnumMember(Value = "EVALUATE")]
        Evaluate,
    }
}