namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A threaded list of comments for a given pull request.
    /// </summary>
    public class PullRequestThread : QueryableValue<PullRequestThread>
    {
        internal PullRequestThread(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of pull request comments associated with the thread.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="skip">Skips the first _n_ elements in the list.</param>
        public PullRequestReviewCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<int>? skip = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before, skip), Octokit.GraphQL.Model.PullRequestReviewCommentConnection.Create);

        /// <summary>
        /// The side of the diff on which this thread was placed.
        /// </summary>
        public DiffSide DiffSide { get; }

        /// <summary>
        /// The Node ID of the PullRequestThread object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Whether or not the thread has been collapsed (resolved)
        /// </summary>
        public bool IsCollapsed { get; }

        /// <summary>
        /// Indicates whether this thread was outdated by newer changes.
        /// </summary>
        public bool IsOutdated { get; }

        /// <summary>
        /// Whether this thread has been resolved
        /// </summary>
        public bool IsResolved { get; }

        /// <summary>
        /// The line in the file to which this thread refers
        /// </summary>
        public int? Line { get; }

        /// <summary>
        /// Identifies the file path of this thread.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Identifies the pull request associated with this thread.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Identifies the repository associated with this thread.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The user who resolved this thread
        /// </summary>
        public User ResolvedBy => this.CreateProperty(x => x.ResolvedBy, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The side of the diff that the first line of the thread starts on (multi-line only)
        /// </summary>
        public DiffSide? StartDiffSide { get; }

        /// <summary>
        /// The line of the first file diff in the thread.
        /// </summary>
        public int? StartLine { get; }

        /// <summary>
        /// The level at which the comments in the corresponding thread are targeted, can be a diff line or a file
        /// </summary>
        public PullRequestReviewThreadSubjectType SubjectType { get; }

        /// <summary>
        /// Indicates whether the current viewer can reply to this thread.
        /// </summary>
        public bool ViewerCanReply { get; }

        /// <summary>
        /// Whether or not the viewer can resolve this thread
        /// </summary>
        public bool ViewerCanResolve { get; }

        /// <summary>
        /// Whether or not the viewer can unresolve this thread
        /// </summary>
        public bool ViewerCanUnresolve { get; }

        internal static PullRequestThread Create(Expression expression)
        {
            return new PullRequestThread(expression);
        }
    }
}