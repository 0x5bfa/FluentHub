namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a Git commit part of a pull request.
    /// </summary>
    public class PullRequestCommit
    {
        /// <summary>
        /// The Git commit object
        /// </summary>
        public Commit Commit { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// The pull request this commit belongs to
        /// </summary>
        public PullRequest PullRequest { get; set; }

        /// <summary>
        /// The HTTP path for this pull request commit
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The HTTP URL for this pull request commit
        /// </summary>
        public string Url { get; set; }
    }
}