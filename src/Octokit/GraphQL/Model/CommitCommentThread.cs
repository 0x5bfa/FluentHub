namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A thread of comments on a commit.
    /// </summary>
    public class CommitCommentThread : QueryableValue<CommitCommentThread>
    {
        internal CommitCommentThread(Expression expression) : base(expression)
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
        /// The Node ID of the CommitCommentThread object
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
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static CommitCommentThread Create(Expression expression)
        {
            return new CommitCommentThread(expression);
        }
    }
}