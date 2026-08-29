namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a commit comment thread part of a pull request.
    /// </summary>
    public class PullRequestCommitCommentThread : QueryableValue<PullRequestCommitCommentThread>
    {
        internal PullRequestCommitCommentThread(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The comments that exist in this thread.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CommitCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octokit.GraphQL.Model.CommitCommentConnection.Create);

        /// <summary>
        /// The commit the comments were made on.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The Node ID of the PullRequestCommitCommentThread object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The file the comments were made on.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The position in the diff for the commit that the comment was made on.
        /// </summary>
        public int? Position { get; }

        /// <summary>
        /// The pull request this commit comment thread belongs to
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static PullRequestCommitCommentThread Create(Expression expression)
        {
            return new PullRequestCommitCommentThread(expression);
        }
    }
}