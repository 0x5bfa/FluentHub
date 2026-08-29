using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// The rule types supported in rulesets
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RepositoryRuleType
    {
        /// <summary>
        /// Only allow users with bypass permission to create matching refs.
        /// </summary>
        [EnumMember(Value = "CREATION")]
        Creation,

        /// <summary>
        /// Only allow users with bypass permission to update matching refs.
        /// </summary>
        [EnumMember(Value = "UPDATE")]
        Update,

        /// <summary>
        /// Only allow users with bypass permissions to delete matching refs.
        /// </summary>
        [EnumMember(Value = "DELETION")]
        Deletion,

        /// <summary>
        /// Prevent merge commits from being pushed to matching refs.
        /// </summary>
        [EnumMember(Value = "REQUIRED_LINEAR_HISTORY")]
        RequiredLinearHistory,

        /// <summary>
        /// Merges must be performed via a merge queue.
        /// </summary>
        [EnumMember(Value = "MERGE_QUEUE")]
        MergeQueue,

        /// <summary>
        /// When enabled, all conversations on code must be resolved before a pull request can be merged into a branch that matches this rule.
        /// </summary>
        [EnumMember(Value = "REQUIRED_REVIEW_THREAD_RESOLUTION")]
        RequiredReviewThreadResolution,

        /// <summary>
        /// Choose which environments must be successfully deployed to before refs can be pushed into a ref that matches this rule.
        /// </summary>
        [EnumMember(Value = "REQUIRED_DEPLOYMENTS")]
        RequiredDeployments,

        /// <summary>
        /// Commits pushed to matching refs must have verified signatures.
        /// </summary>
        [EnumMember(Value = "REQUIRED_SIGNATURES")]
        RequiredSignatures,

        /// <summary>
        /// Require all commits be made to a non-target branch and submitted via a pull request before they can be merged.
        /// </summary>
        [EnumMember(Value = "PULL_REQUEST")]
        PullRequest,

        /// <summary>
        /// Choose which status checks must pass before the ref is updated. When enabled, commits must first be pushed to another ref where the checks pass.
        /// </summary>
        [EnumMember(Value = "REQUIRED_STATUS_CHECKS")]
        RequiredStatusChecks,

        /// <summary>
        /// Require all commits be made to a non-target branch and submitted via a pull request and required workflow checks to pass before they can be merged.
        /// </summary>
        [EnumMember(Value = "REQUIRED_WORKFLOW_STATUS_CHECKS")]
        RequiredWorkflowStatusChecks,

        /// <summary>
        /// Prevent users with push access from force pushing to refs.
        /// </summary>
        [EnumMember(Value = "NON_FAST_FORWARD")]
        NonFastForward,

        /// <summary>
        /// Authorization
        /// </summary>
        [EnumMember(Value = "AUTHORIZATION")]
        Authorization,

        /// <summary>
        /// Tag
        /// </summary>
        [EnumMember(Value = "TAG")]
        Tag,

        /// <summary>
        /// Merge queue locked ref
        /// </summary>
        [EnumMember(Value = "MERGE_QUEUE_LOCKED_REF")]
        MergeQueueLockedRef,

        /// <summary>
        /// Branch is read-only. Users cannot push to the branch.
        /// </summary>
        [EnumMember(Value = "LOCK_BRANCH")]
        LockBranch,

        /// <summary>
        /// Max ref updates
        /// </summary>
        [EnumMember(Value = "MAX_REF_UPDATES")]
        MaxRefUpdates,

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

        /// <summary>
        /// Require all changes made to a targeted branch to pass the specified workflows before they can be merged.
        /// </summary>
        [EnumMember(Value = "WORKFLOWS")]
        Workflows,

        /// <summary>
        /// Secret scanning
        /// </summary>
        [EnumMember(Value = "SECRET_SCANNING")]
        SecretScanning,

        /// <summary>
        /// Workflow files cannot be modified.
        /// </summary>
        [EnumMember(Value = "WORKFLOW_UPDATES")]
        WorkflowUpdates,
    }
}