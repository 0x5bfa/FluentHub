using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The bypass mode for a specific actor on a ruleset.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryRulesetBypassActorBypassMode
    {
        /// <summary>
        /// The actor can always bypass rules
        /// </summary>
        [EnumMember(Value = "ALWAYS")]
        Always,

        /// <summary>
        /// The actor can only bypass rules via a pull request
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST")]
        PullRequest,
    }
}