using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The possible default commit messages for squash merges.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SquashMergeCommitMessage
    {
        /// <summary>
        /// Default to the pull request's body.
        /// </summary>
        [EnumMember(Value = "PR_BODY")]
        PrBody,

        /// <summary>
        /// Default to the branch's commit messages.
        /// </summary>
        [EnumMember(Value = "COMMIT_MESSAGES")]
        CommitMessages,

        /// <summary>
        /// Default to a blank commit message.
        /// </summary>
        [EnumMember(Value = "BLANK")]
        Blank,
    }
}