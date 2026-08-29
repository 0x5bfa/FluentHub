namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an auto-merge request for a pull request
    /// </summary>
    public class AutoMergeRequest : QueryableValue<AutoMergeRequest>
    {
        internal AutoMergeRequest(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The email address of the author of this auto-merge request.
        /// </summary>
        public string AuthorEmail { get; }

        /// <summary>
        /// The commit message of the auto-merge request. If a merge queue is required by the base branch, this value will be set by the merge queue when merging.
        /// </summary>
        public string CommitBody { get; }

        /// <summary>
        /// The commit title of the auto-merge request. If a merge queue is required by the base branch, this value will be set by the merge queue when merging
        /// </summary>
        public string CommitHeadline { get; }

        /// <summary>
        /// When was this auto-merge request was enabled.
        /// </summary>
        public DateTimeOffset? EnabledAt { get; }

        /// <summary>
        /// The actor who created the auto-merge request.
        /// </summary>
        public IActor EnabledBy => this.CreateProperty(x => x.EnabledBy, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The merge method of the auto-merge request. If a merge queue is required by the base branch, this value will be set by the merge queue when merging.
        /// </summary>
        public PullRequestMergeMethod MergeMethod { get; }

        /// <summary>
        /// The pull request that this auto-merge request is set against.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static AutoMergeRequest Create(Expression expression)
        {
            return new AutoMergeRequest(expression);
        }
    }
}