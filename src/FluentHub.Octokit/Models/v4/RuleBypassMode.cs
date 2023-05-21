using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The bypass mode for a rule or ruleset.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RuleBypassMode
    {
        /// <summary>
        /// Bypassing is disabled
        /// </summary>
        [EnumMember(Value = "NONE")]
        None,

        /// <summary>
        /// Those with bypass permission at the repository level can bypass
        /// </summary>
        [EnumMember(Value = "REPOSITORY")]
        Repository,

        /// <summary>
        /// Those with bypass permission at the organization level can bypass
        /// </summary>
        [EnumMember(Value = "ORGANIZATION")]
        Organization,
    }
}