using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible default commit titles for squash merges.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SquashMergeCommitTitle
    {
        /// <summary>
        /// Default to the pull request's title.
        /// </summary>
        [EnumMember(Value = "PR_TITLE")]
        PrTitle,

        /// <summary>
        /// Default to the commit's title (if only one commit) or the pull request's title (when more than one commit).
        /// </summary>
        [EnumMember(Value = "COMMIT_OR_PR_TITLE")]
        CommitOrPrTitle,
    }
}