using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The targets supported for rulesets
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryRulesetTarget
    {
        /// <summary>
        /// Branch
        /// </summary>
        [EnumMember(Value = "BRANCH")]
        Branch,

        /// <summary>
        /// Tag
        /// </summary>
        [EnumMember(Value = "TAG")]
        Tag,
    }
}