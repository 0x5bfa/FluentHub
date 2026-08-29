using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Represents available types of methods to use when merging a pull request.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PullRequestMergeMethod
    {
        /// <summary>
        /// Add all commits from the head branch to the base branch with a merge commit.
        /// </summary>
        [EnumMember(Value = "MERGE")]
        Merge,

        /// <summary>
        /// Combine all commits from the head branch into a single commit in the base branch.
        /// </summary>
        [EnumMember(Value = "SQUASH")]
        Squash,

        /// <summary>
        /// Add all commits from the head branch onto the base branch individually.
        /// </summary>
        [EnumMember(Value = "REBASE")]
        Rebase,
    }
}