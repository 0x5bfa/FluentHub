namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Branch protection rules that are enforced on the viewer.
    /// </summary>
    public class RefUpdateRule : QueryableValue<RefUpdateRule>
    {
        internal RefUpdateRule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Can this branch be deleted.
        /// </summary>
        public bool AllowsDeletions { get; }

        /// <summary>
        /// Are force pushes allowed on this branch.
        /// </summary>
        public bool AllowsForcePushes { get; }

        /// <summary>
        /// Can matching branches be created.
        /// </summary>
        public bool BlocksCreations { get; }

        /// <summary>
        /// Identifies the protection rule pattern.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Number of approving reviews required to update matching branches.
        /// </summary>
        public int? RequiredApprovingReviewCount { get; }

        /// <summary>
        /// List of required status check contexts that must pass for commits to be accepted to matching branches.
        /// </summary>
        public IEnumerable<string> RequiredStatusCheckContexts { get; }

        /// <summary>
        /// Are reviews from code owners required to update matching branches.
        /// </summary>
        public bool RequiresCodeOwnerReviews { get; }

        /// <summary>
        /// Are conversations required to be resolved before merging.
        /// </summary>
        public bool RequiresConversationResolution { get; }

        /// <summary>
        /// Are merge commits prohibited from being pushed to this branch.
        /// </summary>
        public bool RequiresLinearHistory { get; }

        /// <summary>
        /// Are commits required to be signed.
        /// </summary>
        public bool RequiresSignatures { get; }

        /// <summary>
        /// Is the viewer allowed to dismiss reviews.
        /// </summary>
        public bool ViewerAllowedToDismissReviews { get; }

        /// <summary>
        /// Can the viewer push to the branch
        /// </summary>
        public bool ViewerCanPush { get; }

        internal static RefUpdateRule Create(Expression expression)
        {
            return new RefUpdateRule(expression);
        }
    }
}