using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The merge options available for pull requests to this repository.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepoChangeMergeSettingAuditEntryMergeType
    {
        /// <summary>
        /// The pull request is added to the base branch in a merge commit.
        /// </summary>
        [EnumMember(Value = "MERGE")]
        Merge,

        /// <summary>
        /// Commits from the pull request are added onto the base branch individually without a merge commit.
        /// </summary>
        [EnumMember(Value = "REBASE")]
        Rebase,

        /// <summary>
        /// The pull request's commits are squashed into a single commit before they are merged to the base branch.
        /// </summary>
        [EnumMember(Value = "SQUASH")]
        Squash,
    }
}