using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentHub.Octokit.Models.v4
{
    /// <summary>
    /// The rule types supported in rulesets
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryRuleType
    {
        /// <summary>
        /// Creation
        /// </summary>
        [EnumMember(Value = "CREATION")]
        Creation,

        /// <summary>
        /// Update
        /// </summary>
        [EnumMember(Value = "UPDATE")]
        Update,

        /// <summary>
        /// Deletion
        /// </summary>
        [EnumMember(Value = "DELETION")]
        Deletion,

        /// <summary>
        /// Required linear history
        /// </summary>
        [EnumMember(Value = "REQUIRED_LINEAR_HISTORY")]
        RequiredLinearHistory,

        /// <summary>
        /// Required deployments
        /// </summary>
        [EnumMember(Value = "REQUIRED_DEPLOYMENTS")]
        RequiredDeployments,

        /// <summary>
        /// Required signatures
        /// </summary>
        [EnumMember(Value = "REQUIRED_SIGNATURES")]
        RequiredSignatures,

        /// <summary>
        /// Pull request
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST")]
        PullRequest,

        /// <summary>
        /// Required status checks
        /// </summary>
        [EnumMember(Value = "REQUIRED_STATUS_CHECKS")]
        RequiredStatusChecks,

        /// <summary>
        /// Non fast forward
        /// </summary>
        [EnumMember(Value = "NON_FAST_FORWARD")]
        NonFastForward,

        /// <summary>
        /// Commit message pattern
        /// </summary>
        [EnumMember(Value = "COMMIT_MESSAGE_PATTERN")]
        CommitMessagePattern,

        /// <summary>
        /// Commit author email pattern
        /// </summary>
        [EnumMember(Value = "COMMIT_AUTHOR_EMAIL_PATTERN")]
        CommitAuthorEmailPattern,

        /// <summary>
        /// Committer email pattern
        /// </summary>
        [EnumMember(Value = "COMMITTER_EMAIL_PATTERN")]
        CommitterEmailPattern,

        /// <summary>
        /// Branch name pattern
        /// </summary>
        [EnumMember(Value = "BRANCH_NAME_PATTERN")]
        BranchNamePattern,

        /// <summary>
        /// Tag name pattern
        /// </summary>
        [EnumMember(Value = "TAG_NAME_PATTERN")]
        TagNamePattern,
    }
}