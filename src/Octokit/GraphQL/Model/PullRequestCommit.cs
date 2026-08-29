namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git commit part of a pull request.
    /// </summary>
    public class PullRequestCommit : QueryableValue<PullRequestCommit>
    {
        internal PullRequestCommit(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Git commit object
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The Node ID of the PullRequestCommit object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The pull request this commit belongs to
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The HTTP path for this pull request commit
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this pull request commit
        /// </summary>
        public string Url { get; }

        internal static PullRequestCommit Create(Expression expression)
        {
            return new PullRequestCommit(expression);
        }
    }
}