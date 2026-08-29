using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
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