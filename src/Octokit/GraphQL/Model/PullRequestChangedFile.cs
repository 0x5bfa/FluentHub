namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A file changed in a pull request.
    /// </summary>
    public class PullRequestChangedFile : QueryableValue<PullRequestChangedFile>
    {
        internal PullRequestChangedFile(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of additions to the file.
        /// </summary>
        public int Additions { get; }

        /// <summary>
        /// How the file was changed in this PullRequest
        /// </summary>
        public PatchStatus ChangeType { get; }

        /// <summary>
        /// The number of deletions to the file.
        /// </summary>
        public int Deletions { get; }

        /// <summary>
        /// The path of the file.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The state of the file for the viewer.
        /// </summary>
        public FileViewedState ViewerViewedState { get; }

        internal static PullRequestChangedFile Create(Expression expression)
        {
            return new PullRequestChangedFile(expression);
        }
    }
}