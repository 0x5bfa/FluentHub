namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A branch protection rule.
    /// </summary>
    public class BranchProtectionRule
    {
        /// <summary>
        /// Can this branch be deleted.
        /// </summary>
        public bool AllowsDeletions { get; set; }

        /// <summary>
        /// Are force pushes allowed on this branch.
        /// </summary>
        public bool AllowsForcePushes { get; set; }

        /// <summary>
        /// Is branch creation a protected operation.
        /// </summary>
        public bool BlocksCreations { get; set; }

        /// <summary>
        /// A list of conflicts matching branches protection rule and other branch protection rules
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BranchProtectionRuleConflictConnection BranchProtectionRuleConflicts { get; set; }

        /// <summary>
        /// A list of actors able to force push for this branch protection rule.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BypassForcePushAllowanceConnection BypassForcePushAllowances { get; set; }

        /// <summary>
        /// A list of actors able to bypass PRs for this branch protection rule.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BypassPullRequestAllowanceConnection BypassPullRequestAllowances { get; set; }

        /// <summary>
        /// The actor who created this branch protection rule.
        /// </summary>
        public IActor Creator { get; set; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; set; }

        /// <summary>
        /// Will new commits pushed to matching branches dismiss pull request review approvals.
        /// </summary>
        public bool DismissesStaleReviews { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Can admins overwrite branch protection.
        /// </summary>
        public bool IsAdminEnforced { get; set; }

        /// <summary>
        /// Whether users can pull changes from upstream when the branch is locked. Set to `true` to allow fork syncing. Set to `false` to prevent fork syncing.
        /// </summary>
        public bool LockAllowsFetchAndMerge { get; set; }

        /// <summary>
        /// Whether to set the branch as read-only. If this is true, users will not be able to push to the branch.
        /// </summary>
        public bool LockBranch { get; set; }

        /// <summary>
        /// Repository refs that are protected by this rule
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">Filters refs with query on name</param>
        public RefConnection MatchingRefs { get; set; }

        /// <summary>
        /// Identifies the protection rule pattern.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// A list push allowances for this branch protection rule.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PushAllowanceConnection PushAllowances { get; set; }

        /// <summary>
        /// The repository associated with this branch protection rule.
        /// </summary>
        public Repository Repository { get; set; }

        /// <summary>
        /// Whether the most recent push must be approved by someone other than the person who pushed it
        /// </summary>
        public bool RequireLastPushApproval { get; set; }

        /// <summary>
        /// Number of approving reviews required to update matching branches.
        /// </summary>
        public int? RequiredApprovingReviewCount { get; set; }

        /// <summary>
        /// List of required deployment environments that must be deployed successfully to update matching branches
        /// </summary>
        public List<string> RequiredDeploymentEnvironments { get; set; }

        /// <summary>
        /// List of required status check contexts that must pass for commits to be accepted to matching branches.
        /// </summary>
        public List<string> RequiredStatusCheckContexts { get; set; }

        /// <summary>
        /// List of required status checks that must pass for commits to be accepted to matching branches.
        /// </summary>
        public List<RequiredStatusCheckDescription> RequiredStatusChecks { get; set; }

        /// <summary>
        /// Are approving reviews required to update matching branches.
        /// </summary>
        public bool RequiresApprovingReviews { get; set; }

        /// <summary>
        /// Are reviews from code owners required to update matching branches.
        /// </summary>
        public bool RequiresCodeOwnerReviews { get; set; }

        /// <summary>
        /// Are commits required to be signed.
        /// </summary>
        public bool RequiresCommitSignatures { get; set; }

        /// <summary>
        /// Are conversations required to be resolved before merging.
        /// </summary>
        public bool RequiresConversationResolution { get; set; }

        /// <summary>
        /// Does this branch require deployment to specific environments before merging
        /// </summary>
        public bool RequiresDeployments { get; set; }

        /// <summary>
        /// Are merge commits prohibited from being pushed to this branch.
        /// </summary>
        public bool RequiresLinearHistory { get; set; }

        /// <summary>
        /// Are status checks required to update matching branches.
        /// </summary>
        public bool RequiresStatusChecks { get; set; }

        /// <summary>
        /// Are branches required to be up to date before merging.
        /// </summary>
        public bool RequiresStrictStatusChecks { get; set; }

        /// <summary>
        /// Is pushing to matching branches restricted.
        /// </summary>
        public bool RestrictsPushes { get; set; }

        /// <summary>
        /// Is dismissal of pull request reviews restricted.
        /// </summary>
        public bool RestrictsReviewDismissals { get; set; }

        /// <summary>
        /// A list review dismissal allowances for this branch protection rule.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReviewDismissalAllowanceConnection ReviewDismissalAllowances { get; set; }
    }
}